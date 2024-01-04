using Google.Apis.Auth;

namespace IDBMS_API.Supporters.JwtAuthSupport
{
    public class GoogleTokenVerify
    {
        public async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleTokenId(string token)
        {
            try
            {
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(token);

                return payload;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid google token");

            }
            return null;
        }
    }
}
