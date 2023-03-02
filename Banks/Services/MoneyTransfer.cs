using Banks.Interfaces;

namespace Banks.Services
{
    public class MoneyTransfer
    {
        public MoneyTransfer(IAccount account1, IAccount account2, int money)
        {
            Account1 = account1;
            Account2 = account2;
            Money = money;
        }

        public IAccount Account1 { get; }
        public IAccount Account2 { get; }
        public int Money { get; }
    }
}