using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenHack.University.Models;

namespace OpenHack.University.Pages.Departments
{
    public class DetailsModel : PageModel
    {
        private readonly string webserviceUrl;

        public DetailsModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:DepartmentUrl").Value;
        }

        public Department Department { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departmentClient = new OpenHack.University.Services.Department.DepartmentClient();
            departmentClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);
            var departmentContract = await departmentClient.GetByIdAsync(id.Value);


            Department = new Department()
            {
                Budget = departmentContract.Budget,
                DepartmentID = departmentContract.DepartmentID,
                InstructorID = departmentContract.InstructorID,
                Name = departmentContract.Name,
                StartDate = departmentContract.StartDate
            };

            return Page();
        }
    }
}
