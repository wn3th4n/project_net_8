using IdentityAPI.Middleware;
using IdentityAPI.Middleware;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;


namespace IdentityAPI.Services
{
    public class User
    {
        public static readonly string DocumentName = nameof(User);

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; init; }
        public required string Email { get; init; }
        public required string Password { get; set; }
        public string? Salt { get; set; }
        public bool IsAdmin { get; init; }

        public void SetPassword(string password, IEncryptor encryptor)
        {
            Salt = encryptor.GetSalt();
            Password = encryptor.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncryptor encryptor) =>
            Password == encryptor.GetHash(password, Salt);


    }


    public interface IUserRepository
    {
        User? GetUser(string email);
        void InsertUser(User user);
    }


    public class UserRepository(IMongoDatabase db) : IUserRepository
    {
        private readonly IMongoCollection<User> _col =
                         db.GetCollection<User>(User.DocumentName);

        public User? GetUser(string email) =>
            _col.Find(u => u.Email == email).FirstOrDefault();

        public void InsertUser(User user) =>
            _col.InsertOne(user);
    }
}
