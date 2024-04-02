namespace IdentityAPI.Models
{
    public class MongoOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }

        // Các thuộc tính bổ sung (tùy chọn)
        // ...

        public MongoOptions()
        {
        }

        public MongoOptions(string connectionString, string database)
        {
            ConnectionString = connectionString;
            Database = database;
        }
    }
}
