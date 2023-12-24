namespace PersonServices.Interfaces
{
    public interface IMqttClientService
    {

        void StartClient();
        void SubscribeTopic();

    }
}
