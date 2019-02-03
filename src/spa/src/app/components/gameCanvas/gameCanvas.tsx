import * as React from "react";
import { Game } from "../../game/Game";
import Constants from "../../game/Constants";
import { RouteComponentProps } from "react-router-dom";
import { IAppProps } from "../../models/IAppProps";
import { connect } from "react-redux";
import { Dispatch } from "redux";
import IStore from "app/store/IStore";

interface OwnProps {}

interface DispatchProps {}

type ComponentProps = OwnProps & IAppProps & DispatchProps & RouteComponentProps<{}>;

class GameCanvas extends React.Component<ComponentProps, {}> {
  constructor(props: ComponentProps) {
    super(props);
  }

  componentDidMount() {
    const game = new Game(this.props);
    game.startNew();
  }

  render() {
    return <canvas id={"gameCanvas"} width={Constants.CanvasSize} height={Constants.CanvasSize} />;
  }
}

function mapStateToProps(store: IStore): IAppProps {
  return {
    store: store
  };
}

function mapDispatchToProps(dispatch: Dispatch<IStore>): DispatchProps {
  return {};
}

export default connect<IAppProps, DispatchProps, OwnProps>(
  mapStateToProps,
  mapDispatchToProps
)(GameCanvas);
