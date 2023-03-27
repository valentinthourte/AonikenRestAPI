using System.ComponentModel.DataAnnotations;

namespace AonikenRestAPI.Models
{
    public class User
    {
        [Key]
        public int ID_USER { get; set; }
        public string USERNAME { get; set; }
        public string PASS { get; set; }
        public int USER_TYPE { get; set; }
    }
}
