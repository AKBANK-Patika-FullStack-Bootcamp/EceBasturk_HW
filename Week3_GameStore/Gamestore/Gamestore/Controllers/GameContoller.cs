using DAL.Model;
using GameStore2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GameStore2.Controllers
{
    [ApiController]
    [Route("[controller]s")]  
        public class GameController : ControllerBase
        {
            List<Game> GameList = new List<Game>();
            Logger logger = new Logger();
            Result result = new Result();
            DBOperations DbOpp = new DBOperations();
            //GameContext0 gameDbOperation = new GameContext();

          /*  private static List<Game> GameList = new List<Game>()
        {
                // Genre 1 : War
                // 2 : Adventure
                // 3 : Race
            new Game{Id = 1, Name = "Call Of Duty", Genre=1, Publisher = "Activision", PublishDate = new DateTime(2019,10,19)},
            new Game{Id = 2, Name ="Assassin's Creed", Genre = 2, Publisher = "Ubisoft" , PublishDate = new DateTime(2016,01,01) },
            new Game{Id = 3, Name = "Need For Speed", Genre = 3, Publisher = "Electronic Arts", PublishDate = new DateTime(2014,02,21)}
        }; */



            [HttpGet]
            public List<Game> GetGame()
            {   
                return DbOpp.GetGame();
               // GameList = GameList.OrderBy(x => x.Name).ToList<Game>();
               //GameList = gameDbOperation.game.OrderBy(g => g.Id).ToList();
               // return GameList;
            }

            /// <summary>
            /// HttpGet request with id
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            [HttpGet("{id}")]
            public Game getById(int id)
            {
                var game = GameList.Where(game => game.Id == id).SingleOrDefault();
                return game;
            }

            /// <summary>
            /// Requestler yapılırken sadece bir request çağrımı parametresiz olarak-[HttpGet]- çağırabilir.
            /// İstenilen id değeri ile Game listeleme.
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            /* [HttpGet]
             public Game getById([FromQuery] string id)
             {
                 var game = GameList.Where(game => game.Id == Convert.ToInt32(id)).SingleOrDefault();
                 return game;
             } */

            //HtppPost ile veri eklenmesi
            [HttpPost]
            public IActionResult AddGame([FromBody] Game newGame)
            {
               // var game = GameList.SingleOrDefault(x => x.Name == newGame.Name);

                //Eğer kitap benim listemde mevcut ise listeme eklememesi için 'is not null' olarak BadRequest çağırırz.
                //if (game is not null)
                  //  return BadRequest();

                DbOpp.AddModel(newGame);
                return Ok();
            }

            //HttpPut ile GameList içindeki id değeri ile eşleşen nesnenin güncellenmesi
            [HttpPut("{id}")]
            public IActionResult UpdateGame(int id, [FromBody] Game updatedGame)
            {
                var game = GameList.SingleOrDefault(x => x.Id == id);
                if (game is null)
                    return BadRequest();

                game.Genre = updatedGame.Genre != default ? updatedGame.Genre : game.Genre;
                if (updatedGame.Name != "string") game.Name = updatedGame.Name;
                game.Publisher = updatedGame.Publisher != default ? updatedGame.Publisher : game.Publisher;
                game.PublishDate = updatedGame.PublishDate != default ? updatedGame.PublishDate : game.PublishDate;

                return Ok();
            }

            //HttpDelete ile GameList içindeki id değeri ile eşleşen nesnenin silinmesi.
            [HttpDelete("{id}")]
            public IActionResult DeleteGame(int id)
            {
                var Game = GameList.SingleOrDefault(x => x.Id == id);
                if (Game is null)
                {
                    result.status = 0;
                    result.message = "Kullanici silinemedi.";
                    logger.createLog(id.ToString() + "ID li " + result.message);
                    result.GameList = GameList;
                    return BadRequest();
                }
                GameList.Remove(Game);
                result.status = 1;
                result.message = "Kullanici silindi.";
                result.GameList = GameList;
                logger.createLog(id.ToString() + "ID li " + result.message);
                return Ok();

            }
        }
    }
