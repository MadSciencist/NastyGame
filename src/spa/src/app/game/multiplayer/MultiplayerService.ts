import * as SignalR from "@aspnet/signalr";
import * as MsgPack from "@aspnet/signalr-protocol-msgpack";
import Constants from "../Constants";
import Bubble from "../Bubble";
import BubbleDto from "./BubbleDto";
import EnemyBubbleDto from "./EnemyBubbleDto";

export default class MultiplayerService {
  private conn: SignalR.HubConnection;
  // private token: string;
  private enemiesUpdated: (dto: Array<EnemyBubbleDto>) => void;

  constructor() {
    // this.token =
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI1Y2MxNTU3Ni01OGNmLTQ5OTYtOTY2YS1kMTg0MmIxMTA1ZTQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTWF0dGUiLCJlbWFpbCI6ImFzZEAuYXMiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEyIiwiZXhwIjoxNTQ4OTk4MTExLCJpc3MiOiJOYXN0eUdhbWUiLCJhdWQiOiIqIn0.1cv1fy2Rs3GNlWUEuAfQi01FqKcE--qC1g1KMmxVPsY";
    this.conn = new SignalR.HubConnectionBuilder()
      // .withUrl(Constants.HubEndpoint, { accessTokenFactory: () => this.token })
      .withUrl(Constants.HubEndpoint, {})
      .withHubProtocol(new MsgPack.MessagePackHubProtocol())
      .configureLogging(SignalR.LogLevel.Information)
      .build();

    this.conn
      .start()
      .then(this.onConnected.bind(this))
      .catch(this.errorHandler.bind(this));

    this.conn.on("UpdateEnemies", (enemies: Array<EnemyBubbleDto>) => {
      this.enemiesUpdated(enemies);
    });
  }

  public updateMyPosition(bubble: Bubble) {
    if (this.conn.state === SignalR.HubConnectionState.Connected) {
      const bubbleDto = new BubbleDto(bubble);

      this.conn.send("Update", bubbleDto).catch(this.errorHandler.bind(this));
    }
  }

  private registerNickname(name: string) {
    if (this.conn.state === SignalR.HubConnectionState.Connected) {
      this.conn.invoke("RegisterName", name).catch(this.errorHandler.bind(this));
    }
  }

  // function to register callback by clients
  public onTrasureSpawned(callback: (position: Array<EnemyBubbleDto>) => void): void {
    this.enemiesUpdated = callback;
  }

  private onConnected() {
    console.log("Connection started");
    this.registerNickname("TestNick");
  }

  private errorHandler(err: any): void {
    console.error(err.toString());
  }
}
