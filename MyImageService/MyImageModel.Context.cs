﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyImageService
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyImageEntities : DbContext
    {
        public MyImageEntities()
            : base("name=MyImageEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tb_admin> tb_admin { get; set; }
        public virtual DbSet<tb_customer> tb_customer { get; set; }
        public virtual DbSet<tb_deliverytime> tb_deliverytime { get; set; }
        public virtual DbSet<tb_image> tb_image { get; set; }
        public virtual DbSet<tb_order> tb_order { get; set; }
        public virtual DbSet<tb_orderdetail> tb_orderdetail { get; set; }
        public virtual DbSet<tb_printsize> tb_printsize { get; set; }
        public virtual DbSet<tb_shippingcategory> tb_shippingcategory { get; set; }
    }
}
