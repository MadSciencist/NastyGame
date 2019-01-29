import { IPoint } from "./IPoint";

export interface IVector {
  cord: IPoint;
  mag: number;
}

export class Vector implements IVector {
  public cord: IPoint;
  public mag: number;

  constructor(point: IPoint, magnitude: number) {
    this.cord = { x: point.x * magnitude, y: point.y * magnitude };
    this.mag = magnitude;
  }

  applyMagnitude(magnitude: number): Vector {
    return this.normalize().multiply(magnitude);
  }

  add(vect: Vector): Vector {
    this.cord.x = this.cord.x + vect.cord.x;
    this.cord.y = this.cord.y + vect.cord.y;

    return this;
  }

  substract(vect: Vector): Vector {
    this.cord.x = this.cord.x - vect.cord.x;
    this.cord.y = this.cord.y - vect.cord.y;

    return this;
  }

  multiply(scalar: number): Vector {
    this.cord.x *= scalar;
    this.cord.y *= scalar;

    return this;
  }

  normalize(): Vector {
    const len = this.magnitude();

    if (len !== 0) {
      this.multiply(1 / len);
    }

    return this;
  }

  magnitude(): number {
    const x = this.cord.x;
    const y = this.cord.y;

    return Math.sqrt(x * x + y * y);
  }
}
