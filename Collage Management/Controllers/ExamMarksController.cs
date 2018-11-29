using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Collage_Management.Models;

namespace Collage_Management.Controllers
{
    [Authorize]
    public class ExamMarksController : Controller
    {
        private School_ManagementEntities db = new School_ManagementEntities();

        // GET: ExamMarks
        public ActionResult Index()
        {
            var examMarks = db.ExamMarks.Include(e => e.Department).Include(e => e.User).Include(e => e.Semester).Include(e => e.Student);
            return View(examMarks.ToList());
        }

        // GET: ExamMarks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamMark examMark = db.ExamMarks.Find(id);
            if (examMark == null)
            {
                return HttpNotFound();
            }
            return View(examMark);
        }

        // GET: ExamMarks/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.AddedBy = new SelectList(db.Users, "UserId", "UserName");
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName");
            return View();
        }

        // POST: ExamMarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentId,SubjectCode,Marks,ExamName,SemesterId,Session,DepartmentId,AddedDate,AddedBy")] ExamMark examMark)
        {
            if (ModelState.IsValid)
            {
                db.ExamMarks.Add(examMark);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", examMark.DepartmentId);
            ViewBag.AddedBy = new SelectList(db.Users, "UserId", "UserName", examMark.AddedBy);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", examMark.SemesterId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName", examMark.StudentId);
            return View(examMark);
        }

        // GET: ExamMarks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamMark examMark = db.ExamMarks.Find(id);
            if (examMark == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", examMark.DepartmentId);
            ViewBag.AddedBy = new SelectList(db.Users, "UserId", "UserName", examMark.AddedBy);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", examMark.SemesterId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName", examMark.StudentId);
            return View(examMark);
        }

        // POST: ExamMarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentId,SubjectCode,Marks,ExamName,SemesterId,Session,DepartmentId,AddedDate,AddedBy")] ExamMark examMark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examMark).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", examMark.DepartmentId);
            ViewBag.AddedBy = new SelectList(db.Users, "UserId", "UserName", examMark.AddedBy);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", examMark.SemesterId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName", examMark.StudentId);
            return View(examMark);
        }

        // GET: ExamMarks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamMark examMark = db.ExamMarks.Find(id);
            if (examMark == null)
            {
                return HttpNotFound();
            }
            return View(examMark);
        }

        // POST: ExamMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamMark examMark = db.ExamMarks.Find(id);
            db.ExamMarks.Remove(examMark);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
