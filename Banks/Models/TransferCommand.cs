namespace Banks.Models
{
    public class TransferCommand : ICommand
    {
        private readonly IAccount _accountFrom;
        private readonly IAccount _accountTo;
        private readonly decimal _money;

        public TransferCommand(IAccount accountFrom, IAccount accountTo, decimal money)
        {
            _accountFrom = accountFrom;
            _accountTo = accountTo;
            _money = money;
        }

        public void Execute()
        {
            _accountFrom.Transfer(_accountTo, _money);
        }

        public void Undo()
        {
            _accountFrom.TopUp(_money);
            _accountTo.MinusSum(_money);
        }
    }
}