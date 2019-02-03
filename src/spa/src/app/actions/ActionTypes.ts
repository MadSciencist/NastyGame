import { IGetUserActionSuccess, IPlayerInitialized } from "./Actions";

export enum ActionTypeKeys {
  USER_FETCHED = "USER_FETCHED",
  PLAYER_INITIALIZED = "PLAYER_INITIALIZED"
}

export type ActionTypes = IGetUserActionSuccess | IPlayerInitialized;
