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
    public class IndexModel : PageModel
    {
        private readonly string webserviceUrl;

   
        public IndexModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:DepartmentUrl").Value;

        }


        public IList<Department> Department { get;set; }

        public async Task OnGetAsync()
        {
            var departmentClient = new OpenHack.University.Services.Department.DepartmentClient();
            departmentClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);



            var departmentContractList = await departmentClient.SearchAsync();
            Department = new List<Department>();

            foreach (var departmentContract in departmentContractList)
            {
                var department = new Department()
                {
                    Budget = departmentContract.Budget,
                    DepartmentID = departmentContract.DepartmentID,
                    InstructorID = departmentContract.InstructorID,
                    Name = departmentContract.Name,
                    StartDate = departmentContract.StartDate,

                };

                if(departmentContract.Administrator != null)
                {
                    department.Administrator = new Instructor()
                    {
                        FirstMidName = departmentContract.Administrator.FirstMidName,
                        LastName = departmentContract.Administrator.LastName
                    };
                }
                Department.Add(department);
            }

          
        }
    }
}
