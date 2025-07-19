using Kadelle_Liburd_C__Cumulative.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Kadelle_Liburd_C__Cumulative.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Should return a list of students along with their information
        /// </summary>
        /// <example>GET api/Student/ListInformationStudents -> 
        /// StudentId: "11", StudentFName: "Grant" StudentLName: "Wallace", StudentNumber: "N2315" EnrolDate: "2014-06-25"
        /// StudentId: "12", StudentFName: "Kayla" StudentLName: "Green", StudentNumber: "N2626", EnrolDate: "2017-10-04"
        /// StudentId: "13", StudentFName: "Susan" StudentLName: "Campbell", StudentNumber: "N2142", EnrolDate: "2015-05-09"
        /// </example>
        /// <returns>Returns a list of students information which include categories like: "first name", "last name", "student number", student id, etc</returns>
        [HttpGet]
        [Route(template: "ListInformationStudents")]


        public List<string> InformationStudents()
        {
            //Create empty list for teacher name
            List<string> InformationStudents = new List<string>();


            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                //Creates a new command (query) for the database
                MySqlCommand Command = connection.CreateCommand();

                //MySql Query goes here
                //can place the select from teachers into it's own variable if you would like and have  commandtext equal that
                Command.CommandText = "select * from students";

                //Places result of Query above into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loops through the result set
                    while (ResultSet.Read())
                    {
                        int studentId = Int32.Parse(ResultSet["studentid"].ToString());
                        string studentfname = ResultSet["studentfname"].ToString();
                        string studentLname = ResultSet["studentlname"].ToString();
                        string studentNumber = ResultSet["studentnumber"].ToString();
                        DateTime enrolDate = DateTime.Parse(ResultSet["enroldate"].ToString());

                        string InformationStudent = $"{studentId} {studentfname} {studentLname} {studentNumber} {enrolDate} ";

                        //Adds names to the empty list we created above
                        InformationStudents.Add(InformationStudent);

                    }
                }
            }

            return InformationStudents;


        }
    }
}
