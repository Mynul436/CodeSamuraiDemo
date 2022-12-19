using System.Drawing;
using System.Globalization;
using core.Entities.Model;
using core.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;

namespace api.Data
{
    public class Seed
    {
        // public static async Task ClearConnections(DataContext context)
        // {

        // }

        public static async Task SeedProjects(IUnitOfWork _unitOfWork)
        {
            if(await _unitOfWork.ProjectRepository.isExitAsync(x => true)) return ;


            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = false
            };

          //  Console.WriteLine("Ok");

            using var streamReader = File.OpenText("Data/SampleCSV/projects.csv");

            using var csvReader = new CsvReader(streamReader, csvConfig);

            string value;

            

            while (csvReader.Read())
            {
                var project = new Project();

                for (int i = 0; csvReader.TryGetField<string>(i, out value); i++)
                {
                    if(value == "project_name" || value == "category" 

                        || value == "affiliated_agency"

                        || value == "description"

                        || value == "project_start_time"

                        || value == "project_completion_time"

                        || value == "total_budget"

                        || value == "completion_percentage"

                        || value == "location_coordinates"
                        ) continue;


                   //  Console.WriteLine($"{value} {i}");
                    if(i == 0) project.ProjectName = value;
                    if(i == 1) {
                        var category = new Category();
                        category.Name = value;
                        project.Category = category;
                    }
                    if(i == 2) {
                        String agencey = "";

                        var agenceyList = new List<AffiliatedAgency>();

                        int countBreakPoint = 0;
                       // Console.WriteLine(value);
                        foreach(var ch in value)
                        {
                            if(ch == ',') countBreakPoint++;
                        }
                        
                        int idx = 0;

                        for(int loop = 0; loop <= countBreakPoint; loop++)
                        {
                            for(; idx < value.Length; idx++)
                            {
                                if(value[idx] == ','){
                                    idx++;
                                    break;
                                }

                            //  Console.WriteLine(value[idx]);
                                agencey = agencey + Char.ToString(value[idx]);
                            }

                          //  Console.WriteLine(loop + " "+  agencey);
                            agenceyList.Add(new AffiliatedAgency{
                                Name = agencey
                            });

                            agencey = "";
                        }
                        
                        project.AffiliatedAgencies = agenceyList;
                    }
                    if(i == 3) project.Description = value;
                    if(i == 4) project.StartTime = DateOnly.Parse(value);
                    if(i == 5) project.CompletionTime = DateOnly.Parse(value);
                    if(i == 6) project.TotalBudget = value;

                    if(i == 7) {
                        String percentage = "";
                        foreach(var ch in value)
                        {
                            if(ch == '%') break;
                            percentage += ch;
                        }

                        project.CompletionPercentage = Double.Parse(percentage);
                        // Console.WriteLine(percentage);
                        // Console.WriteLine(project.CompletionPercentage);
                    }

                    if(i == 8)
                    {
                       // Console.WriteLine(value);

                        String locations = "";
                        var locationList = new List<string>();
                        var pLocationList = new List<Location>();

                        foreach(var ch in value){
                            if(ch == '(' || ch == ',' || ch == ')' || ch == ' ') {
                              //  Console.WriteLine(locations);
                                if(locations !="") {
                                    locationList.Add(locations);
                                }
                                locations =""; 
                                continue;
                            }

                            locations += Char.ToString(ch);
                        }

                       //var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                        for(int loc = 0; loc < locationList.Count(); loc += 2)
                        {
                          //  Console.WriteLine(locationList[loc] + ", " + locationList[loc+1]);
                            
                           // var locat = new NetTopologySuite.Geometries.Point(13.003725d, 55.604870d) { SRID = 4326 };
                            pLocationList.Add(new Location(){
                               Latitude = double.Parse(locationList[loc]),
                               Logitude = double.Parse(locationList[loc+1])
                            });
                        }

                        project.Locations = pLocationList;
                    }

                  //  if(i == 6) project.CompletionPercentage = Double.Parse(value)

                }
              _unitOfWork.ProjectRepository.AddAsync(project);
             // await _unitOfWork.CommitAsync()
            }

            await _unitOfWork.CommitAsync();
        }
    }
}