using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MVC.Models
{
    internal class DefaultDateTimeValueAttribute : Attribute
    {
        public string DefaultValue { get; set; }

        public DefaultDateTimeValueAttribute()
        {
            DefaultValue = DateTime.Now.ToString();
        }
    }

    [Table("Order")]
    public partial class Order
    {
        [Key]
        public int ID { get; set; }

        [DefaultDateTimeValue()]
        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        [StringLength(250)]
        public string ShipName { get; set; }

        [StringLength(10)]
        public string ShipMobile { get; set; }

        [StringLength(250)]
        public string ShipAddress { get; set; }

        [StringLength(250)]
        public string ShipEmail { get; set; }

        public string Transport { get; set; }

        public decimal? TransportationFee { get; set; }

        public string PaymentMethods { get; set; }

        public DateTime? Ordered { get; set; }

        public DateTime? Confirmed { get; set; }

        public DateTime? TookProducts { get; set; }

        public DateTime? Complete { get; set; }

        public DateTime? Canceled { get; set; }

        [DefaultValue("true")]
        public bool? Status { get; set; }
    }

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ProductID { get; set; }

        [DefaultValue(0)]
        public int Count { get; set; }

        [DefaultValue(0)]
        public decimal? Price { get; set; }

        [DefaultValue(0)]
        public decimal? PromotionPrice { get; set; }
    }

    [Table("Product")]
    public partial class Product
    {
        [Key]
        public int ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string Author { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [DefaultValue(0)]
        public decimal? Price { get; set; }

        [DefaultValue(0)]
        public decimal? PromotionPrice { get; set; }

        [DefaultValue("true")]
        public bool? IncludedVAT { get; set; }

        [DefaultValue(0)]
        public int? Quantity { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public int? Warranty { get; set; }

        [DefaultDateTimeValue()]
        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        [DefaultDateTimeValue()]
        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        [DefaultValue("true")]
        public bool? Status { get; set; }
    }

    [Table("ProductDetail")]
    public partial class ProductDetail
    {
        [Key]
        [Column(Order = 0)]
        public int ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ProductCategoryID { get; set; }
    }

    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        [Key]
        public int ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public int? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        public string SeoTitle { get; set; }

        [DefaultDateTimeValue()]
        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        [DefaultDateTimeValue()]
        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        [DefaultValue("true")]
        public bool? Status { get; set; }

        [DefaultValue("false")]
        public bool? ShowOnHome { get; set; }
    }

    [Table("Rate")]
    public partial class Rate
    {
        [Key]
        public int ID { get; set; }

        [DefaultValue(1)]
        public int? RatePoint { get; set; }

        [DefaultDateTimeValue()]
        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        public int? ProductID { get; set; }
    }

    [Table("Users")]
    public partial class User
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string UserID { get; set; }

        [StringLength(250)]
        public string Password { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }

        public string Sex { get; set; }

        public string Role { get; set; }

        public string Image { get; set; }

        public bool? Active { get; set; }

        [DefaultValue("true")]
        public bool? Status { get; set; }
    }

    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        public int ID { get; set; }

        public string Content { get; set; }

        [DefaultDateTimeValue()]
        public DateTime? CreateDate { get; set; }

        public DateTime? LastModify { get; set; }

        public int? CreateBy { get; set; }

        public int? ProductID { get; set; }

        public int? ParentID { get; set; }
    }

    public partial class DatabaseDetailsContext : DbContext
    {
        public DatabaseDetailsContext() : base("name=DatabaseDetailsContext") { }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.ShipMobile)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.PromotionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);
        }
    }
}
/*<add name="DatabaseDetailsContext"
         providerName="System.Data.SqlClient"
         connectionString="data source=(local);initial catalog=DatabaseWebOpxin;persist security info=True;user id=sa;password=123456;" />
  </connectionStrings>*/
