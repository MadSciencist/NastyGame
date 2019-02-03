import { combineReducers } from "redux";
import IStore from "../store/IStore";
import { UserReducer, PlayerReducer } from "./userReducer";

const rootReducer = combineReducers<IStore>({
  user: UserReducer,
  player: PlayerReducer
});

export default rootReducer;
