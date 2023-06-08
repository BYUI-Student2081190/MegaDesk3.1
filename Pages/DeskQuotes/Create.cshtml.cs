using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MegaDesk3._0.Data;
using MegaDesk3._0.Models;

namespace MegaDesk3._0.Pages.DeskQuotes
{
    public class CreateModel : PageModel
    {
        private readonly MegaDesk3._0.Data.MegaDesk3_0Context _context;

        public CreateModel(MegaDesk3._0.Data.MegaDesk3_0Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["DeliveryTypeId"] = new SelectList(_context.Set<DeliveryType>(), "DeliveryTypeId", "DeliveryTypeId");
        ViewData["DeskId"] = new SelectList(_context.Set<Desk>(), "DeskId", "DeskId");
        ViewData["DesktopMaterialId"] = new SelectList(_context.Set<DesktopMaterial>(), "DesktopMaterialId", "DesktopMaterialId");
            return Page();
        }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; } = default!;

        /* Attributes added that help with calculating the finished product. */
        private decimal surfaceArea { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.DeskQuote == null || DeskQuote == null)
            {
                return Page();
            }

            // If we are valid to move on to set
            // the DateTime to now,
            // then call these functions to preform the calculations.

            // Set the DateTime to now.
            DeskQuote.DateCreated = DateTime.Now;

            // Obtain the Surface Area of the desk.
            surfaceArea = CalculateSurfaceArea();

            // Get the delivery price.
            DeskQuote.DeliveryType.CalculateDeliveryPrice(surfaceArea);

            // Get the Material Cost.
            DeskQuote.DesktopMaterial.GenerateMaterialCost();

            // Caluculate the Total Price of the Desk and set it to the DeskQuote Database.
            CalculateTotalCost();

            _context.DeskQuote.Add(DeskQuote);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private decimal CalculateSurfaceArea() 
        {
           return (DeskQuote.Desk.Width * DeskQuote.Desk.Depth);
        }

        private void CalculateTotalCost() 
        {
            // Set a varible that hold additional cost if any.
            decimal additionalCosts = 0;

            // Get drawer amount.
            decimal drawerCost = DeskQuote.Desk.NumberOfDrawers * DeskQuote.GetDRAWER_COST();
            // Get Base Desk Price.
            decimal baseDeskCost = DeskQuote.GetBASE_DESK_PRICE();
            
            // Test to see if additional costs are needed.
            if (surfaceArea > 1000) 
            {
                // Then set the value of Additional costs.
                additionalCosts = surfaceArea * DeskQuote.GetSURFACE_AREA_COST();
            }

            // Now calculate the total and set it in DeskQuote.
            DeskQuote.TotalCost = baseDeskCost + drawerCost + additionalCosts + DeskQuote.DesktopMaterial.Cost + DeskQuote.DeliveryType.DeliveryPrice;
        }
    }
}
