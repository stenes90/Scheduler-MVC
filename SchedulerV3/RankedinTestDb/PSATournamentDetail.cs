//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchedulerV3.RankedinTestDb
{
    using System;
    using System.Collections.Generic;
    
    public partial class PSATournamentDetail
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int PSATournamentID { get; set; }
        public string PSATournamentKey { get; set; }
        public Nullable<System.DateTime> SyncDate { get; set; }
        public Nullable<int> SyncResponseCode { get; set; }
        public string SyncResponseDescription { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
    
        public virtual Tournament Tournament { get; set; }
    }
}