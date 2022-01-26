import { AcceptedActivityAll } from "./acceptedActivity";
import { ActivityAll } from "./activity";

export type ActivitiesReport = {
    activities: ActivityAll[];
    frozen: boolean;
}

export type acceptedActivitiesReport = {
    acceptedActivities: AcceptedActivityAll[];
}
