import { useContext, useState } from 'react';
import { Form, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import ApiRequest from '../ApiRequest';
import User from '../models/user';
import { UserContext } from '../UserContext';

export default function Register() {
    const userState = useContext(UserContext);
    const navigate = useNavigate();
    const [username, setUsername] = useState('');
    const [error, setError] = useState<string | null>(null)

    function onSubmit(event: any) {
        ApiRequest.register({ userName: username }).then(
            (_) => {
                ApiRequest.me().then(
                    (user: User) => {
                        userState.setState(user.userName);
                        navigate("/");
                    }
                )
            },
            (_) => {
                setError("Username already in use!");
            }
        )
    }

    let error_message = error ? (
        <>
            <Form.Group className="mb-3">
                {error}
            </Form.Group>
        </>
    ) : (<></>);

    return (
        <>
            <Form>
                <Form.Group className="mb-3">
                    <Form.Label>Username</Form.Label>
                    <Form.Control type="text" className="w-25" onChange={event => setUsername(event.target.value)}></Form.Control>
                </Form.Group>
                <Button variant="primary" className="mb-3" onClick={onSubmit}>
                    Register
                </Button>
                {error_message}
            </Form>
        </>
    );
}
