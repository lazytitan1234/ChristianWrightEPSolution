using Microsoft.EntityFrameworkCore;
using Domain.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace DataAccess.DataContext
{
    public class PollDbContext : IdentityDbContext
    {
        public PollDbContext(DbContextOptions<PollDbContext> options) : base(options) { }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<VoteRecord> VoteRecords { get; set; }
    }
}
