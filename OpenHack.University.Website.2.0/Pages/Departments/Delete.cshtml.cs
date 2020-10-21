
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OpenHack.University.Models;

namespace OpenHack.University.Pages.Departments
{
    public class DeleteModel : PageModel
    {
       
        [BindProperty]
        public Department Department { get; set; }
        public string ConcurrencyErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, bool? concurrencyError)
        {
            var departmentClient = new OpenHack.University.Services.Department.DepartmentClient();
            var departmentContract = await departmentClient.GetByIdAsync(id);


            Department = new Department()
            {
                Budget = departmentContract.Budget,
                DepartmentID = departmentContract.DepartmentID,
                InstructorID = departmentContract.InstructorID,
                Name = departmentContract.Name,
                StartDate = departmentContract.StartDate
            };

            if (Department == null)
            {
                 return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ConcurrencyErrorMessage = "The record you attempted to delete "
                  + "was modified by another user after you selected delete. "
                  + "The delete operation was canceled and the current values in the "
                  + "database have been displayed. If you still want to delete this "
                  + "record, click the Delete button again.";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            //try
            //{
            //    if (await _context.Departments.AnyAsync(
            //        m => m.DepartmentID == id))
            //    {
            //        // Department.rowVersion value is from when the entity
            //        // was fetched. If it doesn't match the DB, a
            //        // DbUpdateConcurrencyException exception is thrown.
            //        _context.Departments.Remove(Department);
            //        await _context.SaveChangesAsync();
            //    }
                return RedirectToPage("./Index");
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    return RedirectToPage("./Delete",
            //        new { concurrencyError = true, id = id });
            //}
        }
    }
}
