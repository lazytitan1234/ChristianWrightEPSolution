using Microsoft.EntityFrameworkCore;
using Domain.Models;
using System.Collections.Generic;



namespace DataAccess.DataContext
{
    public class PollDbContext : DbContext
    {
        public PollDbContext(DbContextOptions<PollDbContext> options) : base(options) { }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<VoteRecord> VoteRecords { get; set; }
    }
}
