using Kadelle_Liburd_C__Cumulative.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices.Marshalling;
using Mysqlx.Resultset;

namespace Kadelle_Liburd_C__Cumulative.Controllers
{
    [Route("api/Teacher")]
    [ApiController]


    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This is should return a list of teacher name with all of their information
        /// </summary>
        /// <example> GET api/Teacher/ListInformationTeachers -> 
        /// TeacherID: "11",Name:"Betty Walker", Employee Number: "T601", Hire Date: "2014-08-04 00:00:00" Salary:"45.25"
        /// TeacherID: "12",Name:"Wendell Williams", Employee Number: "T635", Hire Date: "2018-05-13 00:00:00" Salary: "84.45"
        /// TeacherID: "13", Name: "Dale Hunter", Employee Number: "T784", Hire Date: "2019-06-04 00:00:00" Salary: "87.48"</example>
        /// <returns>Should return a list of teachers that has their Name, ID, Hire Date, Employee Number and Salary</returns>
        
        [HttpGet]
        [Route(template:"ListInformationTeachers")]

        //A previous attempt to bring up list but was getting an error where it says I can't convert a list <string> to list <modal>
        /*
        public List<string> InformationTeachers()
        {
            //Create empty list for teacher name
            List<string> InformationTeachers = new List<string>();

            
            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                //Creates a new command (query) for the database
                MySqlCommand Command = connection.CreateCommand();

                //MySql Query goes here
                //can place the select from teachers into it's own variable if you would like and have  commandtext equal that
                Command.CommandText = "select * from teachers";

                //Places result of Query above into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loops through the result set
                    while (ResultSet.Read())
                    {
                        int teacherId = Int32.Parse(ResultSet["teacherid"].ToString()); 
                        string teacherfname = ResultSet["teacherfname"].ToString ();
                        string teacherLname = ResultSet["teacherlname"].ToString();
                        string employeeNumber = ResultSet["employeenumber"].ToString() ;
                        DateTime hireDate = DateTime.Parse(ResultSet["hiredate"].ToString ()) ;
                        decimal salary = decimal.Parse(ResultSet["salary"].ToString()) ;
                       
                        string InformationTeacher = $"{teacherId} {teacherfname} {teacherLname} {employeeNumber} {hireDate} {salary} ";

                        //Adds names to the empty list we created above
                        InformationTeachers.Add(InformationTeacher);

                    }
                }
            }

            return InformationTeachers;
            
            
        }

        */

        public List<Teacher> ListInformationTeachers()
        {
            //Creates a new list called teacher
            List<Teacher> Teachers = new List<Teacher>();
               
            using (MySqlConnection connection = _context.AccessDatabase())
            {
                //Opens the connection to the database
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                //This is the query I need to get all the information for each teacher
                string query = "select * from teachers";

                //This command seems to be how we execute the SQL query
                command.CommandText = query;

                using (MySqlDataReader ResultSet = command.ExecuteReader())
                {
                    
                    while (ResultSet.Read())
                    {
                        //Gathers teachers information by using a loop and places it inside an object which in this case is "CurrentTeacher"
                        Teacher CurrentTeacher = new Teacher();

                        CurrentTeacher.teacherid = Int32.Parse(ResultSet["teacherid"].ToString());
                        CurrentTeacher.teacherfname = ResultSet["teacherfname"].ToString();
                        CurrentTeacher.teacherlname = ResultSet["teacherlname"].ToString();
                        CurrentTeacher.employeenumber = ResultSet["employeenumber"].ToString();
                        CurrentTeacher.hiredate = DateTime.Parse(ResultSet["hiredate"].ToString());
                        CurrentTeacher.salary = decimal.Parse(ResultSet["salary"].ToString());

                        //contents of CurrentTeachers will be added into Teacher List
                        Teachers.Add(CurrentTeacher);
                    }
                }
            }
            //Returns all the information
            return Teachers;
        }





        /// <summary>
        /// This GET request should bring up the information for a single teacher
        /// </summary>
        /// <param name="TeacherId"></param>       
        /// <example> GET api/Teacher/FindTeacher/{TeacherId} -> TeacherID: "11",Name:"Betty Walker", Employee Number: "T601", Hire Date: "2014-08-04 00:00:00" Salary:"45.25"</example>
        /// <returns>Should return a list of one teachers information</returns>
        [HttpGet]
        //Where users will input which teacher they would like to select
        [Route(template: "FindTeacher/{TeacherId}")]
        public Teacher FindTeacher(int TeacherId)
        {
            //Creates an object to put our information into
            Teacher SelectedTeacher = new Teacher();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                //Query for grabbing teachers information based on their ID
                string query = "select * from teachers where teacherid=" + TeacherId;
                //opens the connection to the database
                Connection.Open();

                MySqlCommand command = Connection.CreateCommand();
                //executes our query
                command.CommandText = query;    

                using (MySqlDataReader ResultSet = command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        //Here is where we collect our information after selecting our teacherId
                        SelectedTeacher.teacherid = Convert.ToInt32(ResultSet["teacherid"]);
                        SelectedTeacher.teacherfname = ResultSet["teacherfname"].ToString();
                        SelectedTeacher.teacherlname = ResultSet["teacherlname"].ToString();
                        SelectedTeacher.employeenumber = ResultSet["employeenumber"].ToString() ;
                        SelectedTeacher.hiredate = DateTime.Parse(ResultSet["hiredate"].ToString ()) ;
                        SelectedTeacher.salary = decimal.Parse(ResultSet["salary"].ToString()) ;
                        
                    }
                }
            }
            //returns information on a single teacher
            return SelectedTeacher;
        }

    }
}
