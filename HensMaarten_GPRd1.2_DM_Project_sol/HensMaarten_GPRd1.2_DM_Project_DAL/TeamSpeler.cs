//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HensMaarten_GPRd1._2_DM_Project_DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TeamSpeler
    {
        public int id { get; set; }
        public Nullable<int> spelerId { get; set; }
        public int teamId { get; set; }
    
        public virtual Speler Speler { get; set; }
        public virtual Team Team { get; set; }
    }
}
