import { useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import ApiRequest from '../ApiRequest';
import { UserContext } from '../contexts/UserContext';

export default function Navbar() {
    const userState = useContext(UserContext);

    const logout = () => {
        ApiRequest.logout().then((_) => userState.setState(null));
    }

    const navigation = userState.state ?
        (<>
            <Link className="nav-link link-light" to="/activities">
                Activities
            </Link>
            <Link className="nav-link link-light" to="/accepted_activities">
                Accepted activities
            </Link>
        </>) :
        (<></>)

    const session = userState.state ?
        (<>
            <Link className="nav-link" to="#">Logged in as {userState.state}.</Link>
            <Link className="nav-link" to="/login" onClick={() => logout()}>Logout</Link>
        </>) :
        (<>
            <Link className="nav-link" to="/login">Log in</Link>
            <Link className="nav-link" to="/register">Register</Link>
        </>)

    return (
        <header className="navbar navbar-expand navbar-dark bg-dark px-3">
            <Link className="navbar-brand mb-2" to="/">TRS-L4</Link>
            <div className="d-flex flex-row w-100 mb-1">
                <ul className="navbar-nav">
                    {navigation}
                </ul>
            </div>
            <div className="d-flex flex-row-reverse w-100 mb-1">
                <ul className="navbar-nav">
                    {session}
                </ul>
            </div>
        </header>
    )
}
