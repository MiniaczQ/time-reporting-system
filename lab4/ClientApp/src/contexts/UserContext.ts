import React from 'react';

export type UserName = string | null;

export type UserState = {
    state: UserName;
    setState: Function;
};

export const UserContext = React.createContext<UserState>({
    state: null,
    setState: () => { },
});
