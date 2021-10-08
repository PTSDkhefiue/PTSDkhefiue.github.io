using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _studentsList;
        public MockStudentRepository()
        {
            _studentsList = new List<Student>()
            {
                new Student(){Id = 1,Name = "张三",ClassName = ClassNameEnum.一年级,Email = "Tony-zhang@qq.com"},
                new Student(){Id = 2,Name = "李四",ClassName = ClassNameEnum.二年级,Email = "list@qq.com"},
                new Student(){Id = 3,Name = "王强",ClassName = ClassNameEnum.三年级,Email = "wang@qq.com"},
            };
        }
        public Student Add(Student student)
        {
            student.Id = _studentsList.Max(s => s.Id) + 1;
            _studentsList.Add(student);
            return student;
        }

        public Student Delete(int id)
        {
            Student student = _studentsList.FirstOrDefault(s => s.Id == id);
            if(student!=null)
            {
                _studentsList.Remove(student);
            }
            return student;
        }

        public Student GetStudent(int id)
        {
            return _studentsList.FirstOrDefault(a => a.Id == id);
        }
        public IEnumerable<Student> GrtAllStudent()
        {
            return _studentsList;
        }

        public Student Update(Student updatestudent)
        {
            Student student = _studentsList.FirstOrDefault(s => s.Id == updatestudent.Id);
            if (student!=null)
            {
                student.Name = updatestudent.Name;
                student.Email = updatestudent.Email;
                student.ClassName = updatestudent.ClassName;
            }
            return student;
        }
    }
}
