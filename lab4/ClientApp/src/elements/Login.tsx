import { useContext, useEffect, useState } from 'react';
import { Form, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import ApiRequest from '../ApiRequest';
import { UserAll as User } from '../models/user';
import { UserContext } from '../contexts/UserContext';

export default function Login() {
    const userState = useContext(UserContext);
    const navigate = useNavigate();
    const [username, setUsername] = useState<string>('');
    const [usernames, setUsernames] = useState<string[]>([]);

    function onSubmit(event: any) {
        if (username != "") {
            ApiRequest.login({ userName: username }).then(
                (_) => {
                    ApiRequest.me().then(
                        (user: User) => {
                            userState.setState(user?.userName);
                        }
                    );
                    navigate("/activities");
                }
            )
        }
    }

    useEffect(() => {
        ApiRequest.users().then((users: User[]) => setUsernames(users.map(u => u.userName)));
    }, []);

    return (
        <div className="d-flex justify-content-center align-items-center h-100">
            <div className="d-flex flex-column" style={{ width: "20%" }}>
                <Form>
                    <Button variant="primary" className="mb-2 w-100 justify-content-center" onClick={onSubmit}>
                        Log in
                    </Button>
                    <Form.Select required value={username} onChange={event => setUsername(event.target.value)}>
                        <option key={""} value="" hidden disabled selected>Select user</option>
                        {usernames.map(username => (
                            <option key={username} value={username}>{username}</option>
                        ))}
                    </Form.Select>
                </Form>
            </div>
        </div>
    );
}
