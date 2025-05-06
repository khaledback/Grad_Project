using System.ComponentModel.DataAnnotations;

namespace AiLingua.HTI.Dtos
{
    public class RegisterDtoForStudent
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
      

        [Required]

        public string Password { get; set; }
    //    public string ProfilePicture { get; set; }


    }
}
