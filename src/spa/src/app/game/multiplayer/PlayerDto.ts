export default class PlayerDto {
  public ConnectionId: string;
  public Name: string;
  public Score: number;
  public Victims: Array<string>;
  public KilledBy: string;
  public JoinedTime: Date;
}
