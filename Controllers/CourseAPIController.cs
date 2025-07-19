using Kadelle_Liburd_C__Cumulative.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Kadelle_Liburd_C__Cumulative.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public CourseAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Should give you a list of courses and their information
        /// </summary>
        /// <example>GET Api/Course/ListInformationCourses ->
        /// courseid: "13", courseCode: "http5306", teacherId: "13", StartDate: "2016-05-05", finishDate: "2018-04-16", courseName: "FrontEnd Development" 
        /// courseid: "14", courseCode: "http5410", teacherId: "14", StartDate: "2012-09-12". finishDate: "2019-08-20", courseName:"BackEnd Development"
        /// </example>
        /// <returns>Should return Course Information such as "Courseid", "Coursecode", "Coursename",etc</returns>

        [HttpGet]
        [Route(template: "ListInformationCourses")]


        public List<string> InformationCourses()
        {
            //Create empty list for teacher name
            List<string> InformationCourses = new List<string>();


            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                //Creates a new command (query) for the database
                MySqlCommand Command = connection.CreateCommand();

                //MySql Query goes here
                //can place the select from teachers into it's own variable if you would like and have  commandtext equal that
                Command.CommandText = "select * from courses";

                //Places result of Query above into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loops through the result set
                    while (ResultSet.Read())
                    {
                        int courseId = Int32.Parse(ResultSet["courseid"].ToString());
                        string courseCode = ResultSet["coursecode"].ToString();
                        int teacherId = Int32.Parse(ResultSet["teacherid"].ToString());
                        DateTime startDate = DateTime.Parse(ResultSet["startdate"].ToString());
                        DateTime finishDate = DateTime.Parse(ResultSet["finishdate"].ToString());
                        string courseName = ResultSet["coursename"].ToString();

                        string InformationCourse = $"{courseId} {courseCode} {teacherId} {startDate} {finishDate} {courseName} ";

                        //Adds names to the empty list we created above
                        InformationCourses.Add(InformationCourse);

                    }
                }
            }

            return InformationCourses;
        }
    }
}
