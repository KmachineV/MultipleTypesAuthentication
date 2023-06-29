using MultipleTypesAuthentication.Domain;

namespace MultipleTypesAuthentication.DTO
{
    public class UserResponseDTO
    {
      
        public string Message { get; set; }
        public int Code { get; set; }
        public User? user { get; set; }
    }
}
