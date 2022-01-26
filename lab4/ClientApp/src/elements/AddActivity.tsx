import { useContext, useEffect, useState } from "react";
import { Button, Form } from "react-bootstrap";
import ApiRequest from "../ApiRequest";
import { DateContext } from "../contexts/DateContext";
import { ActivityCreate } from "../models/activity";
import { ProjectAll } from "../models/project";

function newActivityCreate(date: string) {
    return {
        projectCode: "",
        date: date,
        subprojectCode: "",
        time: 1,
        description: ""
    };
};

export default function AddActivity() {
    const dateState = useContext(DateContext);
    const [activity, setActivity] = useState<ActivityCreate>(newActivityCreate(dateState.state));
    const [projects, setProjects] = useState<ProjectAll[]>([]);
    const [subprojectCodes, setSubprojectCodes] = useState<string[]>([]);

    const setProjectCode = (projectCode: string) => setActivity({ ...activity, projectCode: projectCode });
    const setSubprojectCode = (subprojectCode: string) => setActivity({ ...activity, subprojectCode: subprojectCode });
    const setTime = (time: number) => setActivity({ ...activity, time: time });
    const setDescription = (description: string) => setActivity({ ...activity, description: description });

    function onClear() {
        setActivity(newActivityCreate(dateState.state));
        setSubprojectCodes([]);
    }

    function onSubmit() {
        if (activity.projectCode !== "") {
            ApiRequest.add_activity(activity).then(_ => {
                let temp = dateState.state;
                dateState.setState("0001-01-01");
                dateState.setState(temp);
                onClear();
            });
        }
    }

    useEffect(() => {
        ApiRequest.projects().then(projects => setProjects(projects));
    }, []);

    useEffect(() => {
        setActivity(a => { a.date = dateState.state; return a; });
    }, [dateState.state]);

    useEffect(() => {
        if (activity.projectCode !== "") {
            setSubprojectCode("");
            ApiRequest.subprojectCodes(activity.projectCode)
                .then(
                    subprojects => setSubprojectCodes(subprojects)
                );
        }
    }, [activity.projectCode]);

    return (
        <>
            <td>
                <Form.Select required value={activity.projectCode} onChange={e => setProjectCode(e.target.value)}>
                    <option key={""} value={""} hidden disabled selected>Select project</option>
                    {projects.map(project => (
                        <option key={project.projectCode} value={project.projectCode}>{`${project.projectCode} - ${project.projectName}`}</option>
                    ))}
                </Form.Select>
            </td>
            <td>
                <Form.Select required value={activity.subprojectCode} onChange={e => setSubprojectCode(e.target.value)}>
                    <option key={""} value={""} hidden disabled selected>Select subproject (optional)</option>
                    <option selected></option>
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
                <Button onClick={_ => onSubmit()}>Submit</Button>
                <Button onClick={_ => onClear()}>Clear</Button>
            </td>
        </>
    );
}
