using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Acme.OrderProject.Customers;
using Acme.OrderProject.Stocks;
using Acme.OrderProject.Orders;

namespace Acme.OrderProject.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class OrderProjectDbContext :
    AbpDbContext<OrderProjectDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }

    #region Entities from the modules

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public OrderProjectDbContext(DbContextOptions<OrderProjectDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        // Customer mapping
        builder.Entity<Customer>(b =>
        {
            b.ToTable(OrderProjectConsts.DbTablePrefix + "Customers", OrderProjectConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);

            b.Property(x => x.RiskLimit)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            b.Property(x => x.BillAddress)
                .IsRequired()
                .HasMaxLength(256);
        });

        // Stock mapping
        builder.Entity<Stock>(b =>
        {
            b.ToTable(OrderProjectConsts.DbTablePrefix + "Stocks", OrderProjectConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);

            b.Property(x => x.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            b.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        });

        // Order mapping
        builder.Entity<Order>(b =>
        {
            b.ToTable(OrderProjectConsts.DbTablePrefix + "Orders", OrderProjectConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.DeliveryAddress)
                .IsRequired()
                .HasMaxLength(256);

            b.Property(x => x.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            b.HasMany(x => x.Lines)
                .WithOne()
                .HasForeignKey(l => l.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // OrderLine mapping
        builder.Entity<OrderLine>(b =>
        {
            b.ToTable(OrderProjectConsts.DbTablePrefix + "OrderLines", OrderProjectConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Quantity)
                .IsRequired();

            b.Property(x => x.LineTotal)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        });
    }
}

        
        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(OrderProjectConsts.DbTablePrefix + "YourEntities", OrderProjectConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    

