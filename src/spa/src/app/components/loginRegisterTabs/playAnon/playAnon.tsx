import * as React from "react";
import { bindActionCreators, Dispatch } from "redux";
import { connect } from "react-redux";
import { push } from "react-router-redux";
import { RouteComponentProps } from "react-router-dom";
import * as Actions from "../../../actions/Actions";

import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import FormControl from "@material-ui/core/FormControl";

import * as style from "./playAnon.css";
import { IAppProps } from "../../../models/IAppProps";
import { IPlayerModel } from "app/models/IPlayerModel";
import IStore from "../../../store/IStore";

interface PlayAnonState {
  nickname: string;
}

interface OwnProps {}

interface DispatchProps {
  onPlayerInfoChanged?: (user: IPlayerModel) => Actions.IPlayerInitialized;
  onChangePath?: (key: string) => void;
}

type ComponentProps = OwnProps & IAppProps & DispatchProps & RouteComponentProps<{}>;

class PlayAnon extends React.Component<ComponentProps, PlayAnonState> {
  constructor(props: ComponentProps) {
    super(props);
    this.state = {
      nickname: ""
    };
  }

  onPlayClick() {
    if (this.state.nickname === "") {
      alert("Please enter username");
    } else {
      this.props.onPlayerInfoChanged({
        nickname: this.state.nickname
      });
      this.props.onChangePath("/game");
    }
  }

  render() {
    return (
      <div className={style.formWrapper}>
        <FormControl>
          <TextField
            required
            label="Nickname"
            margin="normal"
            placeholder="Type your nick"
            onChange={(ev: any) => {
              this.setState({
                nickname: ev.target.value
              });
            }}
          />
        </FormControl>
        <div className={style.submitWrapper}>
          <Button variant="contained" color="primary" onClick={this.onPlayClick.bind(this)}>
            Play
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
    onPlayerInfoChanged: bindActionCreators(Actions.playerInitialized, dispatch),
    onChangePath: (key: string) => {
      dispatch(push(key));
    }
  };
}

export default connect<IAppProps, DispatchProps, OwnProps>(
  mapStateToProps,
  mapDispatchToProps
)(PlayAnon);
