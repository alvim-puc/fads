using ApiInMemory.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiInMemory.Models
{
    public class UserPatchRequest
    {
        [SwaggerSchema("Nome completo do usuário (opcional)")]
        public string? Name { get; set; }
        
        [SwaggerSchema("E-mail do usuário (opcional)")]
        public string? Email { get; set; }
        
        [SwaggerSchema("Senha do usuário (opcional)")]
        public string? Password { get; set; }
        
        [SwaggerSchema("Código único da pessoa (opcional)")]
        public int? PersonCode { get; set; }
        
        [SwaggerSchema("Lembrete de senha (opcional)")]
        public string? PasswordHint { get; set; }
        
        [SwaggerSchema("Idade do usuário (opcional)")]
        public int? Age { get; set; }
        
        [SwaggerSchema("Sexo/Gênero do usuário (opcional)")]
        public Gender? Gender { get; set; }
    }
}