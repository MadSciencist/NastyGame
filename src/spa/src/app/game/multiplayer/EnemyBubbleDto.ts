import { IPoint } from "../models/IPoint";
import Bubble from "../Bubble";
import Point2D from "../models/Point2D";

export default class EnemyBubbleDto {
  public Position: IPoint;
  public Radius: number;
  public Name: string;

  constructor(bubble: Bubble) {
    this.Position = new Point2D(bubble.pos.cord);
    this.Radius = bubble.radius;
  }
}
