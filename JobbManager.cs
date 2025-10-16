using System;
using System.Collections.Generic;
using System.Linq;

namespace JobApplicationTracker
{
    public class JobManager
    {
        public List<JobApplication> Applications { get; set; } = new List<JobApplication>();

        public void AddJob(JobApplication app)
        {
            Applications.Add(app);
        }
        //uppdaterar status på en ansökan
        
        public void UpdateStatus(int index, Status newStatus)
        {
            if (index >= 0 && index < Applications.Count)
            {
                Applications[index].Status = newStatus;
                if (newStatus != Status.Applied)
                    Applications[index].ResponseDate = DateTime.Now;
                Console.WriteLine(" Status uppdaterad!");
            }
            else
            {
                Console.WriteLine("Fel index.");
            }
        }

        public void RemoveJob(int index)
        {
            if (index >= 0 && index < Applications.Count)
            {
                Applications.RemoveAt(index);
                Console.WriteLine("🗑️ Ansökan borttagen!");
            }
            else Console.WriteLine("Fel index.");
        }
        //visar alla ansökningar
        public void ShowAll()
        {
            if (!Applications.Any())
            {
                Console.WriteLine("Inga ansökningar ännu.");
                return;
            }

            for (int i = 0; i < Applications.Count; i++)
            {
                Console.WriteLine($"[{i}] {Applications[i].GetSummary()}");
            }
        }
        //LINQ1: Filtera efter status
        public void ShowByStatus(Status status)
        {
            var result = Applications.Where(a => a.Status == status);

            if (!result.Any())
            {
                Console.WriteLine($"Inga ansökningar med status {status}.");
                return;
            }

            foreach (var a in result)
                Console.WriteLine(a.GetSummary());
        }
        //LINQ 2 : Sortera efter datum
        public void ShowSortedByDate()
        {
            var sorted = Applications.OrderBy(a => a.ApplicationDate);
            Console.WriteLine("📅 Sorterade ansökningar (äldst först):");
            foreach (var a in sorted)
                Console.WriteLine(a.GetSummary());
        }
        //LINQ 3: Visa statistik
        public void ShowStatistics()
        {
            Console.WriteLine($"Totalt antal ansökningar: {Applications.Count}");

            var perStatus = Applications
                .GroupBy(a => a.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() });

            foreach (var s in perStatus)
                Console.WriteLine($"{s.Status}: {s.Count}");

            var answered = Applications.Where(a => a.ResponseDate != null);
            if (answered.Any())
            {
                var avg = answered.Average(a => (a.ResponseDate.Value - a.ApplicationDate).TotalDays);
                Console.WriteLine($"Genomsnittlig svarstid: {avg:F1} dagar");
            }
            else
            {
                Console.WriteLine("Inga svar ännu.");
            }
        }
    }
}
