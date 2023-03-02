using System;
using System.Collections.Generic;
using Banks.Interfaces;

namespace Banks.Services
{
    public class CentralBank
    {
        private List<Bank> _banksList = new List<Bank>();
        private List<MoneyTransfer> _moneyTransfersList = new List<MoneyTransfer>();

        public Bank CreateBank(string name, int fixedPercent, int commission, int depositThreshold, int percentBeforeThreshold, double percentAfterThreshold, int withdrawalLimit)
        {
            var newBank = new Bank(name, fixedPercent, commission, depositThreshold, percentBeforeThreshold, percentAfterThreshold, withdrawalLimit);
            _banksList.Add(newBank);
            return newBank;
        }

        public void TransferBetweenAccounts(IAccount account1, IAccount account2, int money)
        {
            account1.RemoveMoneyFromAccount(money);
            account2.AddMoneyToAccount(money);
            var newMoneyTransfer = new MoneyTransfer(account1, account2, money);
            _moneyTransfersList.Add(newMoneyTransfer);
        }

        public void CancellationOfTransfer(IAccount account1, IAccount account2, int money)
        {
            foreach (MoneyTransfer moneyTransfer in _moneyTransfersList)
            {
                if (moneyTransfer.Account1.AccountId == account1.AccountId && moneyTransfer.Account2.AccountId == account2.AccountId &&
                    moneyTransfer.Money == money)
                {
                    account1.AddMoneyToAccount(money);
                    account2.RemoveMoneyFromAccount(money);
                    _moneyTransfersList.Remove(moneyTransfer);
                    return;
                }
            }
        }

        public void TopUpAnAccount(IAccount account, int money)
        {
            account.AddMoneyToAccount(money);
        }

        public void WithdrawMoneyFromAnAccount(IAccount account, int money)
        {
            account.RemoveMoneyFromAccount(money);
        }
    }
}