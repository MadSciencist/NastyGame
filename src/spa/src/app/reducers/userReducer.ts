import { Reducer } from "redux";
import initialState from "../store/initialState";
import { UserModel } from "app/models";
import ActionTypeKeys from "app/actions/ActionsTypeKeys";
import ActionTypes from "app/actions/ActionTypes";

export const userReducer: Reducer<UserModel> = (
  state: UserModel = initialState.user,
  action: ActionTypes
): UserModel => {
  switch (action.type) {
    case ActionTypeKeys.GET_CURRENT_USER_INPROGRESS:
      return {
        ...action.payload.user
      };
    default:
      return state;
  }
};
