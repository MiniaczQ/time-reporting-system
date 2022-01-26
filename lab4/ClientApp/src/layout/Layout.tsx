import { useContext } from 'react';
import { Link, Outlet } from 'react-router-dom';
import { UserContext } from '../contexts/UserContext';
import Navbar from './Navbar';

export default function Layout() {
    const userState = useContext(UserContext);

    return (
        <div id="container" className="container-fluid vh-100 p-0 d-flex flex-column">
            <Navbar />
            <div className="flex-grow-1 d-flex" style={{ minHeight: 0 }}>
                <main className="flex-grow-1">
                    <Outlet />
                </main>
            </div>
        </div>
    );
}
