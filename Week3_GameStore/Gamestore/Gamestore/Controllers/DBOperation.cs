using DAL.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using Entities;

namespace GameStore2.Controllers
{
    public class DBOperations
    {
        private GameContext _context = new GameContext();
        

        public void AddModel(Game _game)
        {
            _context.Game.Add(_game);
            _context.SaveChanges();
        }

        public List<Game> GetGame()
        {
            return _context.Game.ToList();      
        }
    }
}
