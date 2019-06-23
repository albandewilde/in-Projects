using System;
using System.Collections.Generic;
using System.Text;

namespace inProjects.Data.Data.ProjectStudent
{
   public class ProjectForumResultData
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public double Average { get; set; }
        public Dictionary<string,double> IndividualGrade { get; set; }
        public List<int> JurysId { get; set; }
        public bool IsBlocked { get; set; }

    }
}
