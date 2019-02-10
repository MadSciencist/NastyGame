namespace Api.Statistics.Domain.Entity
{
    public class GameVictimsEntity
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int MurdererId { get; set; }
        public int VictimId { get; set; }
        public string VictimName { get; set; }
    }
}
