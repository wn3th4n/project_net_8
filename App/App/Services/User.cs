using IdentityAPI.Middleware;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Data;


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

  
    public interface IUserService
    {
        User? Get(string email);
        User? GetByUID(string uid);
        List<User> GetAll();
        void Insert(User user);
        void Update(string uid,User updateUser);
        void RemoveAt(string uid);
        void RemoveAt(User user);
    }


    public class UserService(IMongoDatabase db) : IUserService
    {
        private readonly IMongoCollection<User> _col =
                         db.GetCollection<User>(User.DocumentName);

        public User? Get(string email) =>
            _col.Find(u => u.Email == email).FirstOrDefault();

        public User? GetByUID(string uid) =>
           _col.Find(u => u.Id == uid).FirstOrDefault();

        public List<User> GetAll() =>
            _col.Find(u => true).ToList();

        public void Insert(User user) =>
            _col.InsertOne(user);

        public void Update(string uid,User updateUser) =>
            _col.ReplaceOne(u => u.Id == uid, updateUser);

        public void RemoveAt(string uid) =>
            _col.DeleteOne(u => u.Id == uid);
        
        public void RemoveAt(User user) =>
            _col.DeleteOne(u => u.Id == user.Id);
    }

        
}
