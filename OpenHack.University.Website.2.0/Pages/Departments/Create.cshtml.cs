using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using OpenHack.University.Models;

namespace OpenHack.University.Pages.Departments
{
    public class CreateModel : PageModel
    {
        private readonly string webserviceUrl;


        public CreateModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:DepartmentUrl").Value;
        }


        public IActionResult OnGet()
        {


            OpenHack.University.Services.Instructor.InstructorClient instructorClient = new OpenHack.University.Services.Instructor.InstructorClient();

            var instructorListContract = instructorClient.SearchAsync().Result;

            var instructors = new List<Instructor>();

            foreach (var instructor in instructorListContract)
            {
                instructors.Add(new Instructor()
                {
                    FirstMidName = instructor.FirstMidName,
                    HireDate = instructor.HireDate,
                    ID = instructor.ID,
                    LastName = instructor.LastName
                });
            }

            ViewData["InstructorID"] = new SelectList(instructors, "ID", "FirstMidName");
            return Page();
        }

        [BindProperty]
        public Department Department { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Departments.Add(Department);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}