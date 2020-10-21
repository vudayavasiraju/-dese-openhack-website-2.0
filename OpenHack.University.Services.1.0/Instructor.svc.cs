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

    public class Instructor : IInstructor
    {


        public Instructor()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["OpenHackUniversity"].ConnectionString;
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<SchoolContext>().UseSqlServer(connectionString);
            var _schoolContext = new SchoolContext(builder.Options);
            DbInitializer.Initialize(_schoolContext);
        }

        private SchoolContext GetSchoolContext()
        {
            SchoolContext _schoolContext;
            var connectionString = ConfigurationManager.ConnectionStrings["OpenHackUniversity"].ConnectionString;
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<SchoolContext>().UseSqlServer(connectionString);
            _schoolContext = new SchoolContext(builder.Options);

            return _schoolContext;
        }


        public Contract.Instructor Create(Contract.Instructor instructorToCreate)
        {
            var _schoolContext = GetSchoolContext();
            var instructorToCreateModel = new OpenHack.University.Services.Models.Instructor()
            {
               FirstMidName = instructorToCreate.FirstMidName,
             
               HireDate = instructorToCreate.HireDate,
               ID = instructorToCreate.ID,
               LastName = instructorToCreate.LastName
            };
            _schoolContext.Instructors.Add(instructorToCreateModel);
            _schoolContext.SaveChanges();
            return GetById(instructorToCreateModel.ID);
        }

        public Contract.Instructor Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Contract.Instructor GetById(int id)
        {
            var _schoolContext = GetSchoolContext();
            var instructorModel = _schoolContext.Instructors.Where(s => s.ID == id)
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Department).SingleOrDefault();
            var instructurContract = instructorModel.GetContract();
            return instructurContract;
        }

        public Contract.Instructor Modify(Contract.Instructor instructorToModify)
        {
            throw new NotImplementedException();
        }

        public List<Contract.Instructor> Search()
        {
            var _schoolContext = GetSchoolContext();
            var instructorListContract = new List<Contract.Instructor>();
            var instructorList = _schoolContext.Instructors;
            foreach (var instructor in instructorList)
            {
                instructorListContract.Add(GetById(instructor.ID));
            }

            return instructorListContract;
        }
    }
}
