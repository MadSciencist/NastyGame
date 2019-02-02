import { Mouse } from "./controls/Mouse";
import { Vector } from "./models/Vector";
import { IPoint } from "./models/IPoint";
import { IVector } from "./models/IVector";
import Bubble from "./Bubble";
import MultiplayerService from "./multiplayer/MultiplayerService";
import EnemyBubbleDto from "./multiplayer/EnemyBubbleDto";
import { GameConfigDto } from "./multiplayer/GameConfigDto";
import Constants from "./Constants";

export class Game {
  private multiplayer: MultiplayerService;
  private canvas: HTMLCanvasElement;
  private ctx: CanvasRenderingContext2D | null;
  private mouse: Mouse;
  private mousePos: IPoint = { x: Constants.CanvasSize / 2, y: Constants.CanvasSize / 2 };
  private bubble: Bubble;
  private serverBubbles: Array<Bubble> = [];
  private prevPos: IVector;
  private gameConfig: GameConfigDto;

  constructor() {
    this.canvas = <HTMLCanvasElement>document.getElementById("gameCanvas");
    this.ctx = this.canvas.getContext("2d");

    this.multiplayer = new MultiplayerService();
    this.multiplayer.onStarted((gameConfig: GameConfigDto) => {
      this.gameConfig = gameConfig;

      const initalPos: Vector = new Vector({
        x: Constants.CanvasSize / 2,
        y: Constants.CanvasSize / 2
      });

      this.prevPos = Vector.CreateVector(initalPos);
      this.bubble = new Bubble(
        this.ctx!,
        initalPos,
        gameConfig.InitialRadius,
        gameConfig.RegisteredName,
        gameConfig
      );

      setInterval(this.loop.bind(this), 25);
    });

    this.multiplayer.onEnemiesUpdated((enemies: Array<EnemyBubbleDto>) => {
      this.serverBubbles = [];
      this.serverBubbles = enemies.map(
        (enemy: EnemyBubbleDto): Bubble => {
          return new Bubble(
            this.ctx!,
            new Vector(enemy.Position),
            enemy.Radius,
            enemy.NickName,
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

  private loop() {
    this.fillCanvas();
    this.drawBoard();

    // draw myself to get smooth gameplay
    this.bubble.update(this.mousePos);
    this.bubble.show();
    this.multiplayer.updateMyPosition(this.bubble);

    console.log(this.bubble.pos.cord);

    // draw oponents / NPCs
    for (let i = this.serverBubbles.length - 1; i >= 0; i--) {
      const myName = (<HTMLInputElement>document.getElementById("nick-input")).value as string;

      // dont draw myself, just update radius (to prevent cheating)
      if (this.serverBubbles[i].name === myName) {
        this.bubble.radius = this.serverBubbles[i].radius;
        continue;
      }

      this.serverBubbles[i].show();
    }

    const dx = this.bubble.pos.cord.x - this.prevPos.cord.x;
    const dy = this.bubble.pos.cord.y - this.prevPos.cord.y;
    this.ctx!.translate(-dx, -dy);

    this.prevPos.cord.x = this.bubble.pos.cord.x;
    this.prevPos.cord.y = this.bubble.pos.cord.y;
  }

  private fillCanvas() {
    const margin = 100; // fill 100 more in each direction
    this.ctx!.fillStyle = "gray";
    this.ctx!.fillRect(
      -margin,
      -margin,
      2 * margin + this.gameConfig.WorldWidth,
      2 * margin + this.gameConfig.WorldHeight
    );
  }

  private drawBoard() {
    let bw = this.gameConfig.WorldWidth;
    let bh = this.gameConfig.WorldHeight;
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
