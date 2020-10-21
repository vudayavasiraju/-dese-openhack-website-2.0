using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using OpenHack.University.Models;

namespace OpenHack.University.Pages.Instructors
{
    public class DetailsModel : PageModel
    {
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
    }
}
