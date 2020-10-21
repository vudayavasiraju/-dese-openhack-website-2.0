
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenHack.University.Models;
using OpenHack.University.Models.SchoolViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OpenHack.University.Pages
{
    public class AboutModel : PageModel
    {
       
        public AboutModel()
        {
        
        }

        public IList<EnrollmentDateGroup> Students { get; set; }

        public async Task OnGetAsync()
        {
            //IQueryable<EnrollmentDateGroup> data =
            //    from student in _context.Students
            //    group student by student.EnrollmentDate into dateGroup
            //    select new EnrollmentDateGroup()
            //    {
            //        EnrollmentDate = dateGroup.Key,
            //        StudentCount = dateGroup.Count()
            //    };

            //Students = await data.AsNoTracking().ToListAsync();


            var studentClient = new OpenHack.University.Services.Student.StudentClient();
            var studentContractList = studentClient.SearchAsync().Result.ToList();

            Students = new List<EnrollmentDateGroup>();
            foreach (var student in studentContractList)
            {
                Students.Add(item: new EnrollmentDateGroup() { EnrollmentDate = student.EnrollmentDate});
            }
        }
    }
}
