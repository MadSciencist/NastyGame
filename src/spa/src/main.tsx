import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { createBrowserHistory } from "history";
import { Router } from "react-router";
import { App } from "./app";
import configureStore from "./app/store/index";
import { initializeIcons } from "office-ui-fabric-react/lib/Icons";

// prepare store
export const history = createBrowserHistory();
const store = configureStore();

initializeIcons();

ReactDOM.render(
  <Provider store={store}>
    <Router history={history}>
      <App />
    </Router>
  </Provider>,
  document.getElementById("root")
);
