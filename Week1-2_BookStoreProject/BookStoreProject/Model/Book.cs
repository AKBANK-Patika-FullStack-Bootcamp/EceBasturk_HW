using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreProject.Model
{
    public class Book
    {
        /// <summary>
        /// Id, Başlık, Tür, Sayfa Sayısı ve Basım Tarihi içeren Book sınıfı.
        /// </summary>
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Genre { get; set; } 
        public int PaperCount { get; set; }
        public DateTime PublishDate { get; set; }


    }
}
