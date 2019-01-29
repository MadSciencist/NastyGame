import { UserModel } from "app/models/userModel";
import { IGetUserActionSuccess } from "./IActions";
import ActionTypeKeys from "./ActionsTypeKeys";

export function getCurrentUserSuccess(user: UserModel): IGetUserActionSuccess {
  return {
    type: ActionTypeKeys.GET_CURRENT_USER_INPROGRESS,
    payload: {
      user
    }
  };
}
