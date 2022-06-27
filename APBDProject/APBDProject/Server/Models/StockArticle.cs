namespace APBDProject.Server.Models
{
    public class StockArticle
    {
        public int IdStock { get; set; }
        public int IdArticle { get; set; }
        public Stock Stock { get; set; }
        public Article Article { get; set; }
    }
}
