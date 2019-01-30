import { Mouse } from "./controls/Mouse";
import { Vector } from "./models/Vector";
import { IPoint } from "./models/IPoint";
import { IVector } from "./models/IVector";
import Bubble from "./Bubble";

export class Game {
  private canvas: HTMLCanvasElement;
  private ctx: CanvasRenderingContext2D | null;
  private mouse: Mouse;
  private mousePos: IPoint = { x: 200, y: 200 };
  private bubble: Bubble;
  private bubbles: Array<Bubble> = [];

  constructor() {
    this.canvas = <HTMLCanvasElement>document.getElementById("gameCanvas");
    this.ctx = this.canvas.getContext("2d");

    this.mouse = new Mouse(this.canvas);
    window.addEventListener("mousemove", (e: MouseEvent) => {
      this.mousePos = this.mouse.getPosition(e);
    });

    const initalPos: Vector = new Vector({ x: 200, y: 200 });
    this.prevPos = Vector.CreateVector(initalPos);
    this.bubble = new Bubble(this.ctx!, initalPos, 30);

    for (let i = 0; i < 100; i++) {
      const vect = new Vector({ x: Math.random() * 1600, y: Math.random() * 1600 });
      this.bubbles.push(new Bubble(this.ctx!, vect, Math.random() * 20));
    }

    setInterval(() => {
      this.fillCanvas();
      this.drawBoard();

      this.bubble.show();
      this.bubble.update(this.mousePos);

      for (let i = this.bubbles.length - 1; i >= 0; i--) {
        this.bubbles[i].show();
        if (this.bubble.canEat(this.bubbles[i])) {
          this.shouldUpdateZoom = true;
          this.bubbles.splice(i, 1);
        }
      }

      if (this.shouldUpdateZoom) {
        console.log("update zoom");
        this.shouldUpdateZoom = false;
      }

      const dx = this.bubble.pos.cord.x - this.prevPos.cord.x;
      const dy = this.bubble.pos.cord.y - this.prevPos.cord.y;
      this.ctx!.translate(-dx, -dy);

      this.prevPos.cord.x = this.bubble.pos.cord.x;
      this.prevPos.cord.y = this.bubble.pos.cord.y;
    }, 20);
  }

  private prevPos: IVector;
  private shouldUpdateZoom: boolean = false;

  protected fillCanvas() {
    this.ctx!.fillStyle = "gray";
    this.ctx!.fillRect(0, 0, 400 * 4, 400 * 4);
  }

  drawBoard() {
    let bw = 400 * 4;
    let bh = 400 * 4;
    let p = 20;

    for (let x = 0; x <= bw; x += 40) {
      this.ctx!.moveTo(0.5 + x + p, p);
      this.ctx!.lineTo(0.5 + x + p, bh + p);
    }

    for (var x = 0; x <= bh; x += 40) {
      this.ctx!.moveTo(p, 0.5 + x + p);
      this.ctx!.lineTo(bw + p, 0.5 + x + p);
    }

    this.ctx!.strokeStyle = "yellow";
    this.ctx!.stroke();
  }

  stfu() {}
}
