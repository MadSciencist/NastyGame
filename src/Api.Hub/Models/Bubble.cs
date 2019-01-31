using Api.Hub.Models.DTOs;

namespace Api.Hub.Models
{
    public class Bubble
    {
        public string Name { get; set; }
        public Point2D Position { get; set; }
        public double Radius { get; set; }

        public Bubble(BubbleDto bubbleDto)
        {
            Position = bubbleDto.Position;
            Radius = bubbleDto.Radius;
        }

        public Bubble(string name, BubbleDto bubbleDto)
        {
            Name = name;
            Position = bubbleDto.Position;
            Radius = bubbleDto.Radius;
        }

        public Bubble(){ }
    }
}
