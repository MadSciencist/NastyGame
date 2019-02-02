using Api.Hub.Domain.GameDomain;
using System.Collections.Generic;
using System.Linq;

namespace Api.Hub.Domain.DTOs
{
    public class EnemyBubblesDto
    {
        public string ConnectionId { get; set; }
        public string NickName { get; set; }
        public Point2D Position { get; set; }
        public double Radius { get; set; }

        public EnemyBubblesDto(Player player)
        {
            NickName = player.Name;
            Position = player.Bubble.Position;
            Radius = player.Bubble.Radius;
            ConnectionId = player.ConnectionId;
        }

        // needed for MessagePack serializer
        public EnemyBubblesDto() { }

        public IEnumerable<EnemyBubblesDto> Map(IEnumerable<Player> players)
        {
            return players.Select(x => new EnemyBubblesDto(x));
        }
    }
}
