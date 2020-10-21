
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenHack.University.Models;
using System.Threading.Tasks;

namespace OpenHack.University.Pages.Courses
{
    public class EditModel : DepartmentNamePageModel
    {
        private readonly string webserviceUrl;

        public EditModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:CourseUrl").Value;
        }


        [BindProperty]
        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClient = new OpenHack.University.Services.Course.CourseClient();
            courseClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);

            var courseContract = await courseClient.GetByIdAsync(id.Value);

            Course = new Course() { CourseID = courseContract.CourseID, Credits = courseContract.Credits, DepartmentID = courseContract.DepartmentID, Title = courseContract.Title };

            if (Course == null)
            {
                return NotFound();
            }

            var departmentClient = new OpenHack.University.Services.Department.DepartmentClient();

            // Select current DepartmentID.
            PopulateDepartmentsDropDownList(departmentClient, Course.DepartmentID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var courseToUpdate = await _context.Courses.FindAsync(id);

            //if (courseToUpdate == null)
            //{
            //    return NotFound();
            //}

            //if (await TryUpdateModelAsync<Course>(
            //     courseToUpdate,
            //     "course",   // Prefix for form value.
            //       c => c.Credits, c => c.DepartmentID, c => c.Title))
            //{
            //    await _context.SaveChangesAsync();
            //    return RedirectToPage("./Index");
            //}

            //// Select DepartmentID if TryUpdateModelAsync fails.
            //PopulateDepartmentsDropDownList(_context, courseToUpdate.DepartmentID);
            return Page();
        }       
    }
}
