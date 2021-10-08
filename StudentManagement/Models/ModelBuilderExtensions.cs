using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "小明",
                    ClassName = ClassNameEnum.一年级,
                    Email = "XiaoMing@qq.com",
                },
                new Student
                {
                    Id = 2,
                    Name = "小红",
                    ClassName = ClassNameEnum.二年级,
                    Email = "XiaoHong@qq.com",
                });
        }
    }
}
