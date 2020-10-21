
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenHack.University.Services.Department;

namespace OpenHack.University.Pages.Courses
{
    public class DepartmentNamePageModel : PageModel
    {
        public SelectList DepartmentNameSL { get; set; }

        public void PopulateDepartmentsDropDownList(DepartmentClient departmentClient,
            object selectedDepartment = null)
        {
            var departmentsQuery = departmentClient.SearchAsync().Result;

            DepartmentNameSL = new SelectList(departmentsQuery,
                        "DepartmentID", "Name", selectedDepartment);
        }
    }
}