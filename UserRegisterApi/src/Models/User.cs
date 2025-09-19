using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserRegisterApi.Models
{

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string CodigoPessoa { get; set; } = null!;
        public string LembreteSenha { get; set; } = null!;
        public int Idade { get; set; }
        public string Sexo { get; set; } = null!;
    }
}