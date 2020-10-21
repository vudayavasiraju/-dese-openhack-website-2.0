
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OpenHack.University.Models;
using System.Threading.Tasks;

namespace OpenHack.University.Pages.Courses
{
    public class CreateModel : DepartmentNamePageModel
    {
        private readonly string webserviceUrl;

        public CreateModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:CourseUrl").Value;
        }

        public IActionResult OnGet()
        {
            var courseClient = new Services.Course.CourseClient();
            courseClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);

            var departmentClient = new Services.Department.DepartmentClient();
            departmentClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);

            PopulateDepartmentsDropDownList(departmentClient);
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyCourse = new Course();
            var courseClient = new Services.Course.CourseClient();
            courseClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);

            var departmentClient = new Services.Department.DepartmentClient();
            departmentClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);

            if (await TryUpdateModelAsync<Course>(
                 emptyCourse,
                 "course",   // Prefix for form value.
                 s => s.CourseID, s => s.DepartmentID, s => s.Title, s => s.Credits))
            {
                await courseClient.CreateAsync(new Services.Contract.Course()
                { 
                    Credits = emptyCourse.Credits,
                    DepartmentID = emptyCourse.DepartmentID,
                    Title = emptyCourse.Title,
                });
                
                return RedirectToPage("./Index");
            }

            //// Select DepartmentID if TryUpdateModelAsync fails.
            PopulateDepartmentsDropDownList(departmentClient, emptyCourse.DepartmentID);
            return Page();
        }


        
    }
}