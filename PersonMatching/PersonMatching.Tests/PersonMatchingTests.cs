using System;
using PersonMatching;


namespace PersonMatching.Tests
{
    public class Tests
    {
        [Test]
        public void TestExactMatchEmailAndNumberOverridesAlgorithm()
        {
            var candidate = new Person("John A. Smith", "30", "300 Diff St, Chicago, USA", "john.q@example.com", "000-000-000", "2024-08-26", "Red");
            var record = new Person("These", "11", "are not, equal", "john.q@example.com", "123-678-4567", "2000-00-11", "in any way");

            var db = new List<Person> { record };
            var results = MatchingAlgorithm.CalculateMatch(candidate, db);

            //Email override
            Assert.That(results[0].ConfidenceScore, Is.EqualTo(100));
            Assert.That(results[0].SimilarityScore, Is.EqualTo(100));

            candidate = new Person("Alex M. Testing", "29", "Doesnt event matter", "ella.johnson@example.com", "555-678-9012", "1995-06-12", "Blue");
            record = new Person("Filipe M. Smith", "29", "this is random", "number override test", "555-678-9012", "1995-06-12", "red");

            db = new List<Person> { record };
            results = MatchingAlgorithm.CalculateMatch(candidate, db);

            //Number override
            Assert.That(results[0].ConfidenceScore, Is.EqualTo(100));
            Assert.That(results[0].SimilarityScore, Is.EqualTo(100));
        }

        [Test]
        public void TestLowSimilarityFiltersOutPerson()
        {
            var candidate = new Person("Ella M. Johnson", "29", "890 Cedar St, Cedarville, USA", "ella.johnson@example.com", "555-678-9012", "1995-06-12", "Blue");
            var record = new Person("Ella M. Johnson", "29", "890 Cedar St, Cedarville, USA", "ella.johnson@example.com", "555-678-9012", "1995-06-12", "Blue");
            var record2 = new Person("John A. Smith", "30", "300 Diff St, Chicago, USA", "john.q@example.com", "000-000-000", "2024-08-26", "Red");

            var db = new List<Person> { record, record2 };
            var results = MatchingAlgorithm.CalculateMatch(candidate, db);


            Assert.That(results.Count, Is.EqualTo(1)); //1 less than no. of records which show its been filtered out
            Assert.That(results[0].MatchedPerson.Name, Is.EqualTo("Ella M. Johnson"));
        }

        [Test]
        public void TestHighSimilarityHighConfidence()
        {
            var candidate = new Person("John Anthony Smith", "40", "300 Dif St, Chicago, USA", "john.smit@gmail.com", "123-678-4567", "2024-08-26", "Red");
            var record = new Person("John a. smity", "40", " Diff St 300, Chicago, USB", "johd.sm2th@hotmail.com", "199-678-4567", "2024-08-26", "Ref");

            var db = new List<Person> { record };
            var results = MatchingAlgorithm.CalculateMatch(candidate, db);

            Assert.That(results[0].ConfidenceScore, Is.GreaterThan(60));
            Assert.That(results[0].SimilarityScore, Is.GreaterThan(70));
        }

        [Test]
        public void TestPhoneNumberGetsNormalised()
        {
            var candidate = new Person("John ith", "30", "fffffffffffffff", "sghghh.com", "+1-123-678-4567", "20-08-26", "asa");
            var record = new Person("John A. Smith", "30", "aaaaaaaaaaa", "ssss.com", "123-678-4567", "2024-08-26", "fff");

            var db = new List<Person> { record };
            var results = MatchingAlgorithm.CalculateMatch(candidate, db);

            Assert.That(results[0].ConfidenceScore, Is.EqualTo(100)); //shows +1-... format does not affect result
            Assert.That(results[0].SimilarityScore, Is.EqualTo(100));
        }

        
    }
}