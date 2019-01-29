import { IPoint } from "../models/IPoint";

export class Mouse {
  private canvas: HTMLCanvasElement;

  constructor(canvas: HTMLCanvasElement) {
    this.canvas = canvas;
  }

  getPosition(evt: any): IPoint {
    const bounding = this.canvas.getBoundingClientRect();
    return {
      x: ((evt.clientX - bounding.left) / (bounding.right - bounding.left)) * this.canvas.width,
      y: ((evt.clientY - bounding.top) / (bounding.bottom - bounding.top)) * this.canvas.height
    };
  }
}
