import BubbleBase from "./BubbleBase";
import { Vector } from "./Vector";
import GameConfig from "../GameConfig";

export default class PlayerBubble extends BubbleBase {
  public connectionId: string;

  constructor(ctx: CanvasRenderingContext2D, vect: Vector, config: GameConfig) {
    super(ctx, vect, config.initialRadius, config.registeredName, config.connectionId, config);
  }
}
