namespace Multitenant.Models
{
    public interface ITenant {
        int Id { get; set; }
        string Name { get; set; }
        string Host { get; set; }
        string ConnectionString { get; set; }
        string Theme { get; set; }
    }

    public class Tenant : ITenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string ConnectionString { get; set; }
        public string Theme { get; set; }
    }
}
