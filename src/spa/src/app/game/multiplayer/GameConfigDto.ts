export class GameConfigDto {
  public WorldWidth: number;
  public WorldHeight: number;
  public InitialRadius: number;
  public RegisteredName: string;

  constructor(width: number, height: number, x: number, y: number, r: number, name: string) {
    this.WorldHeight = height;
    this.WorldWidth = width;
    this.InitialRadius = r;
    this.RegisteredName = name;
  }
}
