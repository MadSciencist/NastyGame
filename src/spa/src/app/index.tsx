import * as React from "react";
import { Route, Switch } from "react-router";
import { hot } from "react-hot-loader";
import GameCanvas from "./components/gameCanvas/gameCanvas";
import LoginRegisterTabs, {
  LoginRegisterTabsRole
} from "./components/loginRegisterTabs/loginRegisterTabs";

export const App = hot(module)(() => (
  <Switch>
    <Route
      path="/play"
      render={(props) => <LoginRegisterTabs {...props} activeCard={LoginRegisterTabsRole.Play} />}
    />

    <Route
      path="/login"
      render={(props) => <LoginRegisterTabs {...props} activeCard={LoginRegisterTabsRole.Login} />}
    />
    <Route
      path="/register"
      render={(props) => (
        <LoginRegisterTabs {...props} activeCard={LoginRegisterTabsRole.Register} />
      )}
    />

    <Route path="/game" component={GameCanvas} />

    <Route
      path="/"
      render={(props) => <LoginRegisterTabs {...props} activeCard={LoginRegisterTabsRole.Play} />}
    />
  </Switch>
));
