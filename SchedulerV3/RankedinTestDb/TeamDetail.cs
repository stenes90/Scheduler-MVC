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
    
    public partial class TeamDetail
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int InitiatorId { get; set; }
        public Nullable<int> HomeClubId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<int> TeamLeagueId { get; set; }
        public Nullable<int> TeamLeaguePoolId { get; set; }
    
        public virtual Organisation Organisation { get; set; }
        public virtual Participant Participant { get; set; }
        public virtual Participant Participant1 { get; set; }
        public virtual TeamLeague TeamLeague { get; set; }
        public virtual TeamLeaguePool TeamLeaguePool { get; set; }
    }
}