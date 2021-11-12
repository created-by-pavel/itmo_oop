namespace Banks.Models
{
    public class WithDrawCommand : ICommand
    {
        private readonly IAccount _receiver;
        private readonly decimal _money;

        public WithDrawCommand(IAccount receiver, decimal money)
        {
            _receiver = receiver;
            _money = money;
        }

        public void Execute()
        {
            _receiver.WithDraw(_money);
        }

        public void Undo()
        {
            _receiver.TopUp(_money);
        }
    }
}