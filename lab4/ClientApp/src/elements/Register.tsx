import { useContext, useState } from 'react';
import { Form, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import ApiRequest from '../ApiRequest';
import { UserAll } from '../models/user';
import { UserContext } from '../contexts/UserContext';

export default function Register() {
    const userState = useContext(UserContext);
    const navigate = useNavigate();
    const [username, setUsername] = useState('');
    const [error, setError] = useState<string | null>(null)

    function onSubmit(event: any) {
        setError(null);
        if (username != "") {
            ApiRequest.register({ userName: username }).then(
                (_) => {
                    ApiRequest.me().then(
                        (user: UserAll) => {
                            userState.setState(user?.userName);
                        }
                    );
                    navigate("/activities");
                },
                (_) => {
                    setError("Username already in use!");
                }
            )
        }
    }

    let error_message = error ?
        (<p className="text-center mt-2" style={{ opacity: "60%", color: "red" }}>{error}</p>) :
        (<></>);

    return (
        <div className="d-flex justify-content-center align-items-center h-100">
            <div className="d-flex flex-column" style={{ width: "20%" }}>
                <Form>
                    <Button className="mb-2 w-100 justify-content-center" variant="primary" onClick={onSubmit}>
                        Register
                    </Button>
                    <Form.Control required type="text" onChange={event => setUsername(event.target.value)} placeholder="Enter username"></Form.Control>
                    {error_message}
                </Form>
            </div>
        </div>
    );
}
