using CMPG323.P2.Services.DataAccess;
using CMPG323.P2.Services.Library.Models.Domain;
using CMPG323.P2.Services.Library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CMPG323.P2.Services.BusinessLogic
{

    #region Interface

    public interface IAuthenticationService
    {
        Task<string> AuthenticateApplicationUser(LoginDetail loginDetail);
        Task<int?> AuthorizeUser(string accessToken);
    }

    #endregion

    public class AuthenticationService : IAuthenticationService
    {

        #region Private Fields

        private readonly string _jwtSecretKey;

        private readonly ImageContext _db;

        #endregion

        #region Constructors

        public AuthenticationService(IConfiguration configuration, ImageContext db)
        {
            _jwtSecretKey = configuration["AppSettings:JwtSecretKey"];
            _db = db;
        }

        #endregion

        #region Public Methods

        public async Task<string> AuthenticateApplicationUser(LoginDetail loginDetail)
        {
            User user = await AuthenticateUser(loginDetail);
            return GenerateJwtToken(user.Username);
        }

        public async Task<int?> AuthorizeUser(string accessToken)
        {
            try
            {
                string userName = ValidateJwtToken(accessToken);
                User user = await _db.Users.AsNoTracking().SingleOrDefaultAsync(e => e.Username.ToUpper().Equals(userName.ToUpper()));
                return user?.Id;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Private Methods

        private async Task<User> AuthenticateUser(LoginDetail loginDetail)
        {
            User user = await _db.Users.SingleOrDefaultAsync(e => e.Username.ToUpper().Equals(loginDetail.Username.ToUpper()));

            if (user != null)
            {
                if (loginDetail.Password.Equals(user.Password))
                {
                    await _db.SaveChangesAsync();
                    return user;
                }
                else
                {
                    throw new Exception("Invalid password");
                }
            }
            else
            {
                throw new Exception("Invalid username");
            }
        }

        private string ValidateJwtToken(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new Exception("We could not resolve your authentication request.");

            if (accessToken.StartsWith("Bearer "))
            {
                accessToken = accessToken["Bearer ".Length..].Trim();
            }

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSecretKey))
            };
            handler.ValidateToken(accessToken, validationParameters, out SecurityToken securityToken);

            return ((JwtSecurityToken)securityToken).Claims.First(e => e.Type == "unique_name").Value;
        }

        private string GenerateJwtToken(string userName)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtSecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddHours(8)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion

    }
}
