using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend_APIs.Models
{
    public class UserModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
        public UserModel()
        {
        }

        public UserModel(string value1, string value2, string value3, string role)
        {
            this.Username = value1;
            this.Email = value2;
            this.Password = value3;
            this.Role = role;
        }

        public bool AddNewUser()
        {
            ;
            return false;
        }

    }
}
