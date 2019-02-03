import { Reducer } from "redux";
import initialState from "../store/initialState";
import { UserModel } from "../models";
import { ActionTypes, ActionTypeKeys } from "../actions/ActionTypes";
import { IPlayerModel } from "app/models/IPlayerModel";

export const UserReducer: Reducer<UserModel> = (
  state: UserModel = initialState.user,
  action: ActionTypes
): UserModel => {
  switch (action.type) {
    case ActionTypeKeys.USER_FETCHED:
      return {
        ...action.payload.user
      };
    default:
      return state;
  }
};

export const PlayerReducer: Reducer<IPlayerModel> = (
  state: IPlayerModel = initialState.player,
  action: ActionTypes
): IPlayerModel => {
  switch (action.type) {
    case ActionTypeKeys.PLAYER_INITIALIZED: {
      return {
        ...action.payload.player
      };
    }

    default:
      return state;
  }
};
