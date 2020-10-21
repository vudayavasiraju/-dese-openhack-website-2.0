#region snippet_All

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenHack.University.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenHack.University.Pages.Departments
{
    public class EditModel : PageModel
    {


        [BindProperty]
        public Department Department { get; set; }
        // Replace ViewData["InstructorID"] 
        public SelectList InstructorNameSL { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            OpenHack.University.Services.Department.DepartmentClient departmentClient = new OpenHack.University.Services.Department.DepartmentClient();
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


            OpenHack.University.Services.Instructor.InstructorClient instructorClient = new OpenHack.University.Services.Instructor.InstructorClient();
            var instructorListContract = await instructorClient.SearchAsync();

            var instructors = new List<Instructor>();

            foreach(var instructor in instructorListContract)
            {
                instructors.Add(new Instructor()
                {
                    FirstMidName = instructor.FirstMidName,
                    HireDate = instructor.HireDate,
                    ID = instructor.ID,
                    LastName = instructor.LastName
                });
            }
            // Use strongly typed data rather than ViewData.
            InstructorNameSL = new SelectList(instructors,
                "ID", "FirstMidName");

            return Page();
        }

        #region snippet_RowVersion
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

          

            //if (departmentToUpdate == null)
            //{
            //    return HandleDeletedDepartment();
            //}

            //_context.Entry(departmentToUpdate)
            //    .Property("RowVersion").OriginalValue = Department.RowVersion;
            //#endregion

            //#region snippet_TryUpdateModel
            //if (await TryUpdateModelAsync<Department>(
            //    departmentToUpdate,
            //    "Department",
            //    s => s.Name, s => s.StartDate, s => s.Budget, s => s.InstructorID))
            //{
            //    try
            //    {
            //        await _context.SaveChangesAsync();
            //        return RedirectToPage("./Index");
            //    }
            //    catch (DbUpdateConcurrencyException ex)
            //    {
            //        var exceptionEntry = ex.Entries.Single();
            //        var clientValues = (Department)exceptionEntry.Entity;
            //        var databaseEntry = exceptionEntry.GetDatabaseValues();
            //        if (databaseEntry == null)
            //        {
            //            ModelState.AddModelError(string.Empty, "Unable to save. " +
            //                "The department was deleted by another user.");
            //            return Page();
            //        }

            //        var dbValues = (Department)databaseEntry.ToObject();
            //        await setDbErrorMessage(dbValues, clientValues, _context);

            //        // Save the current RowVersion so next postback
            //        // matches unless an new concurrency issue happens.
            //        Department.RowVersion = (byte[])dbValues.RowVersion;
            //        // Clear the model error for the next postback.
            //        ModelState.Remove("Department.RowVersion");
            //    }
            //    #endregion
            //}

            //var instructors = new List<Instructor>();

            //OpenHack.University.Services.Instructor.InstructorClient instructorClient = new OpenHack.University.Services.Instructor.InstructorClient();
            //var instructorListContract = await instructorClient.SearchAsync();

            //foreach (var instructor in instructorListContract)
            //{
            //    instructors.Add(new Instructor()
            //    {
            //        FirstMidName = instructor.FirstMidName,
            //        HireDate = instructor.HireDate,
            //        ID = instructor.ID,
            //        LastName = instructor.LastName
            //    });
            //}

            //InstructorNameSL = new SelectList(instructors,
            //    "ID", "FullName", departmentToUpdate.InstructorID);

            return Page();
        }

        private IActionResult HandleDeletedDepartment()
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

            var deletedDepartment = new Department();
            // ModelState contains the posted data because of the deletion error
            // and will overide the Department instance values when displaying Page().
            ModelState.AddModelError(string.Empty,
                "Unable to save. The department was deleted by another user.");
            InstructorNameSL = new SelectList(instructors, "ID", "FullName", Department.InstructorID);
            return Page();
        }

        //#region snippet_Error
        //private async Task setDbErrorMessage(Department dbValues,
        //        Department clientValues, SchoolContext context)
        //{

        //    if (dbValues.Name != clientValues.Name)
        //    {
        //        ModelState.AddModelError("Department.Name",
        //            $"Current value: {dbValues.Name}");
        //    }
        //    if (dbValues.Budget != clientValues.Budget)
        //    {
        //        ModelState.AddModelError("Department.Budget",
        //            $"Current value: {dbValues.Budget:c}");
        //    }
        //    if (dbValues.StartDate != clientValues.StartDate)
        //    {
        //        ModelState.AddModelError("Department.StartDate",
        //            $"Current value: {dbValues.StartDate:d}");
        //    }
        //    if (dbValues.InstructorID != clientValues.InstructorID)
        //    {
        //        Instructor dbInstructor = await _context.Instructors
        //           .FindAsync(dbValues.InstructorID);
        //        ModelState.AddModelError("Department.InstructorID",
        //            $"Current value: {dbInstructor?.FullName}");
        //    }

        //    ModelState.AddModelError(string.Empty,
        //        "The record you attempted to edit "
        //      + "was modified by another user after you. The "
        //      + "edit operation was canceled and the current values in the database "
        //      + "have been displayed. If you still want to edit this record, click "
        //      + "the Save button again.");
        //}
        #endregion
    }
}
#endregion