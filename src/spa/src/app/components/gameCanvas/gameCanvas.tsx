import * as React from "react";
import { Game } from "../../game/Game";
import Constants from "app/game/Constants";

class GameCanvas extends React.Component {
  constructor(props: any) {
    super(props);
  }

  onClick() {
    const game = new Game();
    game.stfu();
  }

  render() {
    return (
      <div>
        <input type={"text"} id={"nick-input"} placeholder={"Enter nickname"} />
        <button onClick={this.onClick.bind(this)}>Click</button>
        <canvas id={"gameCanvas"} width={Constants.CanvasSize} height={Constants.CanvasSize} />
      </div>
    );
  }
}

export default GameCanvas;
