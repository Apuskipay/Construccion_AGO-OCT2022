﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRUD
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class INTEC_AGU_OCT22Entities : DbContext
    {
        public INTEC_AGU_OCT22Entities()
            : base("name=INTEC_AGU_OCT22Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ClientType> ClientTypes { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Deparment> Deparments { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Restriction> Restrictions { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<UserRestriction> UserRestrictions { get; set; }
    }
}
