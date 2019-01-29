import * as React from "react";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import FormControl from "@material-ui/core/FormControl";
import Divider from "@material-ui/core/Divider";
import { apiPost } from "../../../API/userAPI";
import * as style from "./loginTab.css";
import { UserModel } from "../../../models/userModel";
import { connect } from "react-redux";
import { bindActionCreators, Dispatch } from "redux";
import IStore from "../../../store/IStore";
import * as Actions from "../../../actions/Actions";
import * as IActions from "../../../actions/IActions";
import { IAppProps } from "../../../models/IAppProps";

interface OwnProps {}

interface DispatchProps {
  onUserFetched?: (user: UserModel) => IActions.IGetUserActionSuccess;
}

type ComponentProps = OwnProps & IAppProps & DispatchProps;

interface LoginTabState {
  login: string;
  password: string;
}

class LoginTab extends React.Component<ComponentProps, LoginTabState> {
  constructor(props: ComponentProps) {
    super(props);

    this.state = { login: "", password: "" };
  }

  async onSubmit() {
    await apiPost("http://localhost:5000/api/v1/account/login", {
      login: this.state.login,
      password: this.state.password
    }).then((resp: any) => {
      let user: UserModel = {
        birthDate: resp.user.birthDate,
        email: resp.user.email,
        id: resp.user.userId,
        joinDate: resp.user.joinDate,
        lastName: resp.user.lastName,
        login: resp.user.login,
        name: resp.user.name,
        nickname: resp.user.name // no nickname yet
      };

      this.props.onUserFetched!(user);
    });
  }

  render() {
    return (
      <div className={style.formWrapper}>
        <FormControl>
          <TextField
            required
            id="Login"
            label="Login"
            margin="normal"
            placeholder="Type your login"
            onChange={(ev: any) => {
              this.setState({
                login: ev.target.value
              });
            }}
          />
          <TextField
            required
            label="Password"
            type="password"
            margin="normal"
            placeholder="Type your password"
            onChange={(ev: any) => {
              this.setState({
                password: ev.target.value
              });
            }}
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
    onUserFetched: bindActionCreators(Actions.getCurrentUserSuccess, dispatch)
  };
}

export default connect<IAppProps, DispatchProps, OwnProps>(
  mapStateToProps,
  mapDispatchToProps
)(LoginTab);
