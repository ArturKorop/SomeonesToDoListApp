using SomeonesToDoListApp.DataAccessLayer.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeonesToDoListApp.DataAccessLayer.Entities
{
   [Table("dbo.ToDo")]
   public class ToDo : IToDo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ToDoItem { get; set; }
    }
}
