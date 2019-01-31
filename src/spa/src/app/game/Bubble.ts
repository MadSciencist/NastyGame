import { IPoint } from "./models/IPoint";
import { Vector } from "./models/Vector";

export default class Bubble {
  public pos: Vector;
  public radius: number;
  public name: string;

  private ctx: CanvasRenderingContext2D | null;

  constructor(ctx: CanvasRenderingContext2D, vect: Vector, radius: number, name: string) {
    this.ctx = ctx;
    this.pos = vect;
    this.radius = radius;
    this.name = name;
  }

  public update(velocity: IPoint) {
    const velocityVector = new Vector({ x: velocity.x - 200, y: velocity.y - 200 });
    velocityVector.applyMagnitude(3);
    this.pos.add(velocityVector);
  }

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
