using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaDesk3._0.Data;
using MegaDesk3._0.Models;

namespace MegaDesk3._0.Pages.DeskQuotes
{
    public class EditModel : PageModel
    {
        private readonly MegaDesk3._0.Data.MegaDesk3_0Context _context;

        public EditModel(MegaDesk3._0.Data.MegaDesk3_0Context context)
        {
            _context = context;
        }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; } = default!;

        /* Attributes added that help with calculating the finished product. */
        private decimal surfaceArea { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }

            var deskquote =  await _context.DeskQuote.FirstOrDefaultAsync(m => m.DeskQuoteId == id);
            if (deskquote == null)
            {
                return NotFound();
            }
            // Set DeskQuote for future calculations.
            DeskQuote = deskquote;

            var desk = await _context.Desk.FirstOrDefaultAsync(m => m.DeskId == DeskQuote.DeskId);
            if (desk == null)
            {
                return NotFound();
            }

            var desktopMaterial = await _context.DesktopMaterial.FirstOrDefaultAsync(m => m.DesktopMaterialId == DeskQuote.DesktopMaterialId);
            if (desktopMaterial == null)
            {
                return NotFound();
            }

            var deliveryType = await _context.DeliveryType.FirstOrDefaultAsync(m => m.DeliveryTypeId == DeskQuote.DeliveryTypeId);
            if (deliveryType == null)
            {
                return NotFound();
            }

            // Assign objects to DeskQuote.
            DeskQuote.Desk = desk;
            DeskQuote.DesktopMaterial = desktopMaterial;
            DeskQuote.DeliveryType = deliveryType;

            ViewData["DeliveryTypeId"] = new SelectList(_context.Set<DeliveryType>(), "DeliveryTypeId", "DeliveryTypeId");
            ViewData["DeskId"] = new SelectList(_context.Set<Desk>(), "DeskId", "DeskId");
            ViewData["DesktopMaterialId"] = new SelectList(_context.Set<DesktopMaterial>(), "DesktopMaterialId", "DesktopMaterialId");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Do new Caluculations.
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

            _context.Attach(DeskQuote).State = EntityState.Modified;
            _context.Attach(DeskQuote.Desk).State = EntityState.Modified;
            _context.Attach(DeskQuote.DesktopMaterial).State = EntityState.Modified;
            _context.Attach(DeskQuote.DeliveryType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeskQuoteExists(DeskQuote.DeskQuoteId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DeskQuoteExists(int id)
        {
          return (_context.DeskQuote?.Any(e => e.DeskQuoteId == id)).GetValueOrDefault();
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
