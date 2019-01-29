import * as SignalR from "@aspnet/signalr";
import * as MsgPack from "@aspnet/signalr-protocol-msgpack";
import { IPoint } from "./IPoint";
import Constants from "./Constants";
import { ISnakePositionDto } from "./ISnakePositionDto";

export default class MultiplayerService {
  private _conn: SignalR.HubConnection;
  private group: string = "";
  private updatedPositionCallback: Function;
  private trasureSpawnedCallback: Function;

  constructor() {
    this._conn = new SignalR.HubConnectionBuilder()
      .withUrl(Constants.HUB_ENDPOINT)
      .withHubProtocol(new MsgPack.MessagePackHubProtocol())
      .configureLogging(SignalR.LogLevel.Information)
      .build();

    this._conn
      .start()
      .then(() => {
        console.log("Connection started");
        this.joinGame();
      })
      .catch((err) => console.warn(err));

    this._conn.on("JoinedGroup", (group) => {
      this.group = group;
      console.log(`Joined group: ${group}`);
    });

    this._conn.on("UpdatedPosition", (user: string, dto: ISnakePositionDto) => {
      this.updatedPositionCallback(user, dto.SnakePosition);
    });

    this._conn.on("TrasureSpawned", (trasure: IPoint) => {
      this.trasureSpawnedCallback(trasure);
    });
  }

  public updateMyPosition(snake: Array<IPoint>) {
    if (this._conn.state === SignalR.HubConnectionState.Connected) {
      const dto: ISnakePositionDto = { SnakePosition: snake };
      this._conn
        .send("UpdatePosition", this.group, dto)
        .catch((err) => console.error(err.toString()));
    }
  }

  public spawnTrasure() {
    if (this._conn.state === SignalR.HubConnectionState.Connected) {
      this._conn.send("SpawnTrasure", this.group);
    }
  }

  // function to register callback by clients
  public onTrasureSpawned(callback: (position: IPoint) => void): void {
    this.trasureSpawnedCallback = callback;
  }

  public onUpdatedEnemyPosition(callback: (name: string, position: Array<IPoint>) => void): void {
    this.updatedPositionCallback = callback;
  }

  private joinGame() {
    if (this._conn.state === SignalR.HubConnectionState.Connected) {
      this._conn.send("JoinGame").catch((err) => console.error(err.toString()));
    }
  }
}
