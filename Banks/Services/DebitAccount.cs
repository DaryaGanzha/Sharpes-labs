using System;
using Banks.Interfaces;

namespace Banks.Services
{
    public class DebitAccount : IAccount
    {
        private DateTime _timeOfCreation = DateTime.Now;

        public DebitAccount(Client client, int money, int percent, Guid accountId, int withdrawalLimit)
        {
            Money = money;
            Percent = percent;
            Client = client;
            AccountId = accountId;
            WithdrawalLimit = withdrawalLimit;
        }

        public Client Client { get; }
        public int Money { get; set; }
        public int Percent { get; }
        public int WithdrawalLimit { get; }
        public Guid AccountId { get; }
        public bool TrustedAccount => Client.DoubtfulnessCheck;

        public void RemoveMoneyFromAccount(int moneyAmount)
        {
            if (TrustedAccount == false && moneyAmount > WithdrawalLimit)
            {
                throw new BanksException("The unreliable client has exceeded the withdrawal limit.");
            }

            if (Money < moneyAmount)
            {
                throw new BanksException("Unable to withdraw money. Insufficient funds.");
            }

            Money -= moneyAmount;
        }

        public void AddMoneyToAccount(int moneyAmount)
        {
            Money += moneyAmount;
        }
    }
}