using ManchesterFans.Domain.Entities.Pages;
using ManchesterFans.Domain.Entities.Site;
using ManchesterFans.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManchesterFans.Application.Interfaces.Context
{
    public interface IDataBaseContext
    {

        DbSet<User> Users { get; set; }
        DbSet<Page> Pages { get; set; }
        DbSet<PageComments> PageComments { get; set; }
        DbSet<PageGroup> PageGroups { get; set; }
        DbSet<PageLikes> PageLikes { get; set; }
        DbSet<Header> Headers { get; set; }
        DbSet<HeaderLinks> HeaderLinks { get; set; }
        DbSet<SliderPosts> SliderPosts { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
