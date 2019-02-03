import { UserModel } from "app/models/userModel";
import { Action } from "redux";
import { ActionTypeKeys } from "./ActionTypes";
import { IPlayerModel } from "app/models/IPlayerModel";

export interface IGetUserActionSuccess extends Action {
  readonly type: ActionTypeKeys.USER_FETCHED;
  payload: {
    user: UserModel;
  };
}

export function getCurrentUserSuccess(user: UserModel): IGetUserActionSuccess {
  return {
    type: ActionTypeKeys.USER_FETCHED,
    payload: {
      user
    }
  };
}

export interface IPlayerInitialized extends Action {
  readonly type: ActionTypeKeys.PLAYER_INITIALIZED;
  payload: {
    player: IPlayerModel;
  };
}

export function playerInitialized(player: IPlayerModel): IPlayerInitialized {
  return {
    type: ActionTypeKeys.PLAYER_INITIALIZED,
    payload: {
      player
    }
  };
}
