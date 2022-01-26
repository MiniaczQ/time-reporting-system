import React from 'react';

export type DateState = {
    state: string;
    setState: Function;
};

export const DateContext = React.createContext<DateState>({
    state: "",
    setState: () => { }
});
