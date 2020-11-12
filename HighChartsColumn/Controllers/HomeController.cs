using HighChartsColumn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HighChartsColumn.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            MOEAdminDbEntities1 Context = new MOEAdminDbEntities1();
            //var queryables = (from schoolTable in Context.SCHOOLS_DATA
            //             join studentTable in Context.STUDENTS_DATA 
            //             on schoolTable.School_ID equals studentTable.School_ID into G
            //             select new
            //             {
            //                 Name = schoolTable.School_Name,
            //                 StudentCount = G.Select(std => std.SCHOOL_ID).Count()
            //             }).GroupBy(ent=>ent.Name);

            var result =
        from t in Context.SCHOOLS_DATA.Select(t => 
        new { Teacher = t.School_Name,school=t.School_ID }).Distinct()
        join studentTable in Context.STUDENTS_DATA.Select(e =>new { studentId=e.STUDENT_ID,studentSchool=e.SCHOOL_ID}).Distinct()
        on t.school equals studentTable.studentSchool
        group t by t.Teacher
        into g
         select new
        {
          
            TeacherCount = g.Select(t => t.Teacher).Distinct().Count(),
            SchoolCount = g.Select(t => t.school).Distinct().Count(),
        
        };
            var data = result.ToList();

            return Json(data);
        }
    }
}