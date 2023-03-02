using System;
using Banks.Services;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTest
    {
        [Test]
        public void UnreliableClientHasExceededTheLimit_Exception()
        {
            var bilder = new Client.ClientBilder();
            string firstName = "FirstName";
            string secondName = "SecondName";
            Client сlient1 = bilder.SetFirstName(firstName).SetSecondName(secondName)
                .ToBild();
            var centralBank = new CentralBank();
            Bank bank = centralBank.CreateBank("bank1", 1, 500, 50000, 1, 0.2, 10000);
            DebitAccount debitAccount1 = bank.CreateDebitAccount(сlient1, 100000);
            Assert.Catch<BanksException>(() =>
            {
                centralBank.WithdrawMoneyFromAnAccount(debitAccount1, 11000);
            });
        }

        [Test]
        public void AddingFieldsToTheClient()
        {
            var bilder = new Client.ClientBilder();
            string firstName = "FirstName";
            string secondName = "SecondName";
            Client сlient1 = bilder.SetFirstName(firstName).SetSecondName(secondName)
                .ToBild();
            Assert.AreEqual(сlient1.Passport, null);
            string address = "address";
            string passport = "passport";
            сlient1 = bilder.SetAddress(address).SetPassport(passport).ToBild();
            Assert.AreEqual(сlient1.Passport, "passport");
        }

        [Test]
        public void CanceledTheTransaction_TransactionCanceled()
        {
            var bilder1 = new Client.ClientBilder();
            string firstName1 = "FirstName1";
            string secondName1 = "SecondName1";
            string address1 = "address1";
            string passport1 = "passport1";
            Client сlient1 = bilder1.SetFirstName(firstName1).SetSecondName(secondName1).SetAddress(address1).SetPassport(passport1)
                .ToBild();
            var bilder2 = new Client.ClientBilder();
            string firstName2 = "FirstName2";
            string secondName2 = "SecondName2";
            string address2 = "address2";
            string passport2 = "passport2";
            Client сlient2 = bilder2.SetFirstName(firstName2).SetSecondName(secondName2).SetAddress(address2).SetPassport(passport2)
                .ToBild();
            var centralBank = new CentralBank();
            Bank bank1 = centralBank.CreateBank("bank1", 1, 500, 50000, 1, 0.2, 10000);
            DebitAccount debitAccount1 = bank1.CreateDebitAccount(сlient1, 100000);
            Bank bank2 = centralBank.CreateBank("bank2", 2, 200, 30000, 2, 0.5, 5000);
            DebitAccount debitAccount2 = bank2.CreateDebitAccount(сlient2, 50000);
            centralBank.TransferBetweenAccounts(debitAccount1, debitAccount2, 10000);
            Assert.AreEqual(debitAccount2.Money, 60000);
            centralBank.CancellationOfTransfer(debitAccount1, debitAccount2, 10000);
            Assert.AreEqual(debitAccount1.Money, 100000);
        }
    }
}