import { IPoint } from "../models/IPoint";
import Bubble from "../Bubble";

export default class BubbleDto {
  public Position: IPoint;
  public Radius: number;

  constructor(bubble: Bubble) {
    this.Position = bubble.pos.cord;
    this.Radius = bubble.radius;
  }
}
