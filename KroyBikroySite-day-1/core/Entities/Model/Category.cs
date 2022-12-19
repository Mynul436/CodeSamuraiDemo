namespace core.Entities.Model
{
    public class Category : BaseEntity
    {
        public string Name {get;set;}

        public int ProjectId {get;set;}
        public Project Project {get;set;}
    }
}