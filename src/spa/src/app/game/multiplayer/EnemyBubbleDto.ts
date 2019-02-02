import { IPoint } from "../models/IPoint";
import Point2D from "../models/Point2D";
import OpponentBubble from "../models/OpponentBubble";

export default class EnemyBubbleDto {
  public Position: IPoint;
  public Radius: number;
  public NickName: string;
  public ConnectionId: string;

  constructor(bubble: OpponentBubble, connId: string) {
    this.Position = new Point2D(bubble.pos.cord);
    this.Radius = bubble.radius;
    this.ConnectionId = connId;
  }
}
