using Api.Hub.Domain.DTOs;

namespace Api.Hub.Domain.GameDomain
{
    public class Player : PlayerBase
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public bool IsAuthenticated { get; set; }

        public Player(string connectionId, BubbleDto bubbleDto)
        {
            ConnectionId = connectionId;
            Bubble = new Bubble(bubbleDto);
        }

        public Player(){}
    }
}
