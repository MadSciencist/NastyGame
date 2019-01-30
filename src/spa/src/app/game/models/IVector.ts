import { IPoint } from "./IPoint";
import { Vector } from "./Vector";

export interface IVector {
  cord: IPoint;
  mag: number;

  applyMagnitude(magnitude: number): Vector;
  add(vect: IVector): IVector;
  substract(vect: Vector): IVector;
  multiply(scalar: number): IVector;
  normalize(): IVector;
  distance(v: IVector): number;
  magnitude(): number;
}
