import { useEffect, useState } from "react";
import { Button, Table } from "react-bootstrap";
import ApiRequest from "../ApiRequest";
import { ActivityAll } from "../models/activity";
import { ReportAll } from "../models/report";
import { format } from 'date-fns'
import AddActivity from "./AddActivity";
import { DateState, DateContext } from "../contexts/DateContext";
import EditActivity from "./EditActivity";

function ShowColumnNames() {
    return (
        <tr>
            <td>Project</td>
            <td>Subproject</td>
            <td>Time [min]</td>
            <td>Description</td>
            <td>Options</td>
        </tr>
    );
}

function ShowActivity(onDelete: (activity: ActivityAll) => void, onEdit: (activity: ActivityAll) => void, activity: ActivityAll, frozen: boolean) {
    let options = frozen ?
        (<div style={{ opacity: 0.2 }}>Month frozen</div>) :
        (<>
            <Button onClick={_ => onEdit(activity)}>Edit</Button>
            <Button onClick={_ => onDelete(activity)}>Delete</Button>
        </>);

    return (
        <tr key={activity.activityPid}>
            <td>{activity.projectName}</td>
            <td>{activity.subprojectCode}</td>
            <td>{activity.time}</td>
            <td>{activity.description}</td>
            <td>{options}</td>
        </tr>
    );
}

export default function Activities() {
    const [date, setDate] = useState(format(new Date(), "yyyy-MM-dd"));
    const [editing, setEditing] = useState<number | null>(null);
    const [report, setReport] = useState<ReportAll>({
        activities: [],
        frozen: false,
    });

    const dateState: DateState = {
        state: date,
        setState: setDate,
    }

    function onEdit(activity: ActivityAll) {
        setEditing(activity.activityPid);
    }

    function onCancelEdit() {
        setEditing(null);
    }

    function onDelete(activity: ActivityAll) {
        ApiRequest.delete_activity(activity).then(_ => {
            let temp = dateState.state;
            dateState.setState("0001-01-01");
            dateState.setState(temp);
        });
    }

    useEffect(() => {
        if (dateState.state !== "0001-01-01")
            ApiRequest.report(dateState.state).then((report: ReportAll) => {
                report.activities.forEach(a => a.projectName = `${a.projectCode} - ${a.projectName}`);
                setReport(report);
            });
    }, [dateState.state]);

    return (
        <DateContext.Provider value={dateState}>
            <Table bordered>
                <thead>
                    <tr>
                        <td>
                            <input type="date" value={dateState.state} onChange={e => dateState.setState(e.target.value)} style={{ border: 'None', outline: 'None' }}></input>
                        </td>
                    </tr>
                    <ShowColumnNames />
                </thead>
                <tbody>
                    {report.activities.map(activity =>
                        (editing === activity.activityPid) ?
                            <EditActivity activity={activity} onCancelEdit={onCancelEdit} /> :
                            ShowActivity(onDelete, onEdit, activity, report.frozen)
                    )}
                </tbody>
                <tfoot>
                    <tr><AddActivity /></tr>
                </tfoot>
            </Table>
        </DateContext.Provider>
    );
}
