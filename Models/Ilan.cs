using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduProject.Models
{
    public class Ilan
    {
        public int IlanId { get; set; }
        public bool Arıyor { get; set; }
        public string Message { get; set; }
        public string iletisim { get; set; }
        public DateTime? atCreated { get; set; }
    }
}