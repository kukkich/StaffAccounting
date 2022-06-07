using Microsoft.EntityFrameworkCore;
using StaffAccounting.Models.Company;
using StaffAccounting.Models.Filtration;
using StaffAccounting.Models.ViewModels.StyleProvider;

namespace StaffAccounting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IOptionsStyleProvider, TableOptionsStyleProvider>();
            builder.Services.AddDbContext<CompanyContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employee}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}