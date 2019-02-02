using Api.Hub.Domain.GameConfig;

namespace Api.Hub.Domain.DTOs
{
    public class GameConfigDto
    {
        public string ConnectionId { get; private set; }
        public string RegisteredName { get; private set; }
        public int WorldWidth { get; private set; }
        public int WorldHeight { get; private set; }
        public int InitialRadius { get; private set; }



        public GameConfigDto(string connectionId, string name)
        {
            ConnectionId = connectionId;
            RegisteredName = name;
            WorldWidth = CanvasConfig.WorldWidth;
            WorldHeight = CanvasConfig.WorldHeight;
            InitialRadius = BubbleConfig.InitialPlayerRadius;
        }

        public GameConfigDto(){ }
    }
}
