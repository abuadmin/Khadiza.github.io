﻿using System;
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
    public class StudentsController : Controller
    {
        private School_ManagementEntities db = new School_ManagementEntities();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Department).Include(s => s.Semester).Include(s => s.Shift).Include(s => s.User);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName");
            ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName");
            ViewBag.AddedBy = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,StudentName,Gender,Address,Phone,Session,DepartmentId,SemesterId,ShiftId,RegNo,AddedDate,AddedBy")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", student.SemesterId);
            ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName", student.ShiftId);
            ViewBag.AddedBy = new SelectList(db.Users, "UserId", "UserName", student.AddedBy);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", student.SemesterId);
            ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName", student.ShiftId);
            ViewBag.AddedBy = new SelectList(db.Users, "UserId", "UserName", student.AddedBy);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,StudentName,Gender,Address,Phone,Session,DepartmentId,SemesterId,ShiftId,RegNo,AddedDate,AddedBy")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", student.SemesterId);
            ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName", student.ShiftId);
            ViewBag.AddedBy = new SelectList(db.Users, "UserId", "UserName", student.AddedBy);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
