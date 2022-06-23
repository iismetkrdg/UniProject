using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduProject.Models
{
    public class Sınav
    {
        public int SınavId { get; set; }
        public string Name { get; set; }
        public string DersAdı { get; set; }
        public string FileName { get; set; }
    }
}