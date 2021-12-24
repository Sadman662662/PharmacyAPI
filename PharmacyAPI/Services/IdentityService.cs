using PharmacyAPI.Contracts.V1.Request;
using PharmacyAPI.Data;
using PharmacyAPI.Model;
using PharmacyAPI.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public class IdentityService : IIdentityService
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly PharmacyDBContext _pharmacyDBContext;
        public IdentityService(UserManager<ApplicationUser> userManager, JwtSettings jwtSettings,
            PharmacyDBContext pharmacyDBContext)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _pharmacyDBContext = pharmacyDBContext;
        }
        
        public async Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest user)
        {
            var existingUser = await _userManager.FindByNameAsync(user.PhoneNumber);
            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email exists" }
                };
            }

            var existingPhone = _pharmacyDBContext.Users.Any(u => u.PhoneNumber == user.PhoneNumber);
            if (existingPhone)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Phone No already registered" }
                };
            }

            var newUser = new ApplicationUser
            {
                Email = user.Email,
                UserName = user.PhoneNumber,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                CreatedAt = DateTime.Now
            };

            var createdUser = await _userManager.CreateAsync(newUser, user.Password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }
            return GenerateAuthenticationResultForUser(newUser);
        }

        private AuthenticationResult GenerateAuthenticationResultForUser(ApplicationUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id),
                    new Claim("username", newUser.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }

        public async Task<AuthenticationResult> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exists" }
                };
            }

            var UserHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!UserHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not have valid Phone/Password" }
                };
            }
            return GenerateAuthenticationResultForUser(user);
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
                return true;
            return false;
        }

        public async Task<Guid> GetUserId(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user!=null)
            {
                return new Guid(user.Id);
            }
            else
            {
                return new Guid();
            }
        }
    }
}

