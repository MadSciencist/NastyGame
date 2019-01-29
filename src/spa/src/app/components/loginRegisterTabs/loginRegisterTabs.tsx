import * as React from "react";
import { withRouter, RouteComponentProps } from "react-router-dom";

import Paper from "@material-ui/core/Paper";
import Tabs from "@material-ui/core/Tabs";
import Tab from "@material-ui/core/Tab";
import Divider from "@material-ui/core/Divider";

import LoginTab from "./login/loginTab";
import RegisterTab from "./register/registerTab";
import PlayAnon from "./playAnon/playAnon";

import * as style from "./loginRegisterTabs.css";

interface ILoginRegisterTabsState {
  activeCard: LoginRegisterTabsRole;
}

interface ILoginRegisterTabsProps {
  activeCard?: LoginRegisterTabsRole;
}

export enum LoginRegisterTabsRole {
  Play = 0,
  Login = 1,
  Register = 2
}

class LoginRegisterTabs extends React.Component<
  RouteComponentProps<{}> & ILoginRegisterTabsProps,
  ILoginRegisterTabsState
> {
  constructor(props: RouteComponentProps<{}> & ILoginRegisterTabsProps) {
    super(props);

    this.state = { activeCard: this.props.activeCard || 0 };
  }

  handleChange = (event: any, value: any) => {
    if (value === LoginRegisterTabsRole.Play) {
      this.props.history.push("/");
    } else if (value === LoginRegisterTabsRole.Login) {
      this.props.history.push("/login");
    } else if (value === LoginRegisterTabsRole.Register) {
      this.props.history.push("/register");
    }

    this.setState({ activeCard: value });
  }

  render() {
    const { activeCard } = this.state;

    return (
      <div className={style.loginRegisterTabs}>
        <Paper>
          <Tabs
            value={activeCard}
            onChange={this.handleChange.bind(this)}
            indicatorColor="primary"
            textColor="primary"
            centered
          >
            <Tab label="Play!" />
            <Tab label="Login" />
            <Tab label="Register" />
          </Tabs>
          <Divider light />
          {activeCard === LoginRegisterTabsRole.Play && <PlayAnon />}
          {activeCard === LoginRegisterTabsRole.Login && <LoginTab />}
          {activeCard === LoginRegisterTabsRole.Register && <RegisterTab />}
        </Paper>
      </div>
    );
  }
}

export default withRouter(LoginRegisterTabs);
