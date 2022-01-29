using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using BookStoreProject.Model;

namespace BookStoreProject.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
                new Book { Title = "Lean StartUp", Genre = 1, PaperCount = 200, PublishDate = new DateTime(2001, 06, 12) },
                new Book { Title = "Dune", Genre = 2, PaperCount = 250, PublishDate = new DateTime(2010, 05, 23) },
                new Book { Title = "Herland", Genre = 2, PaperCount = 540, PublishDate = new DateTime(2001, 12, 21) });
                context.SaveChanges();
            }

        }
    }
}
