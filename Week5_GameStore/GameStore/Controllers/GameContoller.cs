using DAL.Model;
using Entities;
using GameStore2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]  
        public class GameController : ControllerBase
        {
            List<Game> GameList = new List<Game>();    
            Result result = new Result();
            DBOperations DbOpp = new DBOperations();

          //  [Authorize]
            [HttpGet]
            public List<Game> GetGames()
            {   
                return DbOpp.GetGames();
            }

            [HttpGet("/GameController/GetGamesPaging")]
            public IActionResult GetUserPaging([FromQuery] OwnerParameters ownerParameters)
            {
                var owners = DbOpp.GetGames() //Db de game tablosundaki veriler
                //.OrderBy(on => on.Id) //Sıralama
               .Skip(ownerParameters.PageNumber) //Kacıncı Kayıttan itibaren veri gelicek
               .Take(ownerParameters.PageSize) //Kaç tane veri gelecek 
               .ToList();


                return Ok(owners);
            }

            [HttpGet("{id}")]
            public Game? GetGameById(int id)
            {
            return DbOpp.GetGame(id);
            }


            [HttpPost]
            public Result AddGame([FromBody] Game newGame)
            {

            //Eğer kitap benim listemde mevcut ise listeme eklememesi için 'is not null' olarak BadRequest çağırırz.
            if (DbOpp.FindGame(newGame.Id, newGame.Name) is not null)
            {
               // return BadRequest();
                result.status = 0;
                result.message = "Game eklenemedi";
                
            }
                  
            //Eger oyun bu veritabanında yoksa AddModel çağrılır.
            else
            {
                DbOpp.AddGame(newGame);
                result.status = 1;
                result.message = "Game eklendi.";
                //return Ok();
            }
                return result;
            }


            //HttpPut ile GameList içindeki id değeri ile eşleşen nesnenin güncellenmesi
            [HttpPut("{id}")]
            public Result UpdateGame(int id, [FromBody] Game updatedGame)
            {
              
                if(DbOpp.UpdateGame(id, updatedGame))
                { 
                    result.status = 1;
                    result.message = id + " ID'li game güncellendi.";
                    result.GameList = DbOpp.GetGames();  
                }
                else
                {
                    result.status = 0;
                    result.message = id + " ID'li game güncellenemedi.";
                    result.GameList = DbOpp.GetGames();      
                 }
                 return result;
            }


            //HttpDelete ile GameList içindeki id değeri ile eşleşen nesnenin silinmesi.
            [HttpDelete("{id}")]
            public Result DeleteGame(int id)
            {
                if (DbOpp.DeleteGamel(id))
                {
                    result.status = 1;
                    result.message = id + " ID'li game silindi";
                    result.GameList = DbOpp.GetGames();
                }
                else
                {
                    result.status = 0;
                    result.message = id +" ID'li game zaten silinmişti.";
                    result.GameList = DbOpp.GetGames();
                }
                return result;
            }
 
        }
}
    
