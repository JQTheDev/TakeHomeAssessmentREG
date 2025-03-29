using FuzzySharp;
using PersonMatching.FuzzyService;
using static PersonMatching.FuzzyService.DataScoreCalculator;


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
            EmailPrefix = email.Split("@")[0];
            PhoneNo = phoneNo;
            Birthday = birthday;
            FavColour = favColour;
        }
    }
    public class MatchResult
    {
        public Person MatchedPerson { get; set; }
        public double DataScore { get; set; }
        public double ConfidenceScore { get; set; }
    }

    public class MatchingAlgorithm
    {
        static Dictionary<string, double> weighting = new Dictionary<string, double>()
        {
            { "phoneNo", 0.99 },
            { "email", 0.99 },
            { "name", 0.7 },
            { "address", 0.6 },
            { "birthday", 0.45 },
            { "age", 0.2 },
            { "favColour", 0.1 }
        }; //to be edited. TODO: remove number and email. add emailPre

        public  List<MatchResult> CalculateMatch(Person candidate, List<Person> records)
        {
            //Todo: if fuzzy score for phone or fuzzy score for email, return {Person in DB, 1, 1}

            double dataScore = 0;
            double confidenceScore = 0;
            List<MatchResult> results = new List<MatchResult>();

            foreach (Person record in records) {

                double nameScore = GetSimilarityScore("name", candidate.Name, record.Name) * weighting["name"];
                double ageScore = GetSimilarityScore("age", candidate.Age, record.Age) * weighting["age"];
                double addressScore = GetSimilarityScore("address", candidate.Address, record.Address) * weighting["address"];
                double emailScore = GetSimilarityScore("email", candidate.Email, record.Email) * weighting["email"]; //handle appropraitely & add/replace with emailPre
                double phoneScore = GetSimilarityScore("phoneNo", candidate.PhoneNo, record.PhoneNo) * weighting["phoneNo"]; //handle appropraitely
                double birthdayScore = GetSimilarityScore("birthday", candidate.Birthday, record.Birthday) * weighting["birthday"];
                double favColourScore = GetSimilarityScore("favColour", candidate.FavColour, record.FavColour) * weighting["favColour"];

                confidenceScore = (nameScore + phoneScore + emailScore + addressScore) / 4; //TODO: Amend

               
            }
            return results;
        }
    }

    class Program
    {
         void Main(string[] args)
        {
            
            //Person candidate = new Person
            //("John A. Smith", 30, "300 Diff St, Chicago, USA", "john.q@example.com", "123-678-4567", "2024-08-26", "Red");

            //// fake DB with sample records.
            //List<Person> fakeDB = new List<Person> ()
            //{
            //    new Person("Ella M. Johnson", 29, "890 Cedar St, Cedarville, USA", "ella.johnson@example.com", "555-678-9012", "1995-06-12", "Blue"),
            //    new Person("Emma A. Lee", 39, "789 Spruce St, Bigcity, USA", "emma.lee@example.com", "555-210-9876", "1985-11-05", "Green"),
            //    new Person("Nathan R. Rodriguez", 32, "901 Elm St, Smalltown, USA", "nathan.rodriguez@example.com", "555-543-2109", "1992-09-30", "Red"),
            //    new Person("Michael B. Smith", 30, "456 Maple St, Midtown, USA", "michael.smith@example.com", "555-321-6549", "1994-03-15", "Red")
            //};

            //List<MatchResult> results = new List<MatchResult>();

            //foreach (var match in results)
            //{
            //    Console.WriteLine($"Matched Person: {match.MatchedPerson.Name}, Data Score: {match.DataScore}, Confidence: {match.ConfidenceScore}");
            //}

            //Console.WriteLine(DataScoreCalculator.GetSimilarityScore("123-456","456-123"));


    }

    } }
