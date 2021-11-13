using System;
using Banks.Tools;

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
            if (_receiver == null) throw new BanksException("this command not exit");
            _receiver.WithDraw(_money);
        }
    }
}