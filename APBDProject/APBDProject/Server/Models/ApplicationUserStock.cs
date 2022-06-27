namespace APBDProject.Server.Models
{
    public class ApplicationUserStock
    {
        public string Id { get; set; }
        public int IdStock { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set;}
        public virtual Stock Stock { get; set; }
    }
}
