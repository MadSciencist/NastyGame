namespace Api.Hub.Infrastructure
{
    public interface IPlayersNotifierTask
    {
        PlayerNotifierState State { get; }
        void Start();
        void Stop();
    }
}