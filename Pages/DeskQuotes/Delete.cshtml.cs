using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDesk3._0.Data;
using MegaDesk3._0.Models;

namespace MegaDesk3._0.Pages.DeskQuotes
{
    public class DeleteModel : PageModel
    {
        private readonly MegaDesk3._0.Data.MegaDesk3_0Context _context;

        public DeleteModel(MegaDesk3._0.Data.MegaDesk3_0Context context)
        {
            _context = context;
        }

        [BindProperty]
      public DeskQuote DeskQuote { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }

            var deskquote = await _context.DeskQuote.FirstOrDefaultAsync(m => m.DeskQuoteId == id);

            if (deskquote == null)
            {
                return NotFound();
            }
            
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }
            var deskquote = await _context.DeskQuote.FindAsync(id);

            if (deskquote != null)
            {
                DeskQuote = deskquote;

                var desk = await _context.Desk.FindAsync(DeskQuote.DeskId);
                var desktopMaterial = await _context.DesktopMaterial.FindAsync(DeskQuote.DesktopMaterialId);
                var deliveryType = await _context.DeliveryType.FindAsync(DeskQuote.DeliveryTypeId);

                // Join wth DeskQuote as long as they are not null.
                if (desk != null && desktopMaterial != null && deliveryType != null) 
                {
                    // Remove all datatypes.
                    _context.Desk.Remove(desk);
                    _context.DesktopMaterial.Remove(desktopMaterial);
                    _context.DeliveryType.Remove(deliveryType);
                    _context.DeskQuote.Remove(DeskQuote);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
