import { GameConfigDto } from "./multiplayer/GameConfigDto";

export default class GameConfig {
  public worldWidth: number;
  public worldHeight: number;
  public initialRadius: number;
  public registeredName: string;
  public connectionId: string;

  constructor(configDto: GameConfigDto) {
    this.worldHeight = configDto.WorldHeight;
    this.worldWidth = configDto.WorldWidth;
    this.initialRadius = configDto.InitialRadius;
    this.registeredName = configDto.RegisteredName;
    this.connectionId = configDto.ConnectionId;
  }
}
