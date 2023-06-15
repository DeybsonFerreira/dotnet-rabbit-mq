namespace publisher_api.Services
{
    public interface ITaskPublisher
    {
        void PublishMessage(string text);
    }
}