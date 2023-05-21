//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VSborkeMaster.Components
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int FinallyPrice { get; set; }
        public int Count { get; set; }
        public Nullable<int> CountForCreate { get; set; }
        public int PricePerUnit { get; set; }
        public int Discount { get; set; }
        public Nullable<int> DeliveryPrice { get; set; }
        public int ComputerCaseId { get; set; }
        public Nullable<bool> IsReject { get; set; }
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
        public Nullable<System.DateTime> DateDelivery { get; set; }
        public string TimeDelivery { get; set; }
    
        public virtual ComputerCase ComputerCase { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
    }
}