namespace core.Entities.Model
{
    public class AffiliatedAgency : BaseEntity
    {
        public string Name {get;set;}

        public int ProjectId {get;set;}
        public Project Project {get;set;}
    }
}