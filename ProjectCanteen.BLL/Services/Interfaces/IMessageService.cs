namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IMessageService
    {
        void SendMessage<T>(T message);
    }
}
