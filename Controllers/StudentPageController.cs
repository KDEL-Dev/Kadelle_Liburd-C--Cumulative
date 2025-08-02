using Microsoft.AspNetCore.Mvc;
using Kadelle_Liburd_C__Cumulative.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

/*

namespace Kadelle_Liburd_C__Cumulative.Controllers
{
    public class StudentPageController : Controller
    {
        
        private readonly StudentAPIController _api;

        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }

        //GET: TeacherPage/List
        /*
        [HttpGet]

        public IActionResult List(string searchKey)
        {
            List<Student> Students = _api.ListInformationStudent(searchKey);

            return View(Students);
        }
        

//GET: TeacherPage/Show{id}


public IActionResult Show(int id)
{
    //figure out how to get the id to bring in information on a single teacher
    Teacher SelectedStudent = _api.FindStudent(id);

    //SelectedTeacher.teacherid = 5;

    return View(SelectedStudent);
}


//GET: TeacherPage/Add
[HttpGet]

public IActionResult Add(int id)
{
    return View();
}

//POST:TeacherPage/Create
[HttpPost]
public IActionResult Create(Teacher NewStudent)
{
    int teacherId = _api.AddStudent(NewStudent);
    return RedirectToAction("Show", new { id = studentId });
}

//GET : TeacherPage/DeleteConfirm/{id}
[HttpGet]
public IActionResult DeleteConfirm(int id)
{
    Teacher SelectedStudent = _api.FindStudent(id);
    return View(SelectedStudent);
}

[HttpPost]
public IActionResult Delete(int id)
{
    int teacherId = _api.DeleteStudent(id);
    return RedirectToAction("List");
}
}

}






*/