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
    public class IndexModel : PageModel
    {
        private readonly MegaDesk3._0.Data.MegaDesk3_0Context _context;

        public IndexModel(MegaDesk3._0.Data.MegaDesk3_0Context context)
        {
            _context = context;
        }

        public IList<DeskQuote> DeskQuote { get;set; } = default!;


        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }


        public SelectList? OrderDates { get; set; }


        [BindProperty(SupportsGet = true)]
        public DateTime? OrderDate { get; set; }


        public async Task OnGetAsync()
        {
            if (_context.DeskQuote != null)
            {
                // Search Through the Dates.
                IQueryable<DateTime> dateQuery = from m in _context.DeskQuote
                                                 orderby m.DateCreated.Date
                                                 select m.DateCreated.Date;

                // Search through DeskQuote to get customer names.
                var orders = from m in _context.DeskQuote
                             select m;

                if (!string.IsNullOrEmpty(SearchString))
                {
                   orders  = orders.Where(s => s.CustomerName.Contains(SearchString));
                }

                if (OrderDate != null)
                {
                    orders = orders.Where(x => x.DateCreated.Date == OrderDate);
                }

                // Add the Date Search on screen.
                OrderDates = new SelectList(await dateQuery.Distinct().ToListAsync());

                // Add the searchString where applicable.
                // This should make an new list that will be displayed on
                // screen.
                DeskQuote = await orders
                                .Include(d => d.DeliveryType)
                                .Include(d => d.Desk)
                                .Include(d => d.DesktopMaterial).ToListAsync();
            }
        }
    }
}
