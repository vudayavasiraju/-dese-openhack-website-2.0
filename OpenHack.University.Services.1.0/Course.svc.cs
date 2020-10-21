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

    public class Course : ICourse
    {
        public Course()
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

        public Contract.Course Create(Contract.Course courseToCreate)
        {
            var _schoolContext = GetSchoolContext();
            var courseToCreateModel = new University.Services.Models.Course()
            {
               Credits = courseToCreate.Credits,
               DepartmentID = courseToCreate.DepartmentID,
               Title = courseToCreate.Title,
               CourseID = courseToCreate.CourseID
            };

            _schoolContext.Courses.Add(courseToCreateModel);
            _schoolContext.SaveChanges();
            return GetById(courseToCreate.CourseID);
        }

        public Contract.Course Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Contract.Course GetById(int id)
        {
            var _schoolContext = GetSchoolContext();
            var courceModel = _schoolContext.Courses.Where(c => c.CourseID == id).Include(c => c.Department).SingleOrDefault();
            var courseContract = new Contract.Course() { CourseID = courceModel.CourseID, Credits = courceModel.Credits, DepartmentID = courceModel.DepartmentID, Title = courceModel.Title};
            if (courceModel.Department != null)
            {
                courseContract.Department = new Contract.Department()
                {
                    Budget = courceModel.Department.Budget,
                    DepartmentID = courceModel.DepartmentID,
                    InstructorID = courceModel.Department.InstructorID,
                    Name = courceModel.Department.Name,
                    StartDate = courceModel.Department.StartDate

                };
            }
            return courseContract;
        }

        public Contract.Course Modify(Contract.Course courseToModify)
        {
            throw new NotImplementedException();
        }

        public List<Contract.Course> Search()
        {
            var _schoolContext = GetSchoolContext();
            var courseListContract = new List<Contract.Course>();
            var coursesList = _schoolContext.Courses;
            foreach (var course in coursesList)
            {
                courseListContract.Add(GetById(course.CourseID));
            }

            return courseListContract;
        }
    }
}
