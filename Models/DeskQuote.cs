using System.ComponentModel.DataAnnotations;

namespace MegaDesk3._0.Models
{
    public class DeskQuote
    {
        // Constants
        private const decimal BASE_DESK_PRICE = 200.00M;
        private const decimal SURFACE_AREA_COST = 1.00M;
        private const decimal DRAWER_COST = 50.00M;

        // Properties
        public int DeskQuoteId { get; set; }

        [Display(Name = "Customer Name")]
        [Required]
        [StringLength(60)]
        public string CustomerName { get; set; }


        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        public decimal TotalCost { get; set; }

        /* Foreign Key */
        public int DeliveryTypeId { get; set; }
        public int DesktopMaterialId { get; set; }
        public int DeskId { get; set; }

        /* Navigation */
        public DeliveryType DeliveryType { get; set; }
        public DesktopMaterial DesktopMaterial { get; set; }
        public Desk Desk { get; set; }

        /*Methods to obtain the constants.*/
        public decimal GetDRAWER_COST() 
        {
            return DRAWER_COST;
        }
        public decimal GetSURFACE_AREA_COST()
        {
            return SURFACE_AREA_COST;
        }
        public decimal GetBASE_DESK_PRICE()
        {
            return BASE_DESK_PRICE;
        }
    }
}
