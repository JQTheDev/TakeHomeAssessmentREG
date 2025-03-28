using System;
using System.Collections.Generic;

namespace PersonMatching
{
    public class Person
    {
        public string[] Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Birthday { get; set; }
        public string FavColour { get; set; }

        public Person(string name, int age, string address, string email, string phoneNo, string birthday, string favColour)
        {
            Name = name.Split(" ");
            Age = age;
            Address = address;
            Email = email;
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

    public static class MatchingAlgorithm
    {
        public static List<MatchResult> CalculateMatch(Person candidate, List<Person> record)
        {
            double dataScore = 0;
            double confidenceScore = 0;
            List<MatchResult> results = new List<MatchResult>();

           

            return results;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person candidate = new Person
            ("John A. Smith", 30, "300 Diff St, Chicago, USA", "john.q@example.com", "123-678-4567", "2024-08-26", "Red");

            // fake DB with sample records.
            List<Person> fakeDB = new List<Person> ()
             {
                new Person("Ella M. Johnson", 29, "890 Cedar St, Cedarville, USA", "ella.johnson@example.com", "555-678-9012", "1995-06-12", "Blue"),
                new Person("Emma A. Lee", 39, "789 Spruce St, Bigcity, USA", "emma.lee@example.com", "555-210-9876", "1985-11-05", "Green"),
                new Person("Nathan R. Rodriguez", 32, "901 Elm St, Smalltown, USA", "nathan.rodriguez@example.com", "555-543-2109", "1992-09-30", "Red"),
                new Person("Michael B. Smith", 30, "456 Maple St, Midtown, USA", "michael.smith@example.com", "555-321-6549", "1994-03-15", "Red")
             };

            List<MatchResult> results = new List<MatchResult>();

                        foreach (var match in results)
            {
                Console.WriteLine($"Matched Person: {match.MatchedPerson.Name}, Data Score: {match.DataScore}, Confidence: {match.ConfidenceScore}");
            }

        }
    }
}
