import { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import ApiRequest from "../ApiRequest";
import { acceptedActivitiesReport, ActivitiesReport } from "../models/report";
import { format } from 'date-fns'

function ShowColumnNames() {
    return (
        <tr>
            <td className="text-center" style={{ width: "25%" }}>Month</td>
            <td className="text-center" style={{ width: "25%" }}>Project</td>
            <td className="text-center" style={{ width: "25%" }}>Submited time</td>
            <td className="text-center" style={{ width: "25%" }}>Accepted time</td>
        </tr>
    );
}

export default function AcceptedActivities() {
    const [report, setReport] = useState<acceptedActivitiesReport>({
        acceptedActivities: []
    });

    useEffect(() => { ApiRequest.acceptedActivitiesReport().then(report => setReport(report)); }, []);

    return (
        <div className="m-5">
            <h2 className="text-center">Accepted entries</h2>
            <Table bordered striped className="mt-5">
                <thead>
                    <ShowColumnNames />
                </thead>
                <tbody>
                    {report.acceptedActivities.map(a =>
                        <tr>
                            <td className="text-center">{format(new Date(a.reportMonth), "y LLLL")}</td>
                            <td className="text-center">{`${a.projectCode} - ${a.projectName}`}</td>
                            <td className="text-center">{`${a.submitedTime} ${a.submitedTime === 1 ? "minute" : "minutes"}`}</td>
                            <td className="text-center">{`${a.acceptedTime} ${a.acceptedTime === 1 ? "minute" : "minutes"}`}</td>
                        </tr>
                    )}
                </tbody>
            </Table>
        </div>
    );
}
