import * as React from "react";
import TextField from "@material-ui/core/TextField";
import { MessageBar, MessageBarType } from "office-ui-fabric-react/lib/MessageBar";
import Button from "@material-ui/core/Button";
import FormControl from "@material-ui/core/FormControl";
import Divider from "@material-ui/core/Divider";
import * as style from "./loginTab.css";
import { UserModel } from "../../../models/userModel";
import { connect } from "react-redux";
import { bindActionCreators, Dispatch } from "redux";
import IStore from "../../../store/IStore";
import * as Actions from "../../../actions/Actions";
import { IAppProps } from "../../../models/IAppProps";
import Const from "../../../utils/Constants";
import ApiService from "app/services/ApiService";
import { push } from "react-router-redux";
import { RouteComponentProps } from "react-router-dom";

interface OwnProps {}

interface DispatchProps {
  onUserFetched?: (user: UserModel) => Actions.IGetUserActionSuccess;
  onChangePath?: (key: string) => void;
}

type ComponentProps = OwnProps & IAppProps & DispatchProps & RouteComponentProps<{}>;

interface LoginTabState {
  login: string;
  password: string;
  isAuthSuccess: boolean;
  showLoginError: boolean;
  showPasswordError: boolean;
  loginErrorText: string;
  passwordErrorText: string;
}

class LoginTab extends React.Component<ComponentProps, LoginTabState> {
  constructor(props: ComponentProps) {
    super(props);

    this.state = {
      login: "",
      password: "",
      isAuthSuccess: true,
      showLoginError: false,
      showPasswordError: false,
      loginErrorText: "",
      passwordErrorText: ""
    };
  }

  async onSubmit(e: any) {
    e.preventDefault();

    await ApiService.post(Const.Endpoints.login, {
      login: this.state.login,
      password: this.state.password
    })
      .then((resp: any) => {
        let user: UserModel = {
          birthDate: resp.user.birthDate,
          email: resp.user.email,
          id: resp.user.userId,
          joinDate: resp.user.joinDate,
          lastName: resp.user.lastName,
          login: resp.user.login,
          name: resp.user.name,
          token: resp.access.token,
          isAuth: true
        };

        this.setState({ isAuthSuccess: true }); // update component state
        this.props.onUserFetched!(user); // update redux store
        this.props.onChangePath("/play");
      })
      .catch(() => {
        this.setState({ isAuthSuccess: false });
      });
  }

  handleLoginChange(event: any): void {
    // simple validation
    const validationResult = this.validate(event.target.value);

    this.setState({
      login: event.target.value,
      showLoginError: validationResult,
      loginErrorText: validationResult ? "Login should be at least 5 characters long." : ""
    });
  }

  handlePasswordChange(event: any): void {
    // simple validation
    const validationResult = this.validate(event.target.value);

    this.setState({
      password: event.target.value,
      showPasswordError: validationResult,
      passwordErrorText: validationResult ? "Login should be at least 5 characters long." : ""
    });
  }

  private validate(login: string): boolean {
    return login.length < 5;
  }

  render() {
    return (
      <div className={style.formWrapper}>
        {!this.state.isAuthSuccess && (
          <MessageBar messageBarType={MessageBarType.error}>
            Someting went wrong. Please check your credentials.
          </MessageBar>
        )}
        <FormControl>
          <TextField
            required
            id="Login"
            label="Login"
            margin="normal"
            placeholder="Type your login"
            value={this.state.login}
            onChange={this.handleLoginChange.bind(this)}
            error={this.state.showLoginError}
            helperText={this.state.loginErrorText}
          />
          <TextField
            required
            label="Password"
            type="password"
            margin="normal"
            placeholder="Type your password"
            value={this.state.password}
            onChange={this.handlePasswordChange.bind(this)}
            error={this.state.showPasswordError}
            helperText={this.state.passwordErrorText}
          />
        </FormControl>
        <Divider light className={style.divider} />
        <div className={style.submitWrapper}>
          <Button variant="contained" color="primary" onClick={this.onSubmit.bind(this)}>
            Login
          </Button>
        </div>
      </div>
    );
  }
}

function mapStateToProps(store: IStore): IAppProps {
  return {
    store: store
  };
}

function mapDispatchToProps(dispatch: Dispatch<IStore>): DispatchProps {
  return {
    onUserFetched: bindActionCreators(Actions.getCurrentUserSuccess, dispatch),
    onChangePath: (key: string) => {
      dispatch(push(key));
    }
  };
}

export default connect<IAppProps, DispatchProps, OwnProps>(
  mapStateToProps,
  mapDispatchToProps
)(LoginTab);
