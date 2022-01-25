import React, { useEffect, useState } from 'react';
import { Routes, Route } from 'react-router-dom';
import logo from './logo.svg';
import './App.css';
import ApiRequest from './ApiRequest';
import User from './models/user';
import { UserContext, UserName, UserState } from './UserContext';
import Register from './elements/Register';
import Login from './elements/Login';
import Layout from './layout/Layout';

function App() {
    const [username, setUsername] = useState<UserName>(null);
    const userState: UserState = {
        state: username,
        setState: setUsername
    };

    useEffect(() => {
        ApiRequest.me().then(
            (user: User) => {
                userState.setState(user.userName);
            }
        )
    }, [])

    return (
        <UserContext.Provider value={userState} >
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route path="register" element={<Register />} />
                    <Route path="login" element={<Login />} />
                </Route>
            </Routes>
        </UserContext.Provider >
    );
}

export default App;
