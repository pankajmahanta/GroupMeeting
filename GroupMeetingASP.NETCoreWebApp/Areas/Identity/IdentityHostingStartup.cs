using System;
using GroupMeetingASP.NETCoreWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(GroupMeetingASP.NETCoreWebApp.Areas.Identity.IdentityHostingStartup))]
namespace GroupMeetingASP.NETCoreWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<GroupMeetingASPNETCoreWebAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("GroupMeetingASPNETCoreWebAppContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<GroupMeetingASPNETCoreWebAppContext>();
            });
        }
    }
}