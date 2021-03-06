namespace AlDente.DataAccess.Core
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public int CommandTimeout { get; set; }
        public int CacheItemExpiration { get; set; }

        public int RestauranteId { get; set; }

        public string URL { get; set; }
    }
}
