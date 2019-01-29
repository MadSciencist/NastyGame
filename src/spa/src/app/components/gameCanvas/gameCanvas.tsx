import * as React from "react";
import { Game } from "../../game/game";
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
        <button onClick={this.onClick.bind(this)}>Clik</button>
        <canvas id={"gameCanvas"} width={Constants.CANVAS_SIZE} height={Constants.CANVAS_SIZE} />
      </div>
    );
  }
}

export default GameCanvas;
