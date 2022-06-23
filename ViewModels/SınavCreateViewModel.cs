using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduProject.ViewModels
{
    public class SınavCreateViewModel
    {
        public string Name {get;set;}
        public string DersAdı {get;set;}
        public IFormFile FilePath {get;set;}
    }
}