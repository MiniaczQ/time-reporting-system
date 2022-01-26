import { useContext } from 'react';
import { Link, Outlet } from 'react-router-dom';
import { UserContext } from '../contexts/UserContext';
import Navbar from './Navbar';

export default function Layout() {
    const userState = useContext(UserContext);

    const options = userState.state !== null ? (
        <>
            <nav className="bg-secondary" style={{ minWidth: '200px', maxWidth: '200px' }}>
                <ul className="nav nav-pills flex-column mb-auto">
                    <li className="nav-item">
                        <Link className="nav-link link-light" to="/activities">
                            Activities
                        </Link>
                    </li>
                </ul>
            </nav>
        </>
    ) : (
        <></>
    );

    return (
        <div id="container" className="container-fluid vh-100 p-0 d-flex flex-column">
            <Navbar />
            <div className="flex-grow-1 d-flex" style={{ minHeight: 0 }}>
                {options}
                <main className="flex-grow-1">
                    <Outlet />
                </main>
            </div>
        </div>
    );
}
