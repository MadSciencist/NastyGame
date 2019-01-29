import * as React from "react";
import { RouteComponentProps, withRouter } from "react-router-dom";

import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import FormControl from "@material-ui/core/FormControl";

import * as style from "./playAnon.css";

interface PlayAnonProps {}

interface PlayAnonState {}

class PlayAnon extends React.Component<RouteComponentProps<{}> & PlayAnonProps, PlayAnonState> {
  constructor(props: RouteComponentProps<{}> & PlayAnonProps) {
    super(props);
  }

  onPlayClick() {
    this.props.history.push("/play");
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
                login: ev.target.value
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

export default withRouter(PlayAnon);
