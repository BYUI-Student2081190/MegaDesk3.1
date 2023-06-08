using System.ComponentModel.DataAnnotations;

namespace MegaDesk3._0.Models
{
    public class DeliveryType
    {
        public int DeliveryTypeId { get; set; }

        [Display(Name = "Delivery")]
        public int DeliveryName { get; set; }

        // Attribute created to hold the string name of the
        // delivery type.
        public string? DeliveryStringName { get; set; }

        public decimal DeliveryPrice { get; set; }

        // Private Array's to hold the data needed for calculations.
        // 0 = 3 Day Rush
        // 1 = 5 Day Rush
        // 2 = 7 Day Rush
        private decimal[] ThreeDayRush = new decimal[3];
        private decimal[] FiveDayRush = new decimal[3];
        private decimal[] SevenDayRush = new decimal[3];

        // Add method that sets the price that is used.
        public void CalculateDeliveryPrice(decimal surfaceArea) 
        {
            // Initalize the Arrays.
            ThreeDayRush[0] = 60.00M;
            ThreeDayRush[1] = 70.00M;
            ThreeDayRush[2] = 80.00M;

            FiveDayRush[0] = 40.00M;
            FiveDayRush[1] = 50.00M;
            FiveDayRush[2] = 60.00M;

            SevenDayRush[0] = 30.00M;
            SevenDayRush[1] = 35.00M;
            SevenDayRush[2] = 40.00M;

            // Create a varible to hold the value of the
            // surface area test.
            int testResult = 0;

            // Set the StringName.
            GenerateStringDeliveryName();

            // Create if statement to generate a value to get
            // the RushPrice from the Surface area. 
            if (surfaceArea < 1000) 
            {
                testResult = 0;
            }

            else if(surfaceArea > 1000 &&  surfaceArea < 2000) 
            {
                testResult = 1;
            }

            else
            {
                testResult = 2;
            }

            // Test to see what price we get.
            // Note(DeliveryName Values):
            //       0 = No Rush
            //       1 = 3 Day Rush
            //       2 = 5 Day Rush
            //       3 = 7 Day Rush
            if (DeliveryName == 1) 
            {
                DeliveryPrice = ThreeDayRush[testResult];
            }

            else if(DeliveryName == 2) 
            {
                DeliveryPrice = FiveDayRush[testResult];
            }

            else if(DeliveryName == 3) 
            {
                DeliveryPrice = SevenDayRush[testResult];
            }

            else 
            {
                DeliveryPrice = (0.00M);
            }
        }

        public void GenerateStringDeliveryName() 
        {
            if (DeliveryName == 1)
            {
                DeliveryStringName = "3 Day Rush";
            }

            else if (DeliveryName == 2) 
            {
                DeliveryStringName = "5 Day Rush";
            }

            else if (DeliveryName == 3) 
            {
                DeliveryStringName = "7 Day Rush";
            }

            else 
            {
                DeliveryStringName = "No Rush";
            }
        }
    }
}
