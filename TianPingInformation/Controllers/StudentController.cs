using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TianPingInformation.Models.POCO;
using TianPingInformation.Models;
using TianPingInformation.Models.Security;
using MvcPaging;

namespace TianPingInformation.Controllers
{
    [HandleError]
    [MyAuth]
    public class StudentController : Controller
    {
        private TianPingInfoDBContext db = new TianPingInfoDBContext();

        private const int defaultPageSize = 2;       // 默认页面大小
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        /// <summary>
        /// 带分页的学员信息列表，并且按相应的字段进行查询
        /// </summary>
        /// <param name="student_name">学员姓名</param>
        /// <param name="id_number">身份证号</param>
        /// <param name="start_date">发证日期起始时间</param>
        /// <param name="end_date">发证日期结束时间</param>
        /// <param name="page">当前的页数</param>
        /// <returns></returns>
        public ActionResult AjaxIndex(string student_name, string id_number, DateTime? start_date, DateTime? end_date, int? page)
        {
            ViewData["student_name"] = student_name;
            ViewData["id_number"] = id_number;
            ViewData["start_date"] = start_date;
            ViewData["end_date"] = end_date;
            int currentPageIndex = page.HasValue ? page.Value : 1;
            IList<Student> students = db.Students.ToList();

            // 根据姓名查询
            if (string.IsNullOrWhiteSpace(student_name))
            {
                students = students.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                students = students.Where(s => s.Name.ToLower().Contains(student_name.ToLower())).ToPagedList(currentPageIndex, defaultPageSize);
            }

            // 根据身份证号查询
            if (!string.IsNullOrWhiteSpace(id_number))
            {
                students = students.Where(s => s.Identity.Contains(id_number)).ToPagedList(currentPageIndex, defaultPageSize);
            }

            // 按发证日期查询
            if (start_date != null || end_date != null)
            {
                students = students.Where(s => s.IssueDate >= start_date).Where(s => s.IssueDate <= end_date).ToPagedList(currentPageIndex, defaultPageSize);
            }

            if (Request.IsAjaxRequest())
                return PartialView("_AjaxStudentList", students);
            else
                return View(students);
        }

        public ViewResult Details(int id)
        {
            Student student = db.Students.Find(id);
            return View(student);
        }

        public ActionResult AjaxDetails(int id)
        {
            var student = db.Students.Find(id);
            return Json(student, JsonRequestBehavior.DenyGet);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(student);
        }

        public ActionResult AjaxCreate(string name, string identity, string address, DateTime enrollment_date, string phone, DateTime? exam1, DateTime? exam2, DateTime? exam3, DateTime? exam4, DateTime? issue_date)
        {
            Student student = new Student();
            student.Name = name;
            student.Identity = identity;
            student.Address = address;
            if (enrollment_date != null)
            {
                student.EnrollmentDate = Convert.ToDateTime(enrollment_date);
            }
            student.Phone = phone;
            if (exam1 != null)
                student.Exam1 = Convert.ToDateTime(exam1);
            else
                student.Exam1 = DateTime.MaxValue;
            if (exam2 != null)
                student.Exam2 = Convert.ToDateTime(exam2);
            else
                student.Exam2 = DateTime.MaxValue;
            if (exam3 != null)
                student.Exam3 = Convert.ToDateTime(exam3);
            else
                student.Exam3 = DateTime.MaxValue;
            if (exam4 != null)
                student.Exam4 = Convert.ToDateTime(exam4);
            else
                student.Exam4 = DateTime.MaxValue;
            if (issue_date != null)
                student.IssueDate = Convert.ToDateTime(issue_date);
            else
                student.IssueDate = DateTime.MaxValue;

            db.Students.Add(student);
            db.SaveChanges();

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        //
        // Get: /Student/AjaxEdit/5

        public ActionResult AjaxEdit(int id)
        {
            Student student = db.Students.Find(id);
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult AjaxEdit(int id, string name, string identity, string address, DateTime? enrollment_date, string phone, DateTime? exam1, DateTime? exam2, DateTime? exam3, DateTime? exam4, DateTime? issue_date)
        {
            Student student = db.Students.Find(id);
            student.Name = name;
            student.Identity = identity;
            student.Address = address;
            if(enrollment_date != null)
            {
                student.EnrollmentDate = Convert.ToDateTime(enrollment_date);
            }
            student.Phone = phone;
            if (exam1 != null)
            {
                student.Exam1 = Convert.ToDateTime(exam1);
            }
            if (exam2 != null)
            {
                student.Exam2 = Convert.ToDateTime(exam2);
            }
            if (exam3 != null)
            {
                student.Exam3 = Convert.ToDateTime(exam3);
            }
            if (exam4 != null)
            {
                student.Exam4 = Convert.ToDateTime(exam4);
            }
            if (issue_date != null)
            {
                student.IssueDate = Convert.ToDateTime(issue_date);
            }
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        // Get: /Student/Edit/5

        public ActionResult Edit(int id)
        {
            Student student = db.Students.Find(id);
            return View(student);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }


        public ActionResult AjaxDelete(int id) {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return Json(null, JsonRequestBehavior.DenyGet);
        }
        //
        // GET: /Default1/Delete/5
 
        public ActionResult Delete(int id)
        {
            Student student = db.Students.Find(id);
            return View(student);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}