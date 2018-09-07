using MongoDB.Bson;
using MongoDB.Driver;
namespace SimpleBot
{
    public class Pessoa
    {
     
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public int Idade { get; set; }
    }
}