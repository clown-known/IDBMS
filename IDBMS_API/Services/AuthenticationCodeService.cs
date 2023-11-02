using Repository.Implements;
using System.Net.Mail;
using System.Net;
using API.Supporters.JwtAuthSupport;
using Repository.Interfaces;
using IDBMS_API.Constants;
using BusinessObject.Models;

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
        private AuthenticationCode CreateCode(string email)
        {
            var user = userRepository.GetByEmail(email);
            if (user == null) return null;
            // gen code, update to database
            Random random = new Random();
            string rdn = "";
            for (int i = 0; i < 6; i++)
            {
                rdn += (random.Next(0, 9)).ToString();
            }
            authenticationCodeRepository.EnableCodeOfUser(user.Id);
            var code1 = new AuthenticationCode
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                CreatedTime = DateTime.Now,
                ExpiredTime = DateTime.Now.AddMinutes(ConstantValue.ExpiredTimeOfCode),
                Code = rdn,
                Status = BusinessObject.Enums.AuthenticationCodeStatus.Active
            };
            var code = authenticationCodeRepository.Save(code1) ;

            return code;
        }
        public bool SendActivationEmail(string email)
        {

            var code = CreateCode(email);
            if(code == null) return false;
            // end
            // send otp
            using (MailMessage mm = new MailMessage("efoodcompanyservice@gmail.com", email))
            {
                mm.Subject = "Active Code";

                mm.Body = "Your active code is: " + code.Code;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("efoodcompanyservice@gmail.com", "ihyxaxrsytxnwcbo");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
            return true;
        }
    }
}
