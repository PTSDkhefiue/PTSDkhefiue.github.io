using Microsoft.AspNetCore.Http;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels
{
    public class StudentCreateViewModel
    {
        
        public int Id { get; set; }
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "请输入名字"), MaxLength(10, ErrorMessage = "名字的长度不能超过10个字符")]
        public string Name { get; set; }
        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "请输入邮箱地址")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$", ErrorMessage = "请输入正确的邮箱地址")]
        public string Email { get; set; }
        [Display(Name = "年级")]
        [Required(ErrorMessage = "请选择年级")]
        public ClassNameEnum? ClassName { get; set; }
        [Display(Name ="头像")]
        //public IFormFile Photo { get; set; }//单张图片
        public List<IFormFile> Photos { get; set; }//多张图片
    }
}
