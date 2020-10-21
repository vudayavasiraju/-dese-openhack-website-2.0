#region snippet_All

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OpenHack.University.Models;
using OpenHack.University.Models.SchoolViewModels;
using System.Collections.Generic;
using System.Linq;

namespace OpenHack.University.Pages.Instructors
{
    public class InstructorCoursesPageModel : PageModel
    {
        private readonly string webserviceUrl;

        public InstructorCoursesPageModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:CourseUrl").Value;
        }



        public List<AssignedCourseData> AssignedCourseDataList;

        public void PopulateAssignedCourseData(Instructor instructor)
        {
            var courseClient = new OpenHack.University.Services.Course.CourseClient();
            courseClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);
            var courseListContract = courseClient.SearchAsync().Result;
            var allCourses = new List<Course>();

            foreach(var courseContract in courseListContract)
            {
                allCourses.Add(new Course()
                {
                    CourseID = courseContract.CourseID,
                    Credits = courseContract.Credits,
                    DepartmentID = courseContract.DepartmentID,
                    Title = courseContract.Title
                });
            }



            var instructorCourses = new HashSet<int>(
                instructor.CourseAssignments.Select(c => c.CourseID));
            AssignedCourseDataList = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                AssignedCourseDataList.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
        }

        public void UpdateInstructorCourses( 
            string[] selectedCourses, Instructor instructorToUpdate)
        {
            //#region snippet_IfNull
            //if (selectedCourses == null)
            //{
            //    instructorToUpdate.CourseAssignments = new List<CourseAssignment>();
            //    return;
            //}
            //#endregion

            //var selectedCoursesHS = new HashSet<string>(selectedCourses);
            //var instructorCourses = new HashSet<int>
            //    (instructorToUpdate.CourseAssignments.Select(c => c.Course.CourseID));
            //foreach (var course in context.Courses)
            //{
            //    #region snippet_UpdateCourses
            //    if (selectedCoursesHS.Contains(course.CourseID.ToString()))
            //    {
            //        if (!instructorCourses.Contains(course.CourseID))
            //        {
            //            instructorToUpdate.CourseAssignments.Add(
            //                new CourseAssignment
            //                {
            //                    InstructorID = instructorToUpdate.ID,
            //                    CourseID = course.CourseID
            //                });
            //        }
            //    }
            //    #endregion
            //    #region snippet_UpdateCoursesElse
            //    else
            //    {
            //        if (instructorCourses.Contains(course.CourseID))
            //        {
            //            CourseAssignment courseToRemove
            //                = instructorToUpdate
            //                    .CourseAssignments
            //                    .SingleOrDefault(i => i.CourseID == course.CourseID);
            //            context.Remove(courseToRemove);
            //        }
            //    }
            //    #endregion
            //}
        }
    }
}
#endregion
