using DAL.Model;
using GameStore2.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace GameStore2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Login login = new Login();
        private readonly IConfiguration _configuration;
        DBOperations dBOperation = new DBOperations();
        AuthOperations authorityOperations = new AuthOperations();

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
     
        [HttpPost("create")]
        public bool LoginCreate(APIAuthority _user)
        {   
            //Password hashlendi.
            _user.Password = authorityOperations.MD5Hashing(_user.Password);
            dBOperation.CreateLogin(_user);
            return true;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromHeader] LoginDto request)
        {
            APIAuthority tokenUser = new APIAuthority();
            tokenUser.UserName = request.Username;
            tokenUser.Password = authorityOperations.MD5Hashing(request.Password);

            
            APIAuthority result = dBOperation.GetLogin(tokenUser);

            if (result != null)
            {
                return Ok(authorityOperations.CreateToken(login, _configuration));
            }
            else
            {
                return Unauthorized("Kullanıcı bulunamadı veya parola hatalı.");
            }
        }
    }
}