using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment
{
    public class SampleData : ISampleData
    {
        private String FileName { get; }

        //Constructor for file name, allows users more freedom to select the file they wish, as well as
        //Provides other means for testing.
        public SampleData(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            FileName = fileName;
        }

        // 1.   
        public IEnumerable<string> CsvRows
        {
            get
            {
                foreach (var line in File.ReadAllLines(FileName).Skip(1).ToArray())
                {
                    yield return line;
                }
            }
        }

        // 2.
        public IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows()
            => CsvRows
                .Select(line => CreatePerson(line))
                .Select(person => person.Address.State)
                .OrderBy(state => state)
                .Distinct();

        // 3.
        public string GetAggregateSortedListOfStatesUsingCsvRows()
        {
            string[] states = GetUniqueSortedListOfStatesGivenCsvRows().ToArray();

            return string.Join(",", states);
        }

        // 4.
        public IEnumerable<IPerson> People
            => CsvRows
                .Where(line => line.Split(",").Length == 8)
                .OrderBy(state => state.Split(",")[6])
                .ThenBy(city => city.Split(",")[5])
                .ThenBy(zip => zip.Split(",")[7])
                .Select(line => CreatePerson(line));

        // 5.
        public IEnumerable<(string FirstName, string LastName)> FilterByEmailAddress(
            Predicate<string> filter)
            => from person in People
               where filter(person.EmailAddress)
               select (person.FirstName, person.LastName);


        // 6.
        public string GetAggregateListOfStatesGivenPeopleCollection(
            IEnumerable<IPerson> people)
                => people
                    .Select(person => person.Address.State)
                    .Distinct()
                    .Aggregate((a, b) => $"{a},{b}");

        public Person ExternalCreatePerson(string line)
        {
            return CreatePerson(line);
        }

        public static Person CreatePerson(string person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            string[] personInfo = person.Split(",");
            Address address = CreateAddress(personInfo.Skip(4).ToArray());

            return new Person
            {
                FirstName = personInfo[1],
                LastName = personInfo[2],
                Address = address,
                EmailAddress = personInfo[3]
            };
        }

        public static Address CreateAddress(string[] addressInfo)
        {
            return new Address
            {
                StreetAddress = addressInfo[0],
                City = addressInfo[1],
                State = addressInfo[2],
                Zip = addressInfo[3]
            };
        }
    }
}
