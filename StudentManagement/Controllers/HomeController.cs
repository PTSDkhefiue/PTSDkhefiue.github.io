using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using StudentManagement.Models;
using StudentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment IHostEnvironment;
        private readonly ILogger logger;

        public HomeController(IStudentRepository studentRepository,IWebHostEnvironment IHostEnvironment,ILogger<HomeController> logger)
        {
            _studentRepository = studentRepository;
            this.IHostEnvironment = IHostEnvironment;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            IEnumerable<Student> student = _studentRepository.GrtAllStudent();
            return View(student);
        }
        public ViewResult Details(int id)
        {
            //throw new Exception("此异常发生在Details中");
            logger.LogTrace("Trace（跟踪）Log");
            logger.LogDebug("Debug（调试）Log");
            logger.LogInformation("Information（信息）Log");
            logger.LogWarning("Warning（警告）Log");
            logger.LogError("Error（错误）Log");
            logger.LogCritical("Critical（严重）Log");
            Student student = _studentRepository.GetStudent(id);
            if (student == null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound", id);
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Student = student,
                PageTitle = "学生详细信息"
            };
            return View(homeDetailsViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Student newStudent = _studentRepository.Add(student);
                string uniqueFileName = null;
                if (model.Photos != null && model.Photos.Count > 0){
                    foreach(var photo in model.Photos){
                        string uploadsFolder = Path.Combine(IHostEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }
                Student newStudent = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    ClassName = model.ClassName,
                    PhototPath = uniqueFileName
                };
                _studentRepository.Add(newStudent);
                return RedirectToAction("Details", new { id = newStudent.Id });
            }
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Student student = _studentRepository.GetStudent(id);
            StudentEditViewModel studentEditView = new StudentEditViewModel
            {
                Id = student.Id,
                Name = student.Name,
                ClassName = student.ClassName,
                ExistringPhotoPath = student.PhototPath
            };
            return View(studentEditView);
        }
        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            //检查提供的数据是否有效，如果没有通过验证，需要重新编辑学生信息
            //这样用户就可以更正并重新提交编辑表单
            if (ModelState.IsValid)
            {
                Student student = _studentRepository.GetStudent(model.Id);
                student.Name = model.Name;
                student.Email = model.Email;
                student.ClassName = model.ClassName;
                if (model.Photos.Count > 0)
                {
                    if (model.ExistringPhotoPath != null)
                    {
                        string filePath = Path.Combine(IHostEnvironment.WebRootPath, "images", model.ExistringPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    string uniqueFileName = null;
                    foreach(var Photo in model.Photos)
                    {
                        string uploadsFolder = Path.Combine(IHostEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + '_' + Photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath,FileMode.Create))
                        {
                            Photo.CopyTo(fileStream);
                        }  
                    }
                    student.PhototPath = uniqueFileName;
                }
                Student updatestudent = _studentRepository.Update(student);
                return RedirectToAction("Index");
            }
            return View(model);
        }  
        public IActionResult Delete(int id)
        {
            Student student = _studentRepository.GetStudent(id);
            if(student != null)
            {
                Delete(id);
            }
            return View();
        }
    }
}
