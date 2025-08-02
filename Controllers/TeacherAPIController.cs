using Kadelle_Liburd_C__Cumulative.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using Mysqlx.Resultset;
using System;
using System.Runtime.InteropServices.Marshalling;

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
        [Route(template: "ListInformationTeachers")]

        //NEW Added null to search key
        public List<Teacher> ListInformationTeachers(string searchKey = null)
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
                string query = "select * from teachers where teacherid=" + TeacherId; //REMOVED THIS SECTION!!!


                //opens the connection to the database
                Connection.Open();

                MySqlCommand command = Connection.CreateCommand();
                //executes our query
                command.CommandText = query; //TURNED THIS OFF, NEW CODE BELOW


                using (MySqlDataReader ResultSet = command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        //Here is where we collect our information after selecting our teacherId
                        SelectedTeacher.teacherid = Convert.ToInt32(ResultSet["teacherid"]);
                        SelectedTeacher.teacherfname = ResultSet["teacherfname"].ToString();
                        SelectedTeacher.teacherlname = ResultSet["teacherlname"].ToString();
                        SelectedTeacher.employeenumber = ResultSet["employeenumber"].ToString();
                        SelectedTeacher.hiredate = DateTime.Parse(ResultSet["hiredate"].ToString());
                        SelectedTeacher.salary = decimal.Parse(ResultSet["salary"].ToString());

                    }
                }
            }
            //returns information on a single teacher
            return SelectedTeacher;
        }

        /// <summary>
        /// This POST request will be used to add a new teachers information
        /// </summary>
        /// <param name="TeacherData"></param>
        /// <returns>Adds a new teacher</returns>
        [HttpPost(template: "AddTeacher")]
        public int AddTeacher([FromBody] Teacher TeacherData)
        {
            //Query that I will use to add a new teachers information.
            string query = "insert into teachers (teacherfname,teacherlname,employeenumber,hiredate,salary) values (@teacherfname,@teacherlname,@employeenumber,@hiredate,@salary)";

            int teacherId = -1;

            
            using (MySqlConnection connection = _context.AccessDatabase())
            {
                //opens connection to the database
                connection.Open();

                MySqlCommand command = connection.CreateCommand();

                //excutes the query we stated above
                command.CommandText = query;
                //Parameters that will be manually added except for @hiredate
                command.Parameters.AddWithValue("@teacherfname", TeacherData.teacherfname);
                command.Parameters.AddWithValue("@teacherlname", TeacherData.teacherlname);
                command.Parameters.AddWithValue("@employeenumber", TeacherData.employeenumber);
                command.Parameters.AddWithValue("@hiredate", DateTime.Now);
                command.Parameters.AddWithValue("@salary", TeacherData.salary);

                command.ExecuteNonQuery();
                 teacherId = Convert.ToInt32(command.LastInsertedId);
                //returns the id of the teacher that was last added
                return teacherId;

            }

        
        }

        /// <summary>
        /// This Delete Request will remove a teachers information from our database.
        /// </summary>
        /// <example>GET api/Teacher/DeleteTeacher/{TeacherId} -> /DeleteTeacher/{15} = Teacher with the id of 15 will be deleted from the database</example>
        /// <returns>Deletes a teacher of your choice</returns>
        [HttpDelete(template:"DeleteTeacher/{teacherId}")]
        public int DeleteTeacher(int teacherId)
        {
            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();

                MySqlCommand command = connection.CreateCommand();

                //This is the query that will delete a teacher based off the teacherid you input.
                command.CommandText = "delete from teachers where teacherid=@id";
                command.Parameters.AddWithValue("@id", teacherId);
                return command.ExecuteNonQuery();
            }
        }
        

    }

}

