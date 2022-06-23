using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduProject.Models
{
    public class ForumComment
    {
        public int ForumCommentId { get; set; }
        public int ForumId { get; set; }
        public string Message { get; set; }
        public string Creator { get; set; }
    }
}