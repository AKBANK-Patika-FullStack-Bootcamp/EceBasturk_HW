using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DAL.Model;

namespace GameStore2
{
    public class AuthOperations
    {
        private MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        public string CreateToken(Login login, IConfiguration _configuration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                //Token süresi olusturulur
                expires: DateTime.Now.AddDays(1), 
                signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token); 

                return jwt;

        }
        public string MD5Hashing(string _password)
        {
            byte[] array = Encoding.UTF8.GetBytes(_password);
            array = md5.ComputeHash(array);

            //Hashlenen veriyi tutmak için StringBuilder nesnesi oluşturulur.
            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in array)
            {
                stringBuilder.Append(b.ToString("x2").ToLower());
            }
            return stringBuilder.ToString();
        }
    }
}