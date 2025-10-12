

namespace ToDo.Application.DTOs
{
    public class UserDto // işlem sonraasında frontende response olarak kullancaz
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<CategoryDto> Categories { get; set; } = new();
    }
}
}
