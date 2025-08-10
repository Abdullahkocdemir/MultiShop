using StackExchange.Redis;

namespace MultiShop.Basket.Settings
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;
        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(string host, int port, ConnectionMultiplexer connectionMultiplexer)
        {
            _host = host;
            _port = port;
            _connectionMultiplexer = connectionMultiplexer;
        }



        // Redis'e bağlan
        public void Connect()
        {
            if (_connectionMultiplexer == null || !_connectionMultiplexer.IsConnected)
            {
                _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
            }
        }

        // Bağlantıdan Database al
        public IDatabase GetDb(int db = 1)
        {
            if (_connectionMultiplexer == null || !_connectionMultiplexer.IsConnected)
            {
                Connect();
            }
            return _connectionMultiplexer!.GetDatabase(0);
        }
    }
}
