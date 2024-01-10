using Repository.Implements;
using System.Net.Mail;
using System.Net;
using API.Supporters.JwtAuthSupport;
using Repository.Interfaces;
using IDBMS_API.Constants;
using BusinessObject.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Runtime.CompilerServices;
using IDBMS_API.Supporters.TimeHelper;

namespace IDBMS_API.Services
{
    public class AuthenticationCodeService
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthenticationCodeRepository authenticationCodeRepository;
        private readonly IConfiguration configuration;
        public AuthenticationCodeService(IUserRepository userRepository, IConfiguration configuration, IAuthenticationCodeRepository authenticationCodeRepository)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.authenticationCodeRepository = authenticationCodeRepository;
        }
        public bool Verify(string code,string email)
        {
            var user = userRepository.GetByEmail(email);
            if (user == null) return false;
            AuthenticationCode authcode = authenticationCodeRepository.GetByUserId(user.Id);
            if(authcode == null || !authcode.Code.Equals(code)) return false;
            authcode.Status = BusinessObject.Enums.AuthenticationCodeStatus.Used;
            authenticationCodeRepository.Update(authcode);
            user.Status = BusinessObject.Enums.UserStatus.Active;
            userRepository.Update(user);
            return true;
        }
        public string deCode(string token,string skey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(skey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            string email = jwtToken.Claims.First(claim => claim.Type == "email").Value;
            return email;
        }
        public string CreateCode(string email)
        {
            var user = userRepository.GetByEmail(email);
            if (user == null) return null;
            if (user.LockedUntil != null && user.LockedUntil > TimeHelper.GetTime(DateTime.Now)) return null;
            // gen code, update to database
            Random random = new Random();
            string rdn = "";
            for (int i = 0; i < 32; i++)
            {
                rdn += (random.Next(0, 9)).ToString();
            }
            
            authenticationCodeRepository.EnableCodeOfUser(user.Id);
            if (authenticationCodeRepository.CheckNumOfSend(user.Id) > ConstantValue.NumToLock)
            {
                userRepository.Lock(email);
            }
            var code1 = new AuthenticationCode
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                CreatedTime = TimeHelper.GetTime(DateTime.Now),
                ExpiredTime = TimeHelper.GetTime(DateTime.Now).AddMinutes(ConstantValue.ExpiredTimeOfCode),
                Code = rdn,
                Status = BusinessObject.Enums.AuthenticationCodeStatus.Active
            };
            var code = authenticationCodeRepository.Save(code1) ;

            return rdn;
        }
        public void SendEmail(string email,string subject,string body)
        {
            // send otp
            using (MailMessage mm = new MailMessage("idtautomailer@gmail.com", email))
            {
                mm.Subject = subject;

                mm.Body = body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("idtautomailer@gmail.com", "npufojpvlcxiowda");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
    }
}
