import { UserModel } from "../models/userModel";
import { IPlayerModel } from "app/models/IPlayerModel";

export default interface IStore {
  user: UserModel;
  player: IPlayerModel;
}
