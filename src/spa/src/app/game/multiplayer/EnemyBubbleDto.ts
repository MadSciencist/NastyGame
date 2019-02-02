import { IPoint } from "../models/IPoint";
import Bubble from "../Bubble";
import Point2D from "../models/Point2D";

export default class EnemyBubbleDto {
  public Position: IPoint;
  public Radius: number;
  public NickName: string;
  public ConnectionId: string;

  constructor(bubble: Bubble, connId: string) {
    this.Position = new Point2D(bubble.pos.cord);
    this.Radius = bubble.radius;
    this.ConnectionId = connId;
  }
}
