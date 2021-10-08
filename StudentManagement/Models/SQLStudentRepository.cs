using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly ILogger logger;
        private readonly AppDbContext context;
        public SQLStudentRepository(AppDbContext context,ILogger<SQLStudentRepository> logger)
        {
            this.logger = logger;
            this.context = context;
        }
        public Student Add(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
            return student;
        }

        public Student Delete(int id)
        {
            Student student = context.Students.Find(id);
            if (student!=null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
            }
            return student;
        }

        public Student GetStudent(int id)
        {
            return context.Students.Find(id);
        }

        public IEnumerable<Student> GrtAllStudent()
        {
            logger.LogTrace("学生信息 Trace（跟踪）Log");
            logger.LogDebug("学生信息 Debug（调试）Log");
            logger.LogInformation("学生信息 Information（信息）Log");
            logger.LogWarning("学生信息 Warning（警告）Log");
            logger.LogError("学生信息 Error（错误）Log");
            logger.LogCritical("学生信息 Critical（严重）Log");
            return context.Students;
        }

        public Student Update(Student updatestudent)
        {
            var student = context.Students.Attach(updatestudent);
            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatestudent;
        }
    }
}
