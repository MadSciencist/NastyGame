import { Store, createStore, applyMiddleware } from "redux";
import thunk from "redux-thunk";
import { composeWithDevTools } from "redux-devtools-extension";
import { logger } from "redux-logger";
import promise from "redux-promise-middleware";
import IStore from "./IStore";
import initialState from "./initialState";
import rootReducer from "../reducers/rootReducer";
import { routerMiddleware } from "connected-react-router";
import { history } from "../../main";

export default function configureStore(initialStateValue: IStore = initialState): Store<IStore> {
  return createStore(
    rootReducer as any,
    initialStateValue as any,
    composeWithDevTools(applyMiddleware(thunk, logger, promise(), routerMiddleware(history)))
  );
}
