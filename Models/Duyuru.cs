using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduProject.Models
{
    public class Duyuru
    {
        public int DuyuruId { get; set; }
        public string DuyuruName  { get; set; }
        public string DuyuruText { get; set; }
        public DateTime DuyuruTarih { get; set; }
    }
    
}