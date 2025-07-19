using Microsoft.AspNetCore.Mvc;
using Kadelle_Liburd_C__Cumulative.Models;


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
        public IActionResult List()
        {
            List<Teacher> teachers = _api.ListInformationTeachers();

            return View(teachers);
        }

        //GET: TeacherPage/Show{id}
        [HttpGet]
        public IActionResult Show(int id)
        {
            //figure out how to get the id to bring in information on a single teacher
            Teacher SelectedTeacher = _api.FindTeacher(id);

            SelectedTeacher.teacherid = 5;

           return View(SelectedTeacher);
        }

    }


    

}

        
        

