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
    
    public partial class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CustomConfigurationId { get; set; }
        public Nullable<bool> IsReject { get; set; }
        public int StatusId { get; set; }
        public string CommentOrder { get; set; }
        public string ReasonReject { get; set; }
    
        public virtual CustomConfiguration CustomConfiguration { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
    }
}
