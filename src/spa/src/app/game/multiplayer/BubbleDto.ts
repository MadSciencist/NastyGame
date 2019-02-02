import { IPoint } from "../models/IPoint";
import PlayerBubble from "../models/PlayerBubble";

export default class BubbleDto {
  public Position: IPoint;
  public Radius: number;

  constructor(bubble: PlayerBubble) {
    this.Position = bubble.pos.cord;
    this.Radius = bubble.radius;
  }
}
