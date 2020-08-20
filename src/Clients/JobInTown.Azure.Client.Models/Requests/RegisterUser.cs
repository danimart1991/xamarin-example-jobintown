namespace JobInTown.Azure.Client.Models.Requests
{
    public class RegisterUser
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FullName { get; set; }
    }
}
