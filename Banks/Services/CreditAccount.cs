using System;
using Banks.Interfaces;

namespace Banks.Services
{
    public class CreditAccount : IAccount
    {
        private int _commission;
        private DateTime _timeOfCreation = DateTime.Now;

        public CreditAccount(Client client, int money, int commission, Guid id, int withdrawalLimit)
        {
            Client = client;
            Money = money;
            _commission = commission;
            AccountId = id;
            WithdrawalLimit = withdrawalLimit;
        }

        public Guid AccountId { get; }
        public Client Client { get; }
        public int Money { get; set; }
        public int WithdrawalLimit { get; }
        public bool TrustedAccount => Client.DoubtfulnessCheck;

        public void RemoveMoneyFromAccount(int moneyAmount)
        {
            if (TrustedAccount == false && moneyAmount > WithdrawalLimit)
            {
                throw new BanksException("The unreliable client has exceeded the withdrawal limit.");
            }

            Money -= moneyAmount;
        }

        public void AddMoneyToAccount(int moneyAmount)
        {
            Money += moneyAmount;
        }
    }
}