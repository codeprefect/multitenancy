namespace Multitenant.Models
{
    public enum DatabaseProvider {
        SQLITE = 0,
        MSSQLSERVER = 1,
        POSGRES = 2,
        MYSQL = 3,
    }

    public interface ITenant {
        int Id { get; set; }
        string Name { get; set; }
        string Host { get; set; }
        string ConnectionString { get; set; }
        DatabaseProvider Provider { get; set; }
        string Theme { get; set; }
    }

    public class Tenant : ITenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string ConnectionString { get; set; }
        public string Theme { get; set; }
        public DatabaseProvider Provider { get; set; }
    }
}
