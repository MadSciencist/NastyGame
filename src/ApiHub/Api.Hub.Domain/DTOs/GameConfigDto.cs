using Api.Hub.Domain.GameConfig;

namespace Api.Hub.Domain.DTOs
{
    public class GameConfigDto
    {
        public int WorldWidth { get; private set; }
        public int WorldHeight { get; private set; }
        public int InitialRadius { get; private set; }
        public string RegisteredName { get; private set; }

        public GameConfigDto(string name)
        {
            WorldWidth = CanvasConfig.WorldWidth;
            WorldHeight = CanvasConfig.WorldHeight;
            InitialRadius = BubbleConfig.InitialPlayerRadius;
            RegisteredName = name;
        }

        public GameConfigDto(){ }
    }
}
