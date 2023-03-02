using System;
using System.Collections.Generic;
using Banks.Interfaces;

namespace Banks.Services
{
    public class DepositAccount : IAccount
    {
        private DateTime _timeOfCreation = DateTime.Now;

        public DepositAccount(
            Client client,
            int money,
            DateTime resolutionTime,
            Guid accounId,
            int depositThreshold,
            int percentBeforeThreshold,
            double percentAfterThreshold,
            int withdrawalLimit)
        {
            Client = client;
            Money = money;
            ResolutionTime = resolutionTime;
            AccountId = accounId;
            WithdrawalLimit = withdrawalLimit;

            if (money > depositThreshold && depositThreshold != 0)
            {
                // ReSharper disable once PossibleLossOfFraction
                Percent = percentBeforeThreshold + ((money - 1) / depositThreshold * percentAfterThreshold);
            }
            else
            {
                Percent = percentBeforeThreshold;
            }
        }

        public double Percent { get; }
        public int Money { get; set; }
        public DateTime ResolutionTime { get; }
        public int WithdrawalLimit { get; }
        public Guid AccountId { get; }
        public Client Client { get; }
        public bool TrustedAccount => Client.DoubtfulnessCheck;

        public void RemoveMoneyFromAccount(int moneyAmount)
        {
            if (Money < moneyAmount)
            {
                throw new BanksException("Unable to withdraw money. Insufficient funds.");
            }

            if (DateTime.Now > ResolutionTime)
            {
                throw new BanksException("The term of the deposit has not expired.");
            }

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