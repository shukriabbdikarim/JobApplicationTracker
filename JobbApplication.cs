using System;

namespace JobApplicationTracker
{
    public class JobApplication
    {
        public string CompanyName { get; set; }
        public string PositionTitle { get; set; }
        public Status Status { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int SalaryExpectation { get; set; }

        public JobApplication(string companyName, string positionTitle, int salary)
        {
            CompanyName = companyName;
            PositionTitle = positionTitle;
            SalaryExpectation = salary;
            ApplicationDate = DateTime.Now;
            Status = Status.Applied;
        }

        // Räknar antal dagar sedan ansökan skickades
        public int GetDaysSinceApplied()
        {
            return (DateTime.Now - ApplicationDate).Days;
        }

        // Kort textsammanfattning
        public string GetSummary()
        {
            string response = ResponseDate.HasValue ? ResponseDate.Value.ToShortDateString() : "Inget svar";
            return $"{CompanyName} - {PositionTitle} | Status: {Status} | Skickad: {ApplicationDate.ToShortDateString()} | Lön: {SalaryExpectation} kr | Svar: {response}";
        }
    }
}
