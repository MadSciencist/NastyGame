import { IPoint } from "./IPoint";
import { IVector } from "./IVector";

export class Vector implements IVector {
  public cord: IPoint;
  public mag: number = 1;

  constructor(point: IPoint) {
    this.cord = { x: point.x, y: point.y };
  }

  static CreateVector(vector: IVector): IVector {
    return new Vector(vector.cord);
  }

  public applyMagnitude(magnitude: number): IVector {
    return this.normalize().multiply(magnitude);
  }

  public add(vect: IVector): IVector {
    this.cord.x = this.cord.x + vect.cord.x;
    this.cord.y = this.cord.y + vect.cord.y;

    return this;
  }

  public substract(vect: IVector): IVector {
    this.cord.x = this.cord.x - vect.cord.x;
    this.cord.y = this.cord.y - vect.cord.y;

    return this;
  }

  public multiply(scalar: number): IVector {
    this.cord.x *= scalar;
    this.cord.y *= scalar;

    return this;
  }

  public normalize(): IVector {
    const len = this.magnitude();

    if (len !== 0) {
      this.multiply(1 / len);
    }

    return this;
  }

  public distance(v: IVector): number {
    return Math.sqrt(
      (this.cord.x - v.cord.x) * (this.cord.x - v.cord.x) +
        (this.cord.y - v.cord.y) * (this.cord.y - v.cord.y)
    );
  }

  public magnitude(): number {
    const x = this.cord.x;
    const y = this.cord.y;

    return Math.sqrt(x * x + y * y);
  }
}
