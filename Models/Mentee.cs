namespace Mentorship.Models
{
    public class Mentee
    {

        public int EmployeeId { get; set; }

        public string MenteeName { get; set; }
        public string MenteeMail {  get; set; }
        public string Unit {  get; set; }
        public int BenchAge {  get; set; }
        public string CurrentCity {  get; set; }
        public string SelectedPrimTechSkills { get; set; }
        public int MentorEmpId {  get; set; }
        public string MentorComments { get;set; }
        public DateTime MentorshipStartDate { get; set; }

        public DateTime MentorshipEndDate { get; set; }
        public bool IsReleased {  get; set; }   
        public string ReleasedReason { get; set; }
        public string ReleaseComments {  get; set; }


    }
}
