import * as SignalR from "@aspnet/signalr";
import * as MsgPack from "@aspnet/signalr-protocol-msgpack";
import Constants from "../Constants";
import BubbleDto from "./BubbleDto";
import EnemyBubbleDto from "./EnemyBubbleDto";
import { GameConfigDto } from "./GameConfigDto";
import GameConfig from "../GameConfig";
import PlayerBubble from "../models/PlayerBubble";
import PlayerDto from "./PlayerDto";

export default class MultiplayerService {
  private conn: SignalR.HubConnection;
  private nickname: string;
  private token: string = "";
  private useAuthentication: boolean;
  private enemiesUpdated: (dto: Array<EnemyBubbleDto>) => void;
  private connectionStarted: (dto: GameConfig) => void;
  private lostGame: () => void;

  constructor(nickname: string, useAuthentication: boolean, token?: string) {
    this.nickname = nickname;
    this.useAuthentication = useAuthentication;
    this.token = token;
  }

  public startConnection() {
    if (this.useAuthentication) {
      this.conn = new SignalR.HubConnectionBuilder()
        .withUrl(Constants.HubEndpoint, { accessTokenFactory: () => this.token })
        .withHubProtocol(new MsgPack.MessagePackHubProtocol())
        .configureLogging(SignalR.LogLevel.Information)
        .build();
    } else {
      this.conn = new SignalR.HubConnectionBuilder()
        .withUrl(Constants.HubEndpoint, {})
        .withHubProtocol(new MsgPack.MessagePackHubProtocol())
        .configureLogging(SignalR.LogLevel.Information)
        .build();
    }

    this.conn
      .start()
      .then(this.onConnected.bind(this))
      .catch(this.errorHandler.bind(this));

    this.conn.on("UpdateEnemies", (enemies: Array<EnemyBubbleDto>) => {
      this.enemiesUpdated(enemies);
    });

    this.conn.on("Lost", (lostDto: PlayerDto) => {
      console.log(lostDto);
      this.conn.stop();
      this.lostGame();
    });

    this.conn.on("Scored", (scoredDto: PlayerDto) => {
      console.log(scoredDto);
    });
  }

  public updateMyPosition(bubble: PlayerBubble) {
    if (this.conn.state === SignalR.HubConnectionState.Connected) {
      const bubbleDto = new BubbleDto(bubble);

      this.conn.send("Update", bubbleDto).catch(this.errorHandler.bind(this));
    }
  }

  // function to register callback by clients
  public onEnemiesUpdated(callback: (position: Array<EnemyBubbleDto>) => void): void {
    this.enemiesUpdated = callback;
  }

  public onStarted(callback: (dto: GameConfig) => void): void {
    this.connectionStarted = callback;
  }

  public async onLost(callback: () => void) {
    this.lostGame = callback;
  }

  private async onConnected() {
    const configDto: GameConfigDto = await this.registerNickname(this.nickname);
    this.connectionStarted(new GameConfig(configDto));
  }

  private async registerNickname(name: string): Promise<any> {
    if (this.conn.state === SignalR.HubConnectionState.Connected) {
      return this.conn
        .invoke<GameConfigDto>("RegisterName", name)
        .catch(this.errorHandler.bind(this));
    }
  }

  private errorHandler(err: any): void {
    console.error(err.toString());
  }
}
