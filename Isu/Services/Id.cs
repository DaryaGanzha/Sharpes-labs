namespace Isu.Services
{
    public class Id
    {
        private static Id _instance;
        private int _id;
        protected Id(int id)
        {
            this._id = id;
        }

        public static Id GetInstance()
        {
            if (_instance == null)
                _instance = new Id(0);

            return _instance;
        }

        public int GetId()
        {
            _id++;
            return _id;
        }
    }
}