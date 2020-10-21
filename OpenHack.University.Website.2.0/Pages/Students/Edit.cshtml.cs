
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OpenHack.University.Models;

namespace OpenHack.University.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly string webserviceUrl;

        public EditModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:StudentUrl").Value;
        }


        [BindProperty]
        public Student Student { get; set; }

        #region snippet_OnGetPost
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var studentClient = new OpenHack.University.Services.Student.StudentClient();
            studentClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);

            var studentContract = await  studentClient.GetByIdAsync(id.Value);

            Student = new Student()
            {
                ID = studentContract.ID, 
                LastName = studentContract.LastName, 
                FirstMidName = studentContract.FirstMidName, 
                EnrollmentDate = studentContract.EnrollmentDate };

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var studentClient = new OpenHack.University.Services.Student.StudentClient();
            studentClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);

            var studentToUpdateContract = await studentClient.GetByIdAsync(id);

            if (studentToUpdateContract == null)
            {
                return NotFound();
            }
            var studentToUpdate = new Student()
            {
                ID = studentToUpdateContract.ID,
                LastName = studentToUpdateContract.LastName,
                FirstMidName = studentToUpdateContract.FirstMidName,
                EnrollmentDate = studentToUpdateContract.EnrollmentDate
            };


            if (await TryUpdateModelAsync<Student>(
                studentToUpdate,
                "student",
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
                await studentClient.ModifyAsync(new Services.Contract.Student()
                {
                    ID = studentToUpdate.ID,
                    EnrollmentDate = studentToUpdate.EnrollmentDate,
                    FirstMidName = studentToUpdate.FirstMidName,
                    LastName = studentToUpdate.LastName
                });
                return RedirectToPage("./Index");
            }

            return Page();
        }
        #endregion

      
    }
}
