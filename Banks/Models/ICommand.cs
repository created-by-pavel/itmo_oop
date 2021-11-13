namespace Banks.Models
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}