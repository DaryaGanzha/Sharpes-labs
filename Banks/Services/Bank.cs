using System;
using System.Collections.Generic;
using Banks.Interfaces;

namespace Banks.Services
{
    public class Bank
    {
        private List<IAccount> _accountsList = new List<IAccount>();

        public Bank(string bankName, int fixedPercent, int commission, int depositThreshold, int percentBeforeThreshold, double percentAfterThreshold, int withdrawalLimit)
        {
            BankName = bankName;
            FixedPercent = fixedPercent;
            Commission = commission;
            DepositThreshold = depositThreshold;
            PercentBeforeThreshold = percentBeforeThreshold;
            PercentAfterThreshold = percentAfterThreshold;
            WithdrawalLimit = withdrawalLimit;
        }

        public int WithdrawalLimit { get; }
        public int DepositThreshold { get; }
        public int PercentBeforeThreshold { get; }
        public double PercentAfterThreshold { get; }
        public string BankName { get; }
        public int FixedPercent { get; }
        public int Commission { get; }

        public DebitAccount CreateDebitAccount(Client client, int money)
        {
            var accountId = Guid.NewGuid();
            var newDebitAccount = new DebitAccount(client, money, FixedPercent, accountId, WithdrawalLimit);
            _accountsList.Add(newDebitAccount);
            return newDebitAccount;
        }

        public DepositAccount CreateDepositAccount(Client client, int money, DateTime resolutionTime)
        {
            var accountId = Guid.NewGuid();
            var newDepositAccount = new DepositAccount(client, money, resolutionTime, accountId, DepositThreshold, PercentBeforeThreshold, PercentAfterThreshold, WithdrawalLimit);
            _accountsList.Add(newDepositAccount);
            return newDepositAccount;
        }

        public CreditAccount CreateCreditAccount(Client client, int money, int commission)
        {
            var accountId = Guid.NewGuid();
            var newCreditAccount = new CreditAccount(client, money, commission, accountId, WithdrawalLimit);
            _accountsList.Add(newCreditAccount);
            return newCreditAccount;
        }
    }
}