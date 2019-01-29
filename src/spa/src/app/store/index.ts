import { Store, createStore } from "redux";
// import thunkMiddleware from "redux-thunk";
import { composeWithDevTools } from "redux-devtools-extension";
// import { logger } from "redux-logger";
// import promise from "redux-promise-middleware";
import IStore from "./IStore";
import initialState from "./initialState";
import rootReducer from "app/reducers/rootReducer";

export default function configureStore(initialStateValue: IStore = initialState): Store<IStore> {
  return createStore(rootReducer as any, initialStateValue as any, composeWithDevTools());
}
