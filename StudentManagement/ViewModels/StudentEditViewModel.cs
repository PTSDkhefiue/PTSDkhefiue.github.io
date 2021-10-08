using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels
{
    public class StudentEditViewModel:StudentCreateViewModel
    {
        public int id { get; set; }
        public string ExistringPhotoPath { get; set; }
    }
}
