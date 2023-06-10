//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VSborkeDeliveryMan.Components
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.DeliverManOrder = new HashSet<DeliverManOrder>();
        }
    
        public int Id { get; set; }
        public int UserId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int FinallyPrice { get; set; }
        public int GeneralCount { get; set; }
        public int CountInAccessable { get; set; }
        public int CountForCreate { get; set; }
        public int PricePerUnitStock { get; set; }
        public int PricePerUnit { get; set; }
        public int Discount { get; set; }
        public int DeliveryPrice { get; set; }
        public float TotalWeight { get; set; }
        public int ComputerCaseId { get; set; }
        public int StatusId { get; set; }
        public string CommentOrder { get; set; }
        public string ReasonReject { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FlatNumber { get; set; }
        public Nullable<bool> PrivateHome { get; set; }
        public Nullable<bool> UpToFloor { get; set; }
        public string Floor { get; set; }
        public Nullable<bool> LiftForFullOrder { get; set; }
        public System.DateTime DateDelivery { get; set; }
        public string TimeDelivery { get; set; }
        public Nullable<System.DateTime> DateForConstruct { get; set; }
        public Nullable<System.DateTime> DateForPocket { get; set; }
        public Nullable<bool> DeleteFromHistory { get; set; }
        public Nullable<bool> IsAcceptedOperator { get; set; }
        public Nullable<bool> IsAcceptedMaster { get; set; }
        public Nullable<bool> IsForMaster { get; set; }
        public Nullable<bool> IsForStorager { get; set; }
        public Nullable<bool> IsForDeliveler { get; set; }
        public Nullable<bool> IsReadyMaster { get; set; }
        public Nullable<bool> IsRejectedOperator { get; set; }
        public Nullable<bool> IsRejectedMaster { get; set; }
        public Nullable<bool> IsRejectedStorager { get; set; }
        public Nullable<bool> IsPocketed { get; set; }
        public Nullable<bool> IsStoraged { get; set; }
        public Nullable<bool> IsTakeForDelivery { get; set; }
        public Nullable<bool> IsRejectDelivery { get; set; }
        public Nullable<bool> IsReject { get; set; }
    
        public virtual ComputerCase ComputerCase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliverManOrder> DeliverManOrder { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
    }
}
