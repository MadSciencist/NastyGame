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
        <canvas id={"gameCanvas"} width={Constants.CanvasSize} height={Constants.CanvasSize} />
        <input type={"text"} id={"nick-input"} placeholder={"Enter nickname"} />
        <button onClick={this.onClick.bind(this)}>Click</button>
      </div>
    );
  }
}

export default GameCanvas;
