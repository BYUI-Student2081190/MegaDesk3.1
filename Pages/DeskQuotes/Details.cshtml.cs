using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDesk3._0.Data;
using MegaDesk3._0.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MegaDesk3._0.Pages.DeskQuotes
{
    public class DetailsModel : PageModel
    {
        private readonly MegaDesk3._0.Data.MegaDesk3_0Context _context;

        public DetailsModel(MegaDesk3._0.Data.MegaDesk3_0Context context)
        {
            _context = context;
        }

      public DeskQuote DeskQuote { get; set; } = default!;
 
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }

            // Get DeskQuote from Database.
            var deskquote = await _context.DeskQuote.FirstOrDefaultAsync(m => m.DeskQuoteId == id);
            if (deskquote == null)
            {
                return NotFound();
            }

            // Assign DeskQuote.
            DeskQuote = deskquote;

            // Get Desk from Database.
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

            return Page();
        }
    }
}
