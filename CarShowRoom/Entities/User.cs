//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarShowRoom.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Role Role { get; set; }
    }
}
