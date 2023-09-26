using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("name=StrConnect") { }
        public DbSet<Config> Configs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Config_setting> Config_settings { get; set; }
        public DbSet<Product_image> Product_images { get; set; }
        public DbSet<Product_option> Product_options { get; set; }
        public DbSet<Product_sale> Product_sales { get; set; }
        public DbSet<Product_store> Product_stores { get; set; }
        public DbSet<Product_value> Product_values { get; set; }

    }
}
