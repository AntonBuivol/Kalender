using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagerMvc.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
