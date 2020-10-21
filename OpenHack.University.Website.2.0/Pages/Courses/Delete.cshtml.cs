
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenHack.University.Models;
using System.Threading.Tasks;

namespace OpenHack.University.Pages.Courses
{
    public class DeleteModel : PageModel
    {


        private readonly string webserviceUrl;

        public DeleteModel(IConfiguration configuration)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Course = await _context.Courses.FindAsync(id);

            //if (Course != null)
            //{
            //    _context.Courses.Remove(Course);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage("./Index");
        }
    }
}
