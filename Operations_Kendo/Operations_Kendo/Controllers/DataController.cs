using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Operations_Kendo.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        // Json to retrieve list of students from database
        public JsonResult Studentlist()
        {
            using (var db = new StudentEntities())
            {
                var userlist = db.Students.Select(x => new
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EnrollmentDate = x.EnrollmentDate,
                    Email = x.Email
                }).OrderBy(x => x.EnrollmentDate).ToList();
                return Json(userlist, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateStudent(Student model)
        {
            using (var db = new StudentEntities())
            {
                var UpStudent = db.Students.Where(x => x.Id == model.Id).FirstOrDefault();

                if (UpStudent != null)
                {
                    UpStudent.FirstName = model.FirstName;
                    UpStudent.LastName = model.LastName;
                    UpStudent.EnrollmentDate = model.EnrollmentDate;
                    UpStudent.Email = model.Email;
                    db.SaveChanges();
                }

                else
                {
                    return Json(new { Success = false, message = "not updated Sucessfully" });
                }
                return Json(new { Success = true, message = " updated Sucessfully" });
            }
        }


        public ActionResult DeleteStudent(int Id)
        {
            using (var db = new StudentEntities())
            {
                var deleteStudent = db.Students.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefault();
                deleteStudent.IsActive = false;
                db.SaveChanges();
                return Json(new { Success = true, Message = " deleted successfully" });
            }

        }
    }
}