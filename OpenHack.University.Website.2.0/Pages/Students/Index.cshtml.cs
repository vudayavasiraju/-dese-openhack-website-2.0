#region snippet_All

using OpenHack.University.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OpenHack.University.Pages.Students
{
    public class IndexModel : PageModel
    {

        private readonly string webserviceUrl;
        public IndexModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:StudentUrl").Value;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public List<Student> Students { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            var studentClient = new Services.Student.StudentClient();
            studentClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);
            var studentContractList = studentClient.SearchAsync().Result.ToList();

            Students = new List<Student>();
            foreach (var studentContract in studentContractList)
            {
                var studentModel = new Student() 
                { 
                    EnrollmentDate = studentContract.EnrollmentDate, 
                    FirstMidName = studentContract.FirstMidName, 
                    LastName = studentContract.LastName, 
                    ID = studentContract.ID,
                    Enrollments = new List<Enrollment>()
                };

                Students.Add(item: studentModel);
            }
        }
    }
}
#endregion