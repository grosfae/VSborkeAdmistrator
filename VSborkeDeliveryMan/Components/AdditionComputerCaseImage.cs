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
    
    public partial class AdditionComputerCaseImage
    {
        public int Id { get; set; }
        public int ComputerCaseId { get; set; }
        public byte[] AdditionImage { get; set; }
    
        public virtual ComputerCase ComputerCase { get; set; }
    }
}
