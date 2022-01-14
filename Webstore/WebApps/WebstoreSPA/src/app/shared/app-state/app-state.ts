import { Role } from './role';

export interface IAppState {
  accessToken?: string;
  refreshToken?: string;
  username?: string;
  email?: string;
  roles?: Role | Role[];
  firstName?: string;
  lastName?: string;
  userId?: string;

  hasRole(role: Role): boolean;
  clone(): IAppState;
}

export class AppState implements IAppState {
  public accessToken?: string;
  public refreshToken?: string;
  public username?: string;
  public email?: string;
  public roles?: Role | Role[];
  public firstName?: string;
  public lastName?: string;
  public userId?: string;

  public constructor();
  public constructor(
    accessToken?: string,
    refreshToken?: string,
    username?: string,
    email?: string,
    roles?: Role | Role[],
    firstName?: string,
    lastName?: string,
    userId?: string
  );

  public constructor(...args: any[]) {
    if (args.length === 0) {
      return;
    } else if (args.length === 4) {
      this.accessToken = args[0];
      this.refreshToken = args[1];
      this.username = args[2];
      this.email = args[3];
      this.roles = args[4];
      this.firstName = args[5];
      this.lastName = args[6];
      this.userId = args[7];
    }
  }

  public hasRole(role: Role): boolean {
    if (!this.roles) {
      return false;
    }
    if (typeof this.roles === 'string') {
      return this.roles === role;
    }
    return (
      this.roles.find((registeredRole: Role) => registeredRole === role) !==
      undefined
    );
  }

  public clone(): IAppState {
    const newState = new AppState();
    Object.assign(newState, this);
    return newState;
  }
}
