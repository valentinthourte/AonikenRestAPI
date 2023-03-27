using AonikenRestAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace AonikenRestAPI.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }
        public int Status { get; set; }
        public string Author { get; set; }

    }
}
