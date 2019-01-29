import { Action } from "redux";
import { UserModel } from "app/models/userModel";
import ActionTypeKeys from "./ActionsTypeKeys";

export interface IGetUserActionSuccess extends Action {
  readonly type: ActionTypeKeys.GET_CURRENT_USER_INPROGRESS;
  payload: {
    user: UserModel;
  };
}
