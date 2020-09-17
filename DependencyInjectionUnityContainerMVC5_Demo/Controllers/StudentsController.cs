using CURDWithEntityFrameworkCodeFirstWebAPI_Demo.Models;
using DependencyInjectionUnityContainerMVC5_Demo.Repositories;
using System.Net;
using System.Web.Mvc;

namespace DependencyInjectionUnityContainerMVC5_Demo.Controllers
{
    public class StudentsController : Controller
    {
        private UnitOfWork unitOfWork;
        public StudentsController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            var students = unitOfWork.Students.GetStudentsWithCourse();
            return View(students);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = unitOfWork.StudentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(unitOfWork.CourseRepositroy.GetAll(), "Id", "CourseName");
            ViewBag.InstructorId = new SelectList(unitOfWork.InstructorRepository.GetAll(), "Id", "InstructorName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.StudentRepository.Add(student);
                unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(unitOfWork.CourseRepositroy.GetAll(), "Id", "CourseName");
            ViewBag.InstructorId = new SelectList(unitOfWork.InstructorRepository.GetAll(), "Id", "InstructorName");
            return View(student);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = unitOfWork.StudentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(unitOfWork.CourseRepositroy.GetAll(), "Id", "CourseName");
            ViewBag.InstructorId = new SelectList(unitOfWork.InstructorRepository.GetAll(), "Id", "InstructorName");
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                Student edit = unitOfWork.StudentRepository.GetById(student.Id);
                edit.StudentName = student.StudentName;
                edit.CourseFee = student.CourseFee;
                edit.Course = unitOfWork.CourseRepositroy.GetById(student.CourseId);
                edit.CourseDuration = student.CourseDuration;
                edit.StartDate = student.StartDate;
                edit.BatchTime = student.BatchTime;
                edit.Instructor = unitOfWork.InstructorRepository.GetById(student.InstructorId);
                unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(unitOfWork.CourseRepositroy.GetAll(), "Id", "CourseName");
            ViewBag.InstructorId = new SelectList(unitOfWork.InstructorRepository.GetAll(), "Id", "InstructorName");
            return View(student);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = unitOfWork.StudentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var entity = unitOfWork.StudentRepository.GetById(id);
            unitOfWork.StudentRepository.Remove(entity);
            unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
