using StackExchange.Redis;

namespace MultiShop.Basket.Settings
{
    public class RedisService : IDisposable
    {
        private readonly string _host;
        private readonly int _port;
        private ConnectionMultiplexer? _connectionMultiplexer;
        private readonly object _lockObject = new object();

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        // Redis'e bağlan
        public void Connect()
        {
            lock (_lockObject)
            {
                if (_connectionMultiplexer == null || !_connectionMultiplexer.IsConnected)
                {
                    // Fixed: Removed asterisks and used proper field names
                    _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
                }
            }
        }

        // Bağlantıdan Database al
        public IDatabase GetDb(int db = 1)
        {
            if (_connectionMultiplexer == null || !_connectionMultiplexer.IsConnected)
            {
                Connect();
            }

            return _connectionMultiplexer!.GetDatabase(db);
        }

        // Proper disposal implementation
        public void Dispose()
        {
            _connectionMultiplexer?.Dispose();
            GC.SuppressFinalize(this);
        }

        // Check connection status
        public bool IsConnected => _connectionMultiplexer?.IsConnected ?? false;
    }
}