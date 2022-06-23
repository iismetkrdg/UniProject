using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduProject.ViewModels
{
    public class DersNotuCreateViewModel
    {
        public string DersAdı {get;set;}
        public string Konu { get; set; }
        public IFormFile FilePath {get;set;}
    }
}