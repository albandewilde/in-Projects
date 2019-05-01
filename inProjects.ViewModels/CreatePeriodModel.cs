using System;
using System.Collections.Generic;
using System.Text;

namespace inProjects.ViewModels
{
    public class CreatePeriodModel
    {
        public DateTime begDate { get; set; }
        public DateTime endDate { get; set; }
        public char Kind { get; set; }
        public int idUser { get; set; }
        public int idZone { get; set; }
    }
}
