using BlackDigital.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Example.Models
{
    public class MyModel
    {
        [Key]
        [NotShow]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "Test";

        public int Age { get; set; } = 15;

        public int? Size { get; set; }

        [Editable(false)]
        [Display(Name = "Calculation")]
        public int Calc { get; set; }

        public DateTime Date { get; set; }

        public DateOnly? StartDate { get; set; }

        public TimeOnly? Time { get; set; }

        public bool? Active { get; set; }
    }
}
