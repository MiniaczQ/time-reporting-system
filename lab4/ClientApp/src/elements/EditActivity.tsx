import { useContext, useEffect, useState } from "react";
import { Button, Form } from "react-bootstrap";
import ApiRequest from "../ApiRequest";
import { DateContext } from "../contexts/DateContext";
import { ActivityAll } from "../models/activity";

type EditActivityProperties = {
    activity: ActivityAll;
    onCancelEdit: () => void;
}

export default function EditActivity(properties: EditActivityProperties) {
    const dateState = useContext(DateContext);
    const [activity, setActivity] = useState<ActivityAll>(properties.activity);
    const [subprojectCodes, setSubprojectCodes] = useState<string[]>([]);

    const setSubprojectCode = (subprojectCode: string) => setActivity({ ...activity, subprojectCode: subprojectCode });
    const setTime = (time: number) => setActivity({ ...activity, time: time });
    const setDescription = (description: string) => setActivity({ ...activity, description: description });

    function onConfirm() {
        ApiRequest.edit_activity(activity).then(_ => {
            properties.onCancelEdit();
            let temp = dateState.state;
            dateState.setState("0001-01-01");
            dateState.setState(temp);
        })
    }

    useEffect(() => {
        ApiRequest.subprojectCodes(activity.projectCode).then(subprojects => setSubprojectCodes(subprojects));
    }, []);

    return (
        <tr key={activity.activityPid}>
            <td>
                <Form.Select disabled>
                    <option key={""} selected>{`${activity.projectCode} - ${activity.projectName}`}</option>
                </Form.Select>
            </td>
            <td>
                <Form.Select required value={activity.subprojectCode} onChange={e => setSubprojectCode(e.target.value)}>
                    <option key={""} selected></option>
                    {subprojectCodes.map(subprojectCode => (
                        <option key={subprojectCode} value={subprojectCode}>{subprojectCode}</option>
                    ))}
                </Form.Select>
            </td>
            <td>
                <Form.Control required type="number" step={1} value={activity.time} min={1} onChange={e => setTime(Number(e.target.value))}></Form.Control>
            </td>
            <td>
                <Form.Control type="text" value={activity.description} onChange={e => setDescription(e.target.value)}></Form.Control>
            </td>
            <td>
                <Button onClick={_ => onConfirm()}>Confirm</Button>
                <Button onClick={_ => properties.onCancelEdit()}>Cancel</Button>
            </td>
        </tr>
    );
}
