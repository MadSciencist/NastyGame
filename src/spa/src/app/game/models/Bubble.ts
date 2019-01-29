import { IPoint } from "./IPoint";
import { Vector } from "./Vector";

export default class Bubble {
  private ctx: CanvasRenderingContext2D | null;
  private vect: Vector;
  private radius: number;

  constructor(ctx: CanvasRenderingContext2D, vect: Vector, radius: number) {
    this.ctx = ctx;
    this.vect = vect;
    this.radius = radius;
  }

  update(velocity: IPoint) {
    let velocityVector = new Vector(velocity, 1);
    velocityVector.substract(this.vect);
    velocityVector.applyMagnitude(6);
    this.vect.add(velocityVector);

    this.show();
  }

  show() {
    this.ctx!.beginPath();
    this.ctx!.arc(this.vect.cord.x, this.vect.cord.y, this.radius, 0, 2 * Math.PI);
    this.ctx!.fillStyle = "red";
    this.ctx!.fill();
    this.ctx!.strokeStyle = "black";
    this.ctx!.lineWidth = 3;
    this.ctx!.stroke();
  }
}
