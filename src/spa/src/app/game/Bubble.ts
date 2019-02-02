import { IPoint } from "./models/IPoint";
import { Vector } from "./models/Vector";
import Constants from "./Constants";
import GameConfig from "./GameConfig";

export default class Bubble {
  public pos: Vector;
  public radius: number;
  public name: string;
  public connectionId: string;
  private config: GameConfig;

  private ctx: CanvasRenderingContext2D | null;

  constructor(
    ctx: CanvasRenderingContext2D,
    vect: Vector,
    radius: number,
    name: string,
    connectionId: string,
    config: GameConfig
  ) {
    this.ctx = ctx;
    this.pos = vect;
    this.radius = radius;
    this.name = name;
    this.config = config;
    this.connectionId = connectionId;
  }

  public update(velocity: IPoint) {
    const velocityVector = new Vector({
      x: velocity.x - Constants.CanvasSize / 2,
      y: velocity.y - Constants.CanvasSize / 2
    });
    velocityVector.applyMagnitude(3);
    this.pos.add(velocityVector);
    this.bound();
  }

  // this function creates boundaries, so we don't exceed map size
  public bound() {
    this.pos.cord.x = this.constrain(this.pos.cord.x, 0, this.config.worldWidth);
    this.pos.cord.y = this.constrain(this.pos.cord.y, 0, this.config.worldHeight);
  }

  private constrain(input: number, min: number, max: number): number {
    return Math.max(Math.min(input, max), min);
  }

  /*
  /* This was moved to server-side to prevent cheating
  public canEat(opponent: Bubble): boolean {
    const distance = this.pos.distance(opponent.pos);

    if (distance < this.radius + opponent.radius) {
      const totalArea =
        Math.PI * this.radius * this.radius + Math.PI * opponent.radius * opponent.radius;
      this.radius = Math.sqrt(totalArea / Math.PI);
      return true;
    } else {
      return false;
    }
  }
  */

  public show() {
    this.ctx!.beginPath();
    this.ctx!.arc(this.pos.cord.x, this.pos.cord.y, this.radius, 0, 2 * Math.PI);
    this.ctx!.closePath();
    this.ctx!.fillStyle = "red";
    this.ctx!.fill();
    this.ctx!.strokeStyle = "black";
    this.ctx!.lineWidth = 1;
    this.ctx!.stroke();
  }
}
