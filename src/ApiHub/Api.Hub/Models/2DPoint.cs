namespace Api.Hub.Models
{
    public class Point2D
    {
        public double x { get; set; }
        public double y { get; set; }

        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
