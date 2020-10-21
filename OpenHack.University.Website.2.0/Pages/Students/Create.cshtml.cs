using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OpenHack.University.Models;

namespace OpenHack.University.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly string webserviceUrl;
        public CreateModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:StudentUrl").Value;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        #region snippet_OnPostAsync
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyStudent = new Student();

            var studentClient = new Services.Student.StudentClient();
            studentClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);
            
            if (await TryUpdateModelAsync<Student>(
                emptyStudent,
                "student",   // Prefix for form value.
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
                studentClient.Create(new Services.Contract.Student() { EnrollmentDate = emptyStudent.EnrollmentDate, FirstMidName = emptyStudent.FirstMidName, LastName = emptyStudent.LastName });
                return RedirectToPage("./Index");
            }
            #endregion

            return Page();
        }

    }
}