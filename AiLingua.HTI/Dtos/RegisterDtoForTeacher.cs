using System.ComponentModel.DataAnnotations;

namespace AiLingua.HTI.Dtos
{
    public class RegisterDtoForTeacher
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    //    public string ProfilePicture { get; set; }

        [Required]

  //      public string PhoneNumber { get; set; }
        public string About { get; set; }

        [Required]

        public string Password { get; set; }
     //   public string Accent { get; set; } // Example: "USA Accent"
      //  public int ExperienceYears { get; set; }
        public string Speciality { get; set; }
        public string   AccountNumber { get; set; }

        public List<string>? Interests { get; set; }
    //    public List<string> IndustryFamiliarity { get; set; }
    }
}
