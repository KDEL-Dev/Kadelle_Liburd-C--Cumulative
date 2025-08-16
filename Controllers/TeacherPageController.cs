using Microsoft.AspNetCore.Mvc;
using Kadelle_Liburd_C__Cumulative.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;


namespace Kadelle_Liburd_C__Cumulative.Controllers
{

    public class TeacherPageController : Controller
    {
        private readonly TeacherAPIController _api;

        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        //GET: TeacherPage/List
        [HttpGet]

        public IActionResult List(string searchKey)
        {

            List<Teacher> teachers = _api.ListInformationTeachers(searchKey);

            return View(teachers);
        }

        //GET: TeacherPage/Show{id}

        public IActionResult Show(int id)
        {
            //figure out how to get the id to bring in information on a single teacher
            Teacher SelectedTeacher = _api.FindTeacher(id);

            //SelectedTeacher.teacherid = 5;

            return View(SelectedTeacher);
        }


        //GET: TeacherPage/Add
        [HttpGet]

        public IActionResult Add(int id)
        {
            return View();
        }

        //POST:TeacherPage/Create
        [HttpPost]
        public IActionResult Create(Teacher NewTeacher)
        {
            int teacherId = _api.AddTeacher(NewTeacher);
            return RedirectToAction("Show", new { id = teacherId });
        }

        //GET : TeacherPage/DeleteConfirm/{id}
        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);
            return View(SelectedTeacher);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            int teacherId = _api.DeleteTeacher(id);
            return RedirectToAction("List");
        }




        [HttpGet]
        public IActionResult Edit(int id)
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);
            return View(SelectedTeacher);
        }

        [HttpPost]
        public IActionResult Update(int id, string teacherfname, string teacherlname, string employeenumber, decimal salary, DateTime hiredate)
        {
            Teacher UpdatedTeacher = new Teacher();
            UpdatedTeacher.teacherfname = teacherfname;
            UpdatedTeacher.teacherlname = teacherlname;
            UpdatedTeacher.employeenumber = employeenumber;
            UpdatedTeacher.salary = salary;
            UpdatedTeacher.hiredate = hiredate; 

            _api.UpdateTeacher(id, UpdatedTeacher);

            return RedirectToAction("Show", new { id = id });
        }


    }


}

        
        

