using System;
using System.Collections.Generic;

namespace Reports.DAL.Entities
{
    public class FinalReport
    {
        public FinalReport(Guid id, Guid teamLeadId)
        {
            CreationTime = DateTime.Now;
            Id = id;
            TeamLeadId = teamLeadId;
        }
        
        private FinalReport() { }
        public Guid Id { get; private init; }
        public DateTime CreationTime { get; private init; }
        public List<Report> Reports { get; set; }
        public Guid TeamLeadId { get; private init; }
    }
}