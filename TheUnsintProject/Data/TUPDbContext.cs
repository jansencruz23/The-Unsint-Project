using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheUnsintProject.Models;

namespace TheUnsintProject.Data
{
    public class TUPDbContext : DbContext
    {
        public TUPDbContext (DbContextOptions<TUPDbContext> options)
            : base(options)
        {
        }

        public DbSet<TheUnsintProject.Models.Letter> Letter { get; set; } = default!;
    }
}
