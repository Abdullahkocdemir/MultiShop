using StackExchange.Redis;
using System;

public class RedisService
{
    private readonly string _connectionString;
    // Lazy nesnesi sayesinde bağlantı sadece ihtiyaç duyulduğunda ve bir kez oluşturulur.
    private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

    public RedisService(string host, int port)
    {
        _connectionString = $"{host}:{port}";

        _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(_connectionString);
        });
    }

    // Dışarıdan bağlantıya güvenli erişim sağlayan Property
    public ConnectionMultiplexer Connection => _lazyConnection.Value;

    // Veritabanına hızlı erişim için yardımcı metod
    public IDatabase GetDatabase(int db = -1) => Connection.GetDatabase(db);
}