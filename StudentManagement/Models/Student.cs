using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    /// <summary>
    /// 学生模型
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="请输入名字"),MaxLength(10,ErrorMessage ="名字的长度不能超过10个字符")]
        public string Name { get; set; } 
        [Required(ErrorMessage = "请输入邮箱地址")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$", ErrorMessage = "请输入正确的邮箱地址")]
        public string Email { get; set; }
        [Required(ErrorMessage = "请选择年级")]
        public ClassNameEnum? ClassName { get; set; }
        public string PhototPath { get; set; }
        //public int deleteProperty { get; set; }
    }
}
