using System.Drawing;
using GeoAPI.Geometries;

namespace core.Entities.Model
{
    public class Location : BaseEntity
    {
        // public IPoint Point {get;set;}
        public double Latitude {get;set;}
        public double Logitude {get;set;}

        public int ProjectId {get;set;}
        public Project Project {get;set;}

    }
}