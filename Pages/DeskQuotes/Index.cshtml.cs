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
    public class IndexModel : PageModel
    {
        private readonly MegaDesk3._0.Data.MegaDesk3_0Context _context;

        public IndexModel(MegaDesk3._0.Data.MegaDesk3_0Context context)
        {
            _context = context;
        }

        public IList<DeskQuote> DeskQuote { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DeskQuote != null)
            {
                DeskQuote = await _context.DeskQuote
                .Include(d => d.DeliveryType)
                .Include(d => d.Desk)
                .Include(d => d.DesktopMaterial).ToListAsync();
            }
        }
    }
}
