using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoelHiltonMovies.Models
{
    public class MovieResponseContext : DbContext
    {
        public MovieResponseContext(DbContextOptions<MovieResponseContext> options) : base(options)
        { }

        public DbSet<MovieResponse> MovieResponses { get; set; }
    }
}
