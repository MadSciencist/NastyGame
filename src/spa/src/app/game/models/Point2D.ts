import { IPoint } from "./IPoint";

export default class Point2D implements IPoint {
  public x: number;
  public y: number;

  constructor(point: IPoint) {
    this.x = point.x;
    this.y = point.y;
  }
}
