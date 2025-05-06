using AiLingua.Core.Entities.Identity;
using AiLingua.Core.Services.Contract;
using AiLingua.HTI.Dtos;
using AiLingua.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Data;
using System.Net.Mail;
using System.Security.Claims;


namespace AiLingua.HTI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IAuthService authService, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _emailService = emailService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return Unauthorized("User Credentials are wrong");
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded is false)
                return Unauthorized("error at password");
            return Ok(new UserDto()
            {
                FullName = user.FullName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [HttpGet("allUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetAllUsers()
        {
            var users = _userManager.Users.ToList();

            var userDtos = new List<UsersDto>();

            foreach (var user in users) 
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UsersDto
                {
                     Id= user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "NoRole"
                });
            }

            return Ok(userDtos);
        }

        [HttpPost("Teacher/register")]

        public async Task<ActionResult<UserDto>> RegisterAsTeacher(RegisterDtoForTeacher model)
        {
            if (CheckEmailExist(model.Email).Result.Value)
                return BadRequest("this email is already in use!!");
            var user = new User()
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                NormalizedEmail = model.Email.ToUpperInvariant(), // Normalize Email
                Speciality = model.Speciality,
                Interests = model.Interests ?? new List<string>(),
                Password=model.Password,
               // ProfilePicture=model.ProfilePicture,
                About=model.About,
                AccountNumber=model.AccountNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded is false) return BadRequest(result.Errors);
            var roleResult = await _userManager.AddToRoleAsync(user, "Teacher");
            if (!roleResult.Succeeded)
                return BadRequest("Failed to assign Teacher role");
            return Ok(new UserDto()
            {
                FullName = user.FullName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }
        [HttpPost("Student/register")]

        public async Task<ActionResult<UserDto>> RegisterAsStudent(RegisterDtoForStudent model)
        {
            if (CheckEmailExist(model.Email).Result.Value)
                return BadRequest("this email is already in use!!");
            var user = new User()
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                Password = model.Password
               // ProfilePicture = model.ProfilePicture
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded is false) return BadRequest("User Can not be created");
            var roleResult = await _userManager.AddToRoleAsync(user, "Student");
            if (!roleResult.Succeeded)
                return BadRequest("Failed to assign Student role");
            return Ok(new UserDto()
            {
                FullName = user.FullName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound("User not found");

            return Ok(new UserDto()
            {
                FullName = user.FullName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager),
                Role = ClaimTypes.Role
            });
        }
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            string profilePictureUrl = string.IsNullOrEmpty(user.ProfilePicture)
                ? null
                : $"{baseUrl}/{user.ProfilePicture.Replace("\\", "/")}";

            if (roles.Contains("Teacher"))
            {
                return Ok(new
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    UserName = user.UserName,
                    Speciality = user.Speciality,
                    Interests = user.Interests ?? new List<string>(),
                    ProfilePicture = profilePictureUrl,
                    About = user.About,
                    AccountNumber= user.AccountNumber
                });
            }
            else if (roles.Contains("Student"))
            {
                return Ok(new
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    ProfilePicture = profilePictureUrl

                });
            }

            return BadRequest("User role is not recognized.");
        }

        [HttpGet("emailexists")] 

        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;

        }
        [HttpPost("upload-profile-picture")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Get the current user
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound("User not found");

            // Upload image
            string folderName = "images"; // Inside wwwroot/images
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
            Directory.CreateDirectory(uploadsFolder); // Create folder if not exists

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}"; // Random file name
            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Save path to user
            user.ProfilePicture = Path.Combine(folderName, fileName); // e.g., images/abc.png
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest("Could not update user with profile picture");

            return Ok(new { profilePicturePath = user.ProfilePicture });
        }
        [HttpDelete("delete")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("User not found");
            
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest("Failed to delete user");
             
            return Ok("User deleted successfully");
        }
        [HttpDelete("delete-self")]
        public async Task<IActionResult> DeleteSelf()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);


            var user = await _userManager.FindByEmailAsync(email);
            Console.WriteLine(user);
            if (user == null)
                return NotFound("User not found");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest("Failed to delete your account");

            return Ok("Your account has been deleted successfully");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound("User not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
            var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);

            var resetLink = $"{Request.Scheme}://{Request.Host}/reset-password?email={model.Email}&token={encodedToken}";

            // Send the email (replace with actual email service)
            await _emailService.SendEmailAsync(model.Email, "Reset Password", $"Click this link to reset your password: {resetLink}");

            return Ok("Reset password link has been sent to your email.");
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email, [FromQuery] string token, [FromBody] string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("User not found");
            var decodedBytes = WebEncoders.Base64UrlDecode(token);
            var decodedToken = System.Text.Encoding.UTF8.GetString(decodedBytes);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            user.Password = newPassword; 

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return BadRequest(updateResult.Errors);

            return Ok("Password has been successfully reset.");
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound("User not found");

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);
            user.Password = model.NewPassword;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return BadRequest(updateResult.Errors);
            return Ok("Password changed successfully");
        }
        [HttpPut("edit-profile")]
        public async Task<IActionResult> EditProfile(EditUserDto model)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound("User not found");

            user.FullName = model.FullName ?? user.FullName;
            user.Speciality = model.Speciality ?? user.Speciality;
            user.Interests = model.Interests ?? user.Interests;
            user.About = model.About ?? user.About;
            user.AccountNumber=model.AccountNumber ?? user.AccountNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest("Failed to update user");

            return Ok("Profile updated successfully");
        }


    }

}

