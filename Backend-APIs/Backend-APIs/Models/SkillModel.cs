using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend_APIs.Models
{
    public class SkillModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SkillID { get; set; }
        public string SkillName { get; set; }

    }
}
