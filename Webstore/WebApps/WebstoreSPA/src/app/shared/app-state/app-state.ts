import { Role } from "./role";

export interface IAppState {
    accessToken?: string;
    refreshToken?: string;
    username?: string;
    roles?: Role | Role[];

    hasRole(role: Role): boolean;
    clone(): IAppState;
}

export class AppState implements IAppState {
    public accessToken?: string;
    public refreshToken?: string;
    public username?: string;
    public roles?: Role | Role[];

    public constructor();
    public constructor(accessToken?: string, refreshToken?: string, username?: string, roles?: Role | Role[]);

    public constructor(...args: any[]) {
        if (args.length === 0) {
            return;
        }
        else if (args.length === 4) {
            this.accessToken = args[0];
            this.refreshToken = args[1];
            this.username = args[2];
            this.roles = args[3];
        }
    }

    public hasRole(role: Role): boolean {
        if (!this.roles) {
            return false;
        }
        if (typeof this.roles === 'string') {
            return this.roles === role;
        }
        return this.roles.find((registeredRole: Role) => registeredRole === role) !== undefined;
    }

    public clone(): IAppState {
        const newState = new AppState();
        Object.assign(newState, this);
        return newState;
    }
}