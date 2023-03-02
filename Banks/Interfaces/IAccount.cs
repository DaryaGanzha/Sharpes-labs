using System;
using Banks.Services;

namespace Banks.Interfaces
{
    public interface IAccount
    {
        // static Client Client;
        // public int _money;
        // static Guid _accountId;
        Client Client { get; }
        int Money { get; }
        Guid AccountId { get; }
        int WithdrawalLimit { get; }
        bool TrustedAccount { get; }
        void RemoveMoneyFromAccount(int moneyAmount);
        void AddMoneyToAccount(int moneyAmount);
    }
}