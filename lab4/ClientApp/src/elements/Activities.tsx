import { useEffect, useState } from "react";
import { Button, Table } from "react-bootstrap";
import ApiRequest from "../ApiRequest";
import { ActivityAll } from "../models/activity";
import { ActivitiesReport } from "../models/report";
import { format } from 'date-fns'
import AddActivity from "./AddActivity";
import { DateState, DateContext } from "../contexts/DateContext";
import EditActivity from "./EditActivity";

function ShowColumnNames() {
    return (
        <tr>
            <td className="text-center" style={{ width: "20%" }}>Project</td>
            <td className="text-center" style={{ width: "20%" }}>Subproject</td>
            <td className="text-center" style={{ width: "20%" }}>Time [min]</td>
            <td className="text-center" style={{ width: "20%" }}>Description</td>
            <td style={{ width: "1px" }}></td>
            <td className="text-center" style={{ width: "20%" }}>Options</td>
        </tr>
    );
}

function ShowActivity(onDelete: (activity: ActivityAll) => void, onEdit: (activity: ActivityAll) => void, activity: ActivityAll, frozen: boolean) {
    let options = frozen ?
        (<div style={{ opacity: 0.2 }}>Month frozen</div>) :
        (<div className="d-flex justify-content-around">
            <Button className="btn-warning" onClick={_ => onEdit(activity)}>Edit</Button>
            <Button className="btn-danger" onClick={_ => onDelete(activity)}>Delete</Button>
        </div>);

    return (
        <tr key={activity.activityPid}>
            <td className="text-center align-middle">{activity.projectName}</td>
            <td className="text-center align-middle">{activity.subprojectCode}</td>
            <td className="text-center align-middle">{activity.time}</td>
            <td className="text-center align-middle">{activity.description}</td>
            <td></td>
            <td className="text-center align-middle">{options}</td>
        </tr>
    );
}

export default function Activities() {
    const [date, setDate] = useState(format(new Date(), "yyyy-MM-dd"));
    const [editing, setEditing] = useState<number | null>(null);
    const [report, setReport] = useState<ActivitiesReport>({
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
        ApiRequest.deleteActivity(activity).then(_ => {
            let temp = dateState.state;
            dateState.setState("0001-01-01");
            dateState.setState(temp);
        });
    }

    const footer = report.frozen ?
        <></> :
        <tfoot>
            <tr><AddActivity /></tr>
        </tfoot>

    function totalTime() {
        let total = report.activities.map(a => a.time).reduce((total, current) => total += current, 0);
        return `${total} ${total === 1 ? "minute" : "minutes"}`;
    }

    useEffect(() => {
        if (dateState.state !== "0001-01-01")
            ApiRequest.activitiesReport(dateState.state).then((report: ActivitiesReport) => {
                report.activities.forEach(a => a.projectName = `${a.projectCode} - ${a.projectName}`);
                setReport(report);
            });
    }, [dateState.state]);

    return (
        <div className="m-5">
            <DateContext.Provider value={dateState}>
                <h2>
                    <div className="d-flex flex-grow justify-content-between mx-5">
                        <div>
                            <span>Activities for </span>
                            <input type="date" value={dateState.state} onChange={e => dateState.setState(e.target.value)} style={{ border: 'None', outline: 'None' }}></input>
                        </div>
                        <span>{`Total time: ${totalTime()}`}</span>
                        <span>{`Month state: ${report.frozen ? "Frozen" : "Active"}`}</span>

                    </div>
                </h2>
                <Table bordered striped className="mt-5">
                    <thead>
                        <ShowColumnNames />
                    </thead>
                    <tbody>
                        {report.activities.map(activity =>
                            (editing === activity.activityPid) ?
                                <EditActivity activity={activity} onCancelEdit={onCancelEdit} /> :
                                ShowActivity(onDelete, onEdit, activity, report.frozen)
                        )}
                    </tbody>
                    {footer}
                </Table>
            </DateContext.Provider>
        </div>
    );
}
