namespace Api.Hub.Domain.GameDomain
{
    public class PlayerBase
    {
        public Bubble Bubble { get; set; }
        public string BeatenBy { get; set; }

        public bool CanBeat(PlayerBase otherPlayer)
        {
            return Bubble.Radius > otherPlayer.Bubble.Radius;
        }
    }
}