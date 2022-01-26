export type ActivityAll = {
    activityPid: number;
    projectCode: string;
    projectName: string;
    date: string;
    subprojectCode: string;
    time: number;
    description: string;
};

export type ActivityCreate = {
    projectCode: string;
    date: string;
    subprojectCode: string;
    time: number;
    description: string;
};
