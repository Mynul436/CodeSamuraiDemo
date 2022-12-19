namespace core.Entities.Model
{
    public class Project : BaseEntity
    {
        public string? ProjectName {get;set;} 

        
        public Category? Category {get;set;}

        public List<AffiliatedAgency>? AffiliatedAgencies {get;set;}

        public string? Description {get;set;}

        public DateOnly StartTime {get;set;}
        public DateOnly CompletionTime {get;set;}

        public string? TotalBudget{get;set;}

        public double CompletionPercentage {get;set;}

        public List<Location>? Locations {get;set;}
    }
}