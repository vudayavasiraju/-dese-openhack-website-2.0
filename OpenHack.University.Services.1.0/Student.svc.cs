using Microsoft.EntityFrameworkCore;
using OpenHack.University.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace OpenHack.University.Services._1._0
{
    public class Student : IStudent
    {
        public Student()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["OpenHackUniversity"].ConnectionString;
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<SchoolContext>().UseSqlServer(connectionString);
            var schoolContext = new SchoolContext(builder.Options);
            DbInitializer.Initialize(schoolContext);
        }

        private SchoolContext GetSchoolContext()
        {
            SchoolContext _schoolContext;
            var connectionString = ConfigurationManager.ConnectionStrings["OpenHackUniversity"].ConnectionString;
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<SchoolContext>().UseSqlServer(connectionString);
            _schoolContext = new SchoolContext(builder.Options);

            return _schoolContext;
        }


        public Contract.Student Create(Contract.Student studentToCreate)
        {
            var studentToCreateModel = new Models.Student()
            {
                LastName = studentToCreate.LastName,
                EnrollmentDate = studentToCreate.EnrollmentDate,
                FirstMidName = studentToCreate.FirstMidName,
            };

            var _schoolContext = GetSchoolContext();

            _schoolContext.Students.Add(studentToCreateModel);
            _schoolContext.SaveChanges();

            return GetById(studentToCreateModel.ID);
        }

        public Contract.Student Delete(int id)
        {
            var _schoolContext = GetSchoolContext();
            var student = _schoolContext.Students.Find(id);
            if (student != null)
            {
                _schoolContext.Students.Remove(student);
                _schoolContext.SaveChanges();
            }

            return new Contract.Student() { };
        }

        public Contract.Student GetById(int id)
        {
            var _schoolContext = GetSchoolContext();
            var studentModel = _schoolContext.Students.Where(s => s.ID == id).Include(x => x.Enrollments).ThenInclude(e => e.Course).SingleOrDefault();


            var studentContract = studentModel.GetContract();
            return studentContract;
        }

        public Contract.Student Modify(Contract.Student studentToModify)
        {
            var _schoolContext = GetSchoolContext();
            var currentStudent = _schoolContext.Students.FirstOrDefault(s => s.ID == studentToModify.ID);

            currentStudent.LastName = studentToModify.LastName != null ? studentToModify.LastName : currentStudent.LastName;
            currentStudent.FirstMidName = studentToModify.FirstMidName != null ? studentToModify.FirstMidName : currentStudent.FirstMidName;
            currentStudent.EnrollmentDate = studentToModify.EnrollmentDate != null ? studentToModify.EnrollmentDate : currentStudent.EnrollmentDate;
            _schoolContext.Update(currentStudent);
            _schoolContext.SaveChanges();

            return GetById(studentToModify.ID);
        }

        public List<Contract.Student> Search()
        {
            var _schoolContext = GetSchoolContext();
            var studentListContract = new List<Contract.Student>();
            var studentList = _schoolContext.Students;
            foreach(var student in studentList)
            {
                studentListContract.Add(GetById(student.ID));
            }
            return studentListContract;
        }
    }
}
