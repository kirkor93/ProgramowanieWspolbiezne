namespace Zad_1
{
    public abstract class Singleton<T> where T : new()
    {
        private T _instance;

        public T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }
}
