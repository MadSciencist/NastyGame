import Bubble from "./models/Bubble";
import { Mouse } from "./controls/Mouse";
import { Vector } from "./models/Vector";
import { IPoint } from "./models/IPoint";

export class Game {
  private canvas: HTMLCanvasElement;
  private ctx: CanvasRenderingContext2D | null;
  private mouse: Mouse;
  private mousePos: IPoint;

  private bubble: Bubble;

  constructor() {
    this.canvas = <HTMLCanvasElement>document.getElementById("gameCanvas");
    this.ctx = this.canvas.getContext("2d");

    this.mouse = new Mouse(this.canvas);
    window.addEventListener("mousemove", (e) => {
      this.mousePos = this.mouse.getPosition(e);
    });

    let a: Vector = new Vector({ x: 50, y: 50 }, 1);
    this.bubble = new Bubble(this.ctx!, a, 30);

    setInterval(() => {
      this.fillCanvas();
      this.bubble.update(this.mousePos);
    }, 25);
  }

  protected fillCanvas() {
    this.ctx!.fillStyle = "white";
    this.ctx!.fillRect(0, 0, 400, 400);
  }

  stfu() {}
}
