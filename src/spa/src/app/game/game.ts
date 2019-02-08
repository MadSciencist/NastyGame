import { Mouse } from "./controls/Mouse";
import { Vector } from "./models/Vector";
import { IPoint } from "./models/IPoint";
import { IVector } from "./models/IVector";
import MultiplayerService from "./multiplayer/MultiplayerService";
import EnemyBubbleDto from "./multiplayer/EnemyBubbleDto";
import Constants from "./Constants";
import GameConfig from "./GameConfig";
import OpponentBubble from "./models/OpponentBubble";
import PlayerBubble from "./models/PlayerBubble";
import BubbleBase from "./models/BubbleBase";
import { IAppProps } from "app/models/IAppProps";

export class Game {
  private reduxStore: IAppProps;
  private multiplayer: MultiplayerService;
  private canvas: HTMLCanvasElement;
  private ctx: CanvasRenderingContext2D | null;
  private mouse: Mouse;
  private mousePos: IPoint = { x: Constants.CanvasSize / 2, y: Constants.CanvasSize / 2 };
  private bubble: PlayerBubble;
  private serverBubbles: Array<BubbleBase> = [];
  private prevPos: IVector;
  private gameConfig: GameConfig;

  constructor(reduxStore: IAppProps) {
    this.reduxStore = reduxStore;
    this.canvas = <HTMLCanvasElement>document.getElementById("gameCanvas");
    this.ctx = this.canvas.getContext("2d");
  }

  public startNew() {
    let nickname = this.reduxStore.store.player.nickname;
    let useAuthentication = this.reduxStore.store.user.isAuth;
    let token = this.reduxStore.store.user.token;

    if (nickname === "") {
      location.href = "/";
    }

    this.multiplayer = new MultiplayerService(nickname, useAuthentication, token);
    this.multiplayer.startConnection();
    this.multiplayer.onStarted((cfg: GameConfig) => {
      this.gameConfig = cfg;

      const initalPos: Vector = new Vector({
        x: Constants.CanvasSize / 2,
        y: Constants.CanvasSize / 2
      });

      this.prevPos = Vector.CreateVector(initalPos);
      this.bubble = new PlayerBubble(this.ctx!, initalPos, this.gameConfig);

      console.warn(this.reduxStore);
      setInterval(this.loop.bind(this), 25);
    });

    this.multiplayer.onLost(() => {
      alert("You lost");
      location.reload();
    });

    this.multiplayer.onEnemiesUpdated((enemies: Array<EnemyBubbleDto>) => {
      this.serverBubbles = enemies.map(
        (enemy: EnemyBubbleDto): OpponentBubble => {
          return new OpponentBubble(
            this.ctx!,
            new Vector(enemy.Position),
            enemy.Radius,
            enemy.NickName,
            enemy.ConnectionId,
            this.gameConfig
          );
        }
      );
    });

    this.mouse = new Mouse(this.canvas);
    window.addEventListener("mousemove", (e: MouseEvent) => {
      this.mousePos = this.mouse.getPosition(e);
    });
  }

  private drawStatistics() {
    // TODO this
    this.ctx!.font = "10px Arial";
    this.ctx!.fillStyle = "white";
    this.ctx!.fillText(this.gameConfig.connectionId, 10, 50);
  }

  private loop() {
    this.fillCanvas();
    this.drawBoard();

    // draw myself to get smooth gameplay
    this.bubble.update(this.mousePos);
    this.bubble.show();
    this.multiplayer.updateMyPosition(this.bubble);

    // draw oponents / NPCs
    for (let i = this.serverBubbles.length - 1; i >= 0; i--) {
      if (this.serverBubbles[i].connectionId === this.gameConfig.connectionId) {
        this.bubble.radius = this.serverBubbles[i].radius;
        continue; // dont draw myself, just update radius (to prevent cheating)
      }

      this.serverBubbles[i].show();
    }

    const dx = this.bubble.pos.cord.x - this.prevPos.cord.x;
    const dy = this.bubble.pos.cord.y - this.prevPos.cord.y;
    this.ctx!.translate(-dx, -dy);

    this.drawStatistics();

    this.prevPos.cord.x = this.bubble.pos.cord.x;
    this.prevPos.cord.y = this.bubble.pos.cord.y;
  }

  private fillCanvas() {
    const margin = 100; // fill 100 more in each direction
    this.ctx!.fillStyle = "gray";
    this.ctx!.fillRect(
      -margin,
      -margin,
      2 * margin + this.gameConfig.worldWidth,
      2 * margin + this.gameConfig.worldHeight
    );
  }

  private drawBoard() {
    let bw = this.gameConfig.worldWidth;
    let bh = this.gameConfig.worldHeight;
    let p = 20;

    for (let x = 0; x <= bw; x += 40) {
      this.ctx!.moveTo(0.5 + x + p, p);
      this.ctx!.lineTo(0.5 + x + p, bh + p);
    }

    for (var x = 0; x <= bh; x += 40) {
      this.ctx!.moveTo(p, 0.5 + x + p);
      this.ctx!.lineTo(bw + p, 0.5 + x + p);
    }

    this.ctx!.strokeStyle = "yellow";
    this.ctx!.stroke();
  }

  stfu() {}
}
