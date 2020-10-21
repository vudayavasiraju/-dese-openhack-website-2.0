using OpenHack.University.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace OpenHack.University.Pages.Instructors
{
    public class DeleteModel : PageModel
    {


        [BindProperty]
        public Instructor Instructor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructorClient = new OpenHack.University.Services.Instructor.InstructorClient();
            var instructorContract = await instructorClient.GetByIdAsync(id.Value);

            Instructor = new Instructor()
            {
                FirstMidName = instructorContract.FirstMidName,
                HireDate = instructorContract.HireDate,
                ID = instructorContract.ID,
                LastName = instructorContract.LastName,

            };


            if (Instructor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //Instructor instructor = await _context.Instructors
            //    .Include(i => i.CourseAssignments)
            //    .SingleAsync(i => i.ID == id);

            //if (instructor == null)
            //{
            //    return RedirectToPage("./Index");
            //}

            //var departments = await _context.Departments
            //    .Where(d => d.InstructorID == id)
            //    .ToListAsync();
            //departments.ForEach(d => d.InstructorID = null);

            //_context.Instructors.Remove(instructor);

            //await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
