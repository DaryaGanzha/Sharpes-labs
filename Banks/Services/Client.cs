namespace Banks.Services
{
    public class Client
    {
        private string _firstName;
        private string _secondName;
        private string _address;
        private string _passport;

        private Client(string firstName, string secondName, string address, string passport)
        {
            _firstName = firstName;
            _secondName = secondName;
            _address = address;
            _passport = passport;
        }

        public string FirstName => _firstName;
        public string SecondName => _secondName;
        public string Address => _address;
        public string Passport => _passport;

        public bool DoubtfulnessCheck => _address != null && _passport != null;

        public class ClientBilder
        {
            private string _firstName;
            private string _secondName;
            private string _address;
            private string _passport;

            public ClientBilder SetFirstName(string name)
            {
                this._firstName = name;
                return this;
            }

            public ClientBilder SetSecondName(string name)
            {
                this._secondName = name;
                return this;
            }

            public ClientBilder SetAddress(string address)
            {
                this._address = address;
                return this;
            }

            public ClientBilder SetPassport(string passport)
            {
                this._passport = passport;
                return this;
            }

            public Client ToBild()
            {
                var newClient = new Client(_firstName, _secondName, _address, _passport);
                return newClient;
            }
        }
    }
}