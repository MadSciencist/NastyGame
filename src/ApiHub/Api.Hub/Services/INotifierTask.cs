namespace Api.Hub.Services
{
    public interface INotifierTask
    {
        NotifierState State { get; }
        void Start();
        void Stop();
    }
}
