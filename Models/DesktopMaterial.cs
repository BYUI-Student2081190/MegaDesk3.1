using System.ComponentModel.DataAnnotations;

namespace MegaDesk3._0.Models
{
    public class DesktopMaterial
    {
        public int DesktopMaterialId { get; set; }

        [Display(Name = "Material")]
        public string DesktopMaterialName { get; set; }

        public decimal Cost { get; set; }

        // Create a Method to get the price of the material.
        public void GenerateMaterialCost() 
        {
            // Test the names and bring set the value.
            if (DesktopMaterialName == "Laminate") 
            {
                Cost = 100.00M;
            }

            else if (DesktopMaterialName == "Pine")
            {
                Cost = 50.00M;
            }

            else if(DesktopMaterialName == "Rosewood") 
            {
                Cost = 300.00M;
            }

            else if(DesktopMaterialName == "Veneer") 
            {
                Cost = 125.00M;
            }

            else 
            {
                Cost = 200.00M;
            }
        }
    }
}
