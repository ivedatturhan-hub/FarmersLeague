namespace FarmersLeague.ML.DTOs
{
    public class MatchReportDTO
    {
        public string FinalScore { get; set; }
        public int HostAttack { get; set; }
        public int HostDefence { get; set; }
        public int HostChances { get; set; }

        public int VisitorAttack { get; set; }
        public int VisitorDefence { get; set; }
        public int VisitorChances { get; set; }
    }
}