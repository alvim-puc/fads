using ApiInMemory.Enums;

namespace ApiInMemory.DTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public int PersonCode { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
