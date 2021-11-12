using System;

namespace Banks.Models
{
    public class TopUpCommand : ICommand
    {
        private readonly IAccount _receiver;
        private readonly decimal _money;

        public TopUpCommand(IAccount receiver, decimal money)
        {
            _receiver = receiver;
            _money = money;
        }

        public void Execute()
        {
            _receiver.TopUp(_money);
        }

        public void Undo()
        {
            Console.WriteLine(_money);
            _receiver.MinusSum(_money);
        }
    }
}