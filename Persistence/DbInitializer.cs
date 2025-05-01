using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        private readonly StoreIdentityDbContext _identityContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public DbInitializer(StoreDbContext context,
            StoreIdentityDbContext identityContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            _context = context;
            _identityContext = identityContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task InitializeAsync()
        {
            try
            {

                if (!_context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText(@"..\Persistence\Data\Seeding\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types is not null && types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }
                if (!_context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText(@"..\Persistence\Data\Seeding\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    if (brands is not null && brands.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }
                }
                if (!_context.ProductBrands.Any())
                {
                    var productData = File.ReadAllText(@"..\Persistence\Data\Seeding\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    if (products is not null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch
            {

            }
        }


        public async Task InitializeIdentityAsync()
        {
            if (_identityContext.Database.GetPendingMigrations().Any())
                await _identityContext.Database.MigrateAsync();

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if (!_userManager.Users.Any())
            {
                var superAdminUser = new User
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "01120503831",
                };
                var adminUser = new User
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "01091698489",
                };
                await _userManager.CreateAsync(superAdminUser, "SuperAdmin123");
                await _userManager.CreateAsync(adminUser, "Admin123");
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");

            }
        }

    }
}
