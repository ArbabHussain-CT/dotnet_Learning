using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demoWithDotnet10AndEfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace demoWithDotnet10AndEfCore.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Character> Characters => Set<Character>();
    }
}