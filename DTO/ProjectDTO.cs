using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class ProjectDTO
    {
        public ProjectDTO() { }

        public ProjectDTO(long projectId, string name, string content,
                          string img, string description, bool projectStatus,
                          int? semesterId)
        {
            ProjectId = projectId;
            Name = name;
            Content = content;
            Img = img;
            Description = description;
            ProjectStatus = projectStatus;
            SemesterId = semesterId;
        }

        public long ProjectId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public bool ProjectStatus { get; set; }
        public int? SemesterId { get; set; }
    }
}
