import { useContext } from 'react';
import { Link } from 'react-router-dom';
import ApiRequest from '../ApiRequest';
import { UserContext } from '../contexts/UserContext';

export default function Navbar() {
    const userState = useContext(UserContext);

    const logout = () => {
        ApiRequest.logout().then((_) => userState.setState(null));
    }

    const navbar = userState.state ? (<>
        <li className="nav-item">
            <Link className="nav-link" to="#">Logged in as {userState.state}.</Link>
        </li>
        <li className="nav-item">
            <Link className="nav-link" to="/" onClick={() => logout()}>Logout</Link>
        </li>
    </>) : (<>
        <li className="nav-item">
            <Link className="nav-link" to="/login">Log in</Link>
        </li>
        <li className="nav-item">
            <Link className="nav-link" to="/register">Register</Link>
        </li>
    </>)

    return (
        <header className="navbar navbar-expand navbar-dark bg-dark px-3">
            <Link className="navbar-brand mb-2" to="/">Main page</Link>
            <div className="d-flex flex-row-reverse w-100 mb-2">
                <ul className="navbar-nav">
                    {navbar}
                </ul>
            </div>
        </header>
    )
}
