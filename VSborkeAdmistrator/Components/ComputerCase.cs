
//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
public partial class ComputerCase
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public ComputerCase()
    {

        this.AdditionComputerCaseImage = new HashSet<AdditionComputerCaseImage>();

        this.Favourite = new HashSet<Favourite>();

        this.FeedBack = new HashSet<FeedBack>();

        this.Order = new HashSet<Order>();

        this.PriceHistory = new HashSet<PriceHistory>();

    }


    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Count { get; set; }

    public int Price { get; set; }

    public int Discount { get; set; }

    public int FormFactorId { get; set; }

    public int Height { get; set; }

    public int Width { get; set; }

    public int Length { get; set; }

    public float Weight { get; set; }

    public int MaterialSetId { get; set; }

    public float MetalThickness { get; set; }

    public bool WindowOnSide { get; set; }

    public int WindowAlignmentId { get; set; }

    public int WindowMaterialId { get; set; }

    public int FrontPanelMaterialId { get; set; }

    public bool EAtx { get; set; }

    public bool FlexAtx { get; set; }

    public bool MicroAtx { get; set; }

    public bool MiniDtx { get; set; }

    public bool MiniItx { get; set; }

    public bool SsiCeb { get; set; }

    public bool SsiEeb { get; set; }

    public bool StandartAtx { get; set; }

    public bool ThinMiniItx { get; set; }

    public bool XlAtx { get; set; }

    public int PowerBlockStandartSupportId { get; set; }

    public int AlignmentPowerBlockId { get; set; }

    public int MaxLengthPowerBlock { get; set; }

    public int HorizontalAddonSlotId { get; set; }

    public int VerticalAddonSlotId { get; set; }

    public int MaxLengthVideocard { get; set; }

    public int MaxHeightCPUCooler { get; set; }

    public int SlotSSDId { get; set; }

    public int SlotHDDId { get; set; }

    public int SlotXHDDId { get; set; }

    public int CoolerInsideId { get; set; }

    public int SupportFrontCoolerId { get; set; }

    public int SupportBackCoolerId { get; set; }

    public int SupportTopCoolerId { get; set; }

    public int SupportSideCoolerId { get; set; }

    public int SupportBottomCoolerId { get; set; }

    public bool SupportLiquidCooling { get; set; }

    public int FrontLiquidCoolingId { get; set; }

    public int BackLiquidCoolingId { get; set; }

    public int BottomLiquidCoolingId { get; set; }

    public int SideLiquidCoolingId { get; set; }

    public int TopLiquidCoolingId { get; set; }

    public int OrientationMotherboardId { get; set; }

    public bool DustFilter { get; set; }

    public int PrimaryColorId { get; set; }

    public int SecondColorId { get; set; }

    public int IOPanelAlignmentId { get; set; }

    public int IOPanelId { get; set; }

    public bool CutCPUCooler { get; set; }

    public bool CableManagementBackSide { get; set; }

    public bool CardReader { get; set; }

    public int SidePanelFixationId { get; set; }

    public bool RGB { get; set; }

    public int TypeRGBId { get; set; }

    public int ColorRGBId { get; set; }

    public int TypeManagmentRGBId { get; set; }

    public int SourceRGBId { get; set; }

    public int ConnectorRGBId { get; set; }

    public byte[] MainImage { get; set; }

    public int ManufacturerId { get; set; }

    public string DeliverySet { get; set; }

    public bool IsAntiVibration { get; set; }

    public bool IsAccessable { get; set; }

    public bool IsDelete { get; set; }

    public bool IsCustom { get; set; }

    public int UserId { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<AdditionComputerCaseImage> AdditionComputerCaseImage { get; set; }

    public virtual AlignmentPowerBlock AlignmentPowerBlock { get; set; }

    public virtual BackLiquidCooling BackLiquidCooling { get; set; }

    public virtual BottomLiquidCooling BottomLiquidCooling { get; set; }

    public virtual ColorRGB ColorRGB { get; set; }

    public virtual ConnectorRGB ConnectorRGB { get; set; }

    public virtual CoolerInside CoolerInside { get; set; }

    public virtual FormFactor FormFactor { get; set; }

    public virtual FrontLiquidCooling FrontLiquidCooling { get; set; }

    public virtual FrontPanelMaterial FrontPanelMaterial { get; set; }

    public virtual HorizontalAddonSlot HorizontalAddonSlot { get; set; }

    public virtual IOPanel IOPanel { get; set; }

    public virtual IOPanelAlignment IOPanelAlignment { get; set; }

    public virtual Manufacturer Manufacturer { get; set; }

    public virtual MaterialSet MaterialSet { get; set; }

    public virtual OrientationMotherboard OrientationMotherboard { get; set; }

    public virtual PowerBlockStandartSupport PowerBlockStandartSupport { get; set; }

    public virtual PrimaryColor PrimaryColor { get; set; }

    public virtual SecondColor SecondColor { get; set; }

    public virtual SideLiquidCooling SideLiquidCooling { get; set; }

    public virtual SidePanelFixation SidePanelFixation { get; set; }

    public virtual SlotHDD SlotHDD { get; set; }

    public virtual SlotSSD SlotSSD { get; set; }

    public virtual SlotXHDD SlotXHDD { get; set; }

    public virtual SourceRGB SourceRGB { get; set; }

    public virtual SupportBackCooler SupportBackCooler { get; set; }

    public virtual SupportBottomCooler SupportBottomCooler { get; set; }

    public virtual SupportFrontCooler SupportFrontCooler { get; set; }

    public virtual SupportSideCooler SupportSideCooler { get; set; }

    public virtual SupportTopCooler SupportTopCooler { get; set; }

    public virtual TopLiquidCooling TopLiquidCooling { get; set; }

    public virtual TypeManagmentRGB TypeManagmentRGB { get; set; }

    public virtual TypeRGB TypeRGB { get; set; }

    public virtual User User { get; set; }

    public virtual VerticalAddonSlot VerticalAddonSlot { get; set; }

    public virtual WindowAlignment WindowAlignment { get; set; }

    public virtual WindowMaterial WindowMaterial { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Favourite> Favourite { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<FeedBack> FeedBack { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Order> Order { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<PriceHistory> PriceHistory { get; set; }

}

}
