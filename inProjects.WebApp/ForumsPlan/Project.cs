using System;
using System.Collections.Generic;
using System.Text;

namespace inProjects.WebApp.ForumsPlan
{
    public class Project
    {
        public string _projectName;
        public string _semester;
        public int? _posX;
        public int? _posY;
        public ClassRoom _classRoom;

        public Project( string name, string semester, int posX, int posY )
        {
            _projectName = name;
            _semester = semester;
            _posX = posX;
            _posY = posY;
        }

        public string Name => _projectName;
        public string Semester => _semester;
        public int? PosX => _posX;
        public int? PosY => _posY;
    }
}
