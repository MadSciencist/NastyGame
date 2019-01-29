import { Key } from "ts-keycode-enum";
import MultiplayerService from "./MultiplayerService";
import { IPoint } from "./IPoint";
import Constants from "./Constants";

export class Snake {
  private multiplayer: MultiplayerService;
  private canvas: HTMLCanvasElement;
  private ctx: CanvasRenderingContext2D | null;

  private readonly gridSize: number;

  private position: IPoint;
  private velocity: IPoint;
  private treasure: IPoint;
  private oponent: Array<IPoint>;

  private trail: Array<IPoint>;
  private tail: number;

  constructor() {
    this.canvas = <HTMLCanvasElement>document.getElementById("gameCanvas");
    this.ctx = this.canvas.getContext("2d");

    document.addEventListener("keydown", this.keyDown.bind(this));

    this.multiplayer = new MultiplayerService();

    this.multiplayer.onUpdatedEnemyPosition((name: string, position: Array<IPoint>) => {
      this.oponent = position;
    });

    this.multiplayer.onTrasureSpawned((trasurePosition: IPoint) => {
      this.treasure = trasurePosition;
    });

    this.oponent = [];
    this.position = { X: 15, Y: 15 };
    this.velocity = { X: 0, Y: 0 };
    this.treasure = { X: 10, Y: 10 };

    this.gridSize = Constants.GRID_ELEMENT_SIZE;
    this.tail = Constants.SNAKE.DEFAULT_LENTGH;
    this.trail = [];
  }

  public start(): void {
    setInterval(this.loop.bind(this), 1000 / 15);
  }

  protected loop(): void {
    this.position.X += this.velocity.X;
    this.position.Y += this.velocity.Y;

    // going through the walls
    if (this.position.X < 0) {
      this.position.X = this.gridSize - 1;
    }
    if (this.position.X > this.gridSize - 1) {
      this.position.X = 0;
    }
    if (this.position.Y < 0) {
      this.position.Y = this.gridSize - 1;
    }
    if (this.position.Y > this.gridSize - 1) {
      this.position.Y = 0;
    }

    this.fillCanvas();

    this.ctx!.fillStyle = Constants.SNAKE.SNAKE_COLOR;
    for (let i = 0; i < this.trail.length; i++) {
      this.ctx!.fillRect(
        this.trail[i].X * this.gridSize,
        this.trail[i].Y * this.gridSize,
        this.gridSize - 2,
        this.gridSize - 2
      );

      // suicide by knotting
      if (this.trail[i].X == this.position.X && this.trail[i].Y == this.position.Y) {
        this.tail = Constants.SNAKE.DEFAULT_LENTGH;
      }
    }

    this.trail.push({
      X: this.position.X,
      Y: this.position.Y
    });

    while (this.trail.length > this.tail) {
      this.trail.shift();
    }

    // picked trasure
    if (this.treasure.X === this.position.X && this.treasure.Y === this.position.Y) {
      this.tail++;
      this.multiplayer.spawnTrasure();
      // this.treasure = this.spawnTrasure();
    }

    this.fillTrasure();
    this.fillOponent();

    this.multiplayer.updateMyPosition(this.trail);
  }

  // SINGLE PLAYER
  // protected spawnTrasure(position: IPoint): IPoint {
  //   return {
  //     X: Math.floor(Math.random() * this.gridSize),
  //     Y: Math.floor(Math.random() * this.gridSize)
  //   };
  // }

  protected fillCanvas() {
    this.ctx!.fillStyle = Constants.SNAKE.BACKGROUND_COLOR;
    this.ctx!.fillRect(0, 0, Constants.CANVAS_SIZE, Constants.CANVAS_SIZE);
  }

  protected fillTrasure() {
    this.ctx!.fillStyle = Constants.SNAKE.TRASURE_COLOR;
    this.ctx!.fillRect(
      this.treasure.X * this.gridSize,
      this.treasure.Y * this.gridSize,
      this.gridSize - 2,
      this.gridSize - 2
    );
  }

  protected fillOponent() {
    this.ctx!.fillStyle = Constants.SNAKE.OPONENT_COLOR;
    for (let i = 0; i < this.trail.length; i++) {
      if (this.oponent[i] == undefined) {
        break;
      }

      this.ctx!.fillRect(
        this.oponent[i].X * this.gridSize,
        this.oponent[i].Y * this.gridSize,
        this.gridSize - 2,
        this.gridSize - 2
      );
    }
  }

  protected keyDown(event: KeyboardEvent): void {
    switch (event.keyCode) {
      case Key.LeftArrow:
        this.velocity.X = -1;
        this.velocity.Y = 0;
        break;

      case Key.UpArrow:
        this.velocity.X = 0;
        this.velocity.Y = -1;
        break;

      case Key.RightArrow:
        this.velocity.X = 1;
        this.velocity.Y = 0;
        break;

      case Key.DownArrow:
        this.velocity.X = 0;
        this.velocity.Y = 1;
        break;

      default:
        break;
    }
  }
}
