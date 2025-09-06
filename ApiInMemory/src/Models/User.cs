using ApiInMemory.Enums;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiInMemory.Models
{
    public class User
    {
        [SwaggerIgnore] // Ignora no Swagger (input)
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] // Aparece no response
        public int Id { get; set; }
        
        [SwaggerSchema("Código único da pessoa")]
        public required int PersonCode { get; set; }
        
        [SwaggerSchema("Nome completo do usuário")]
        public required string Name { get; set; }
        
        [SwaggerSchema("E-mail do usuário")]
        public required string Email { get; set; }
        
        [SwaggerSchema("Senha do usuário")]
        public required string Password { get; set; }

        [SwaggerSchema("Dica da senha do usuário")]
        public string? PasswordHint { get; set; }

        [SwaggerSchema("Idade do usuário")]
        public required int Age { get; set; }
        
        [SwaggerSchema("Sexo/Gênero do usuário")]
        public required Gender Gender { get; set; }
        
        [SwaggerIgnore] // Ignora no Swagger (input)
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] // Aparece no response
        public DateTime CreatedAt { get; set; }
        
        [SwaggerIgnore] // Ignora no Swagger (input)
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] // Aparece no response
        public DateTime UpdatedAt { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name) &&
                   !string.IsNullOrEmpty(Email) &&
                   Email.Contains('@');
        }

        public void UpdateTimestamps()
        {
            if (CreatedAt == default)
                CreatedAt = DateTime.UtcNow;

            UpdatedAt = DateTime.UtcNow;
        }
    }
}