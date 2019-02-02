import { Vector } from "./Vector";
import GameConfig from "../GameConfig";
import Constants from "../Constants";
import { IPoint } from "./IPoint";

export default class BubbleBase {
  public pos: Vector;
  public radius: number;
  public name: string;
  private config: GameConfig;
  public connectionId: string;

  private ctx: CanvasRenderingContext2D | null;

  constructor(
    ctx: CanvasRenderingContext2D,
    vect: Vector,
    radius: number,
    name: string,
    connId: string,
    config: GameConfig
  ) {
    this.ctx = ctx;
    this.pos = vect;
    this.radius = radius;
    this.name = name;
    this.connectionId = connId;
    this.config = config;
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

  protected constrain(input: number, min: number, max: number): number {
    return Math.max(Math.min(input, max), min);
  }

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
