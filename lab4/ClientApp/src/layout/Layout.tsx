import { Outlet } from 'react-router-dom';
import Navbar from './Navbar';

export default function Layout() {
    return (
        <div id="container" className="container-fluid vh-100 p-0 d-flex flex-column">
            <Navbar />
            <div className="flex-grow-1 d-flex" style={{ minHeight: 0 }}>
                <main className="flex-grow-1">
                    <div className='d-flex h-100'>
                        <Outlet />
                    </div>
                </main>
            </div>
        </div>
    );
}
