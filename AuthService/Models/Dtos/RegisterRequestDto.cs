namespace AuthService.Models.Dtos
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nic { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? JobPost { get; set; }

    }
}
