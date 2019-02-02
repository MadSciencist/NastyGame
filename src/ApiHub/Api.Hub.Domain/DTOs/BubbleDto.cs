using Api.Hub.Domain.GameDomain;

namespace Api.Hub.Domain.DTOs
{
    public class BubbleDto
    {
        /// <summary>
        /// This is used as DTO client -> server
        /// </summary>
        public Point2D Position { get; set; }
        public double Radius { get; set; }
    }
}
