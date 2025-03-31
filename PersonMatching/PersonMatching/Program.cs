using FuzzySharp;
using PersonMatching.FuzzyService;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using static PersonMatching.FuzzyService.SimilarityScoreCalculator;


namespace PersonMatching
{
    public class Person
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string EmailPrefix { get; set; }
        public string PhoneNo { get; set; }
        public string Birthday { get; set; }
        public string FavColour { get; set; }

        public Person(string name, string age, string address, string email, string phoneNo, string birthday, string favColour)
        {
            Name = name;
            Age = age;
            Address = address;
            Email = email;
            EmailPrefix = email.Split("@")[0]; //gets the string before the @symbol and disregards the rest
            PhoneNo = phoneNo;
            Birthday = birthday;
            FavColour = favColour;
        }
    }
    public class MatchResult
    {
        public Person MatchedPerson { get; set; }
        public double SimilarityScore { get; set; }
        public double ConfidenceScore { get; set; }

        public MatchResult(Person person, double confidenceScore, double similarityScore) 
        {
            MatchedPerson = person;
            ConfidenceScore = confidenceScore;
            SimilarityScore = similarityScore;
        }
    }

    public class MatchingAlgorithm
    {
        static Dictionary<string, double> weighting = new Dictionary<string, double>()
        {
            { "emailPre", 0.85 },
            { "name", 0.7 },
            { "address", 0.6 },
            { "birthday", 0.45 },
            { "age", 0.2 },
            { "favColour", 0.1 }
        };

        public static List<MatchResult> CalculateMatch(Person candidate, List<Person> records)
        {

            double similarityScore = 0;
            double confidenceScore = 0;
            List<MatchResult> results = new List<MatchResult>();
            HashSet<string> uniqueVals = new HashSet<string>();

            foreach (Person record in records) {
                if (uniqueVals.Contains(record.Email)|| uniqueVals.Contains(record.PhoneNo)) {
                    continue; //skip duplicates
                }
                if (candidate.PhoneNo[0] == '+')
                {
                    candidate.PhoneNo = candidate.PhoneNo.Substring(3); //Example +1-123-456-7890 returns 123-456-7890
                }
                if ((GetSimilarityScore("phoneNo", candidate.PhoneNo, record.PhoneNo) == 1.0 || GetSimilarityScore("email", candidate.Email, record.Email) == 1.0))
                {
                    results.Add(new MatchResult(record, 100, 100));
                    uniqueVals.Add(record.Email);
                    uniqueVals.Add(record.PhoneNo);
                    continue; //no need to do additional calculations. Person will be 100% match if either of these unique fields match exactly.
                }
                
                double nameSimilarityScore = GetSimilarityScore("name", candidate.Name, record.Name);
                double ageSimilarityScore = GetSimilarityScore("age", candidate.Age, record.Age);
                double addressSimilarityScore = GetSimilarityScore("address", candidate.Address, record.Address);
                double emailPreSimilarityScore = GetSimilarityScore("emailPre", candidate.EmailPrefix, record.EmailPrefix);
                double birthdaySimilarityScore = GetSimilarityScore("birthday", candidate.Birthday, record.Birthday);
                double favColourSimilarityScore = GetSimilarityScore("favColour", candidate.FavColour, record.FavColour);

                double nameWeightedScore = nameSimilarityScore * weighting["name"];
                double ageWeightedScore = ageSimilarityScore * weighting["age"];
                double addressWeightedScore = addressSimilarityScore * weighting["address"];
                double emailPreWeightedScore = emailPreSimilarityScore * weighting["emailPre"];
                double birthdayWeightedScore = birthdaySimilarityScore * weighting["birthday"];
                double favColourWeightedScore = favColourSimilarityScore * weighting["favColour"];

                double totalWeight = 0;

                foreach (var pair in weighting)
                {
                    totalWeight += pair.Value;
                }

                confidenceScore = (nameWeightedScore + birthdayWeightedScore + emailPreWeightedScore + addressWeightedScore + favColourWeightedScore + ageWeightedScore) / totalWeight;
                similarityScore = (nameSimilarityScore + ageSimilarityScore + addressSimilarityScore + emailPreSimilarityScore + birthdaySimilarityScore + favColourSimilarityScore) / 6.0;
                //divided by 6 as that is the highest possible outcome when adding all 6 properties being considered.

                if (confidenceScore > 0.5) {
                    results.Add(new MatchResult(record, confidenceScore * 100, similarityScore * 100)); // n*100 to turn decimal to %
                }
                uniqueVals.Add(record.Email);
                uniqueVals.Add(record.PhoneNo);
            }
            return results;
        }
    }

    class Program
    {
         static void Main(string[] args)
        {

            Person candidate = new Person
            ("John A. Smith", "30", "300 Diff St, Chicago, USA", "john.q@example.com", "555-321-6549", "2024-08-26", "Red");

            // fake DB with sample records.
            List<Person> fakeDB = new List<Person>()
            {
                new Person("Ella M. Johnson", "29", "890 Cedar St, Cedarville, USA", "ella.johnson@example.com", "555-678-9012", "1995-06-12", "Blue"),
                new Person("Emma A. Lee", "39", "789 Spruce St, Bigcity, USA", "emma.lee@example.com", "555-210-9876", "1985-11-05", "Green"),
                new Person("Nathan R. Rodriguez", "32", "901 Elm St, Smalltown, USA", "nathan.rodriguez@example.com", "555-543-2109", "1992-09-30", "Red"),
                new Person("Michael B. Smith", "30", "456 Maple St, Midtown, USA", "michael.smith@example.com", "555-321-6549", "1994-03-15", "Red")
            };

            //Console.WriteLine(SimilarityScoreCalculator.GetSimilarityScore("name", "John Anthony Smith", "John A. Smith"));

            var answer = MatchingAlgorithm.CalculateMatch(candidate, fakeDB);

            foreach (var field in answer)
            {
                Console.WriteLine(field.MatchedPerson.Name);
                Console.WriteLine($"Confidence Score: {field.ConfidenceScore}%");
                Console.WriteLine($"Similarity Score: {field.SimilarityScore}%");
            }

        }

    } }
