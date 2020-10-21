using Microsoft.EntityFrameworkCore;
using OpenHack.University.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace OpenHack.University.Services._1._0
{

    public class Department : IDepartment
    {


        public Department()
        {
           
            var connectionString = ConfigurationManager.ConnectionStrings["OpenHackUniversity"].ConnectionString;
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<SchoolContext>().UseSqlServer(connectionString);
            var _schoolContext = new SchoolContext(builder.Options);
            DbInitializer.Initialize(_schoolContext);
        }


        public Contract.Department Create(Contract.Department departmentToCreate)
        {
            var _schoolContext = GetSchoolContext();
            var departmentToCreateModel = new OpenHack.University.Services.Models.Department()
            {
               Budget = departmentToCreate.Budget,
               InstructorID= departmentToCreate.InstructorID,
               Name = departmentToCreate.Name,
               StartDate= departmentToCreate.StartDate
            };
            _schoolContext.Departments.Add(departmentToCreateModel);
            _schoolContext.SaveChanges();
            return GetById(departmentToCreateModel.DepartmentID);
        }

        public Contract.Department Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Contract.Department GetById(int id)
        {
            var _schoolContext = GetSchoolContext();
            var departmentModel = _schoolContext.Departments.Where(s => s.DepartmentID == id).Include(d => d.Administrator).Include(d => d.Courses).SingleOrDefault();
            var deparmentContract = departmentModel.GetContract();

            

            return deparmentContract;
        }

        private SchoolContext GetSchoolContext()
        {
            SchoolContext _schoolContext;
            var connectionString = ConfigurationManager.ConnectionStrings["OpenHackUniversity"].ConnectionString;
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<SchoolContext>().UseSqlServer(connectionString);
            _schoolContext = new SchoolContext(builder.Options);

            return _schoolContext;
        }

        public Contract.Department Modify(Contract.Department departmentToModify)
        {
            var _schoolContext = GetSchoolContext();

            //var departmentToUpdate = await _context.Departments
            //  .Include(i => i.Administrator)
            //  .FirstOrDefaultAsync(m => m.DepartmentID == id);

            throw new NotImplementedException();
        }

        public List<Contract.Department> Search()
        {
            var _schoolContext = GetSchoolContext();
            var departmentListContract = new List<Contract.Department>();
            var departmentList = _schoolContext.Departments;
            foreach (var department in departmentList)
            {
                departmentListContract.Add(GetById(department.DepartmentID));
            }

            return departmentListContract;
        }
    }
}
