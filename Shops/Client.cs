namespace Shops
{
    public class Client
    {
        public Client(string name, int money)
        {
            Money = money;
            ClientName = name;
        }

        public int Money { get; }
        public string ClientName { get; }
    }
}