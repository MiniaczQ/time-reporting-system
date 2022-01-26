import { useEffect, useState } from 'react';
import { Routes, Route } from 'react-router-dom';
import ApiRequest from './ApiRequest';
import { UserAll } from './models/user';
import { UserContext, UserName, UserState } from './contexts/UserContext';
import Register from './elements/Register';
import Login from './elements/Login';
import Layout from './layout/Layout';
import Activities from './elements/Activities';

function App() {
    const [username, setUsername] = useState<UserName>(null);
    const userState: UserState = {
        state: username,
        setState: setUsername
    };

    useEffect(() => {
        ApiRequest.me().then(
            (user: UserAll) => {
                userState.setState(user?.userName);
            }
        );
    }, [])

    return (
        <UserContext.Provider value={userState}>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route path="register" element={<Register />} />
                    <Route path="login" element={<Login />} />
                    <Route path="activities" element={<Activities />} />
                </Route>
            </Routes>
        </UserContext.Provider >
    );
}

export default App;
