using System.ComponentModel.DataAnnotations;

namespace MegaDesk3._0.Models
{
    public class Desk
    {
        public int DeskId { get; set; }


        [Required]
        [Range(24, 96)]
        public decimal Width { get; set; }


        [Required]
        [Range(12, 48)]
        public decimal Depth { get; set; }


        [Display(Name = "Number Of Drawers")]
        public int NumberOfDrawers { get; set; }
    }
}
