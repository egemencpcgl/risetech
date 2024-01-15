namespace PersonServices.Interfaces
{
    public interface IMqttClientService
    {
        void InitMqttClient();
        void StartClient();
        void SubscribeTopic();

    }
}
