using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.tweetapp.Models
{
    class TweetAppContext : DbContext
    {
        public TweetAppContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=CTSDOTNET368;Initial Catalog=TweetAppDB;User ID=sa;Password=pass@word1");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<UserCred> UserCreds { get; set; }
    }
}
