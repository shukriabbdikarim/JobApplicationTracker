using System;
//Teständring i feature-menubranch 

namespace JobApplicationTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            JobManager manager = new JobManager();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== JOB APPLICATION TRACKER ===");
                Console.WriteLine("1. ➕ Lägg till ny ansökan");
                Console.WriteLine("2. 📋 Visa alla");
                Console.WriteLine("3. 🔍 Filtrera efter status");
                Console.WriteLine("4. 📅 Sortera efter datum");
                Console.WriteLine("5. 📊 Visa statistik");
                Console.WriteLine("6. ✏️ Uppdatera status");
                Console.WriteLine("7. 🗑️ Ta bort ansökan");
                Console.WriteLine("0. 💾 Avsluta");
                Console.Write("Välj: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddJob(manager);
                        break;
                    case "2":
                        manager.ShowAll();
                        break;
                    case "3":
                        FilterByStatus(manager);
                        break;
                    case "4":
                        manager.ShowSortedByDate();
                        break;
                    case "5":
                        manager.ShowStatistics();
                        break;
                    case "6":
                        UpdateStatus(manager);
                        break;
                    case "7":
                        RemoveJob(manager);
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Fel val, försök igen.");
                        break;
                }
            }

            Console.WriteLine("Programmet avslutas. Hejdå!");
        }

        static void AddJob(JobManager manager)
        {
            Console.Write("Företag: ");
            string company = Console.ReadLine();
            Console.Write("Position: ");
            string position = Console.ReadLine();
            Console.Write("Lön (kr): ");
            int salary = int.Parse(Console.ReadLine());

            var app = new JobApplication(company, position, salary);
            manager.AddJob(app);
            Console.WriteLine("✅ Ansökan tillagd!");
        }

        static void FilterByStatus(JobManager manager)
        {
            Console.WriteLine("Välj status: 0=Applied, 1=Interview, 2=Offer, 3=Rejected");
            if (int.TryParse(Console.ReadLine(), out int s) && Enum.IsDefined(typeof(Status), s))
            {
                manager.ShowByStatus((Status)s);
            }
            else
            {
                Console.WriteLine("Ogiltigt val.");
            }
        }

        static void UpdateStatus(JobManager manager)
        {
            manager.ShowAll();
            Console.Write("Ange index att uppdatera: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Ny status: 0=Applied, 1=Interview, 2=Offer, 3=Rejected");
                if (int.TryParse(Console.ReadLine(), out int s) && Enum.IsDefined(typeof(Status), s))
                {
                    manager.UpdateStatus(index, (Status)s);
                }
            }
        }

        static void RemoveJob(JobManager manager)
        {
            manager.ShowAll();
            Console.Write("Ange index att ta bort: ");
            if (int.TryParse(Console.ReadLine(), out int index))
                manager.RemoveJob(index);
        }
    }
}
