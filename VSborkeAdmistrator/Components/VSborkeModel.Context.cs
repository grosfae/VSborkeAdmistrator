﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VSborkeAdmistrator.Components
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VSborkeBaseEntities : DbContext
    {
        public VSborkeBaseEntities()
            : base("name=VSborkeBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdditionComputerCaseImage> AdditionComputerCaseImage { get; set; }
        public virtual DbSet<AlignmentPowerBlock> AlignmentPowerBlock { get; set; }
        public virtual DbSet<ColorRGB> ColorRGB { get; set; }
        public virtual DbSet<ComputerCase> ComputerCase { get; set; }
        public virtual DbSet<CoolerSize> CoolerSize { get; set; }
        public virtual DbSet<FormFactor> FormFactor { get; set; }
        public virtual DbSet<FrontPanelMaterial> FrontPanelMaterial { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<IOPanel> IOPanel { get; set; }
        public virtual DbSet<LiquidCoolingSize> LiquidCoolingSize { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<MaterialSet> MaterialSet { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrientationMotherboard> OrientationMotherboard { get; set; }
        public virtual DbSet<PowerBlockStandartSupport> PowerBlockStandartSupport { get; set; }
        public virtual DbSet<PrimaryColor> PrimaryColor { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SecondColor> SecondColor { get; set; }
        public virtual DbSet<SidePanelFixation> SidePanelFixation { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TypeManagmentRGB> TypeManagmentRGB { get; set; }
        public virtual DbSet<TypeRGB> TypeRGB { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<WindowAlignment> WindowAlignment { get; set; }
        public virtual DbSet<WindowMaterial> WindowMaterial { get; set; }
    }
}
