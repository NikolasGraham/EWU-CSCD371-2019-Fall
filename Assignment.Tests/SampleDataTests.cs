using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assignment.Tests
{
    [TestClass]
    public class SampleDataTests
    {
        string TempFilePath;

        string[] people =
            {
                "13,Daile,Croser,dcroserc@reverbnation.com,3699 Duke Parkway,Winston Salem,NC,44527",
                "14,Editha,Loseke,eloseked@posterous.com,39431 Lotheville Pass,Washington,DC,73484",
                "15,Phillida,Chastagnier,pchastagniere@reference.com,1 Rutledge Point,Spokane,WA,99021",
                "16,Ewart,Puckinghorne,epuckinghornef@indiatimes.com,9 Forster Lane,Lincoln,NE,40053",
                "17,Sancho,Mahony,smahonyg@stanford.edu,90 Birchwood Street,Las Vegas,NV,36230",
                "18,Iggy,Baughen,ibaughenh@addthis.com,95574 Pond Crossing,Winston Salem,NC,76871",
                "19,Fayette,Dougherty,fdoughertyi@stanford.edu,6487 Pepper Wood Court,Spokane,WA,99021",
                "34,Sayres,Rumble,srumblex@addthis.com,56 Milwaukee Park,Minneapolis,MN,17309",
                "35,Marlow,Gossart,mgossarty@elpais.com,711 Cambridge Court,Pasadena,TX,73914",
                "36,Adrea,Lay,alayz@spotify.com,79 Pond Park,Salt Lake City,UT,63481",
                "37,Westley,Mesnard,wmesnard10@amazonaws.com,075 Pierstorff Road,Manchester,NH,66946"
            };

        [TestInitialize]
        public void init()
        {
            TempFilePath = Path.GetTempFileName();

            using (StreamWriter SW = new StreamWriter(TempFilePath))
            {
                SW.WriteLine("Id,FirstName,LastName,Email,StreetAddress,City,State,Zip");
                foreach(string line in people)
                {
                    SW.WriteLine(line);
                }
            }
        }

        [TestCleanup]
        public void cleanup()
        {
            if (File.Exists(TempFilePath))
            {
                File.Delete(TempFilePath);
            }
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullFileName()
        {
            SampleData SD = new SampleData(null);
        }

        [TestMethod]
        public void CsvRows_IterateThroughFile()
        {
            SampleData SD = new SampleData(TempFilePath);

            List<string> list = new List<string>();
            foreach (string line in SD.CsvRows)
            {
                list.Add(line);
            }

            string[] grabbed = list.ToArray();

            for(int i=0; i < people.Length; i++)
            {
                Assert.AreEqual(people[i], grabbed[i]);
            }
        }

        [TestMethod]
        public void GetUniqueSortedListOfStatesGivenCsvRows_Hardcoded_ReturnsSortedList()
        {
            SampleData SD = new SampleData(TempFilePath);

            IEnumerable<string> data = SD.GetUniqueSortedListOfStatesGivenCsvRows();

            string[] expected = { "DC", "MN", "NC", "NE", "NH", "NV", "TX", "UT", "WA" };

            Assert.IsTrue(data.SequenceEqual(expected));
        }

        [TestMethod]
        public void GetUniqueSortedListOfStatesGivenCsvRows_HardcodedSpokane_ReturnsSortedList()
        {
            string TempFilePathSpokane = Path.GetTempFileName();
            string[] spokane =
            {
            "Id,FirstName,LastName,Email,StreetAddress,City,State,Zip",
            "1,Joly,Scneider,jscneider7@pagesperso-orange.fr,53 Grim Point,Spokane,WA,99022",
            "2,Phillida,Chastagnier,pchastagniere@reference.com,1 Rutledge Point,Spokane,WA,99021",
            "3,Fayette,Dougherty,fdoughertyi@stanford.edu,6487 Pepper Wood Court,Spokane,WA,99021",
            "4,Molly,Jeannot,mjeannotp@google.ca,00546 International Alley,Spokane,WA,99021",
            "5,Maria,Rawsthorne,mrawsthorneq@slate.com,609 Kedzie Alley,Spokane,WA,99021",
            "6,Celestyna,Robken,crobken12@t.co,27 Moland Parkway,Spokane,WA,99021",
            "7,Selia,Bowe,sbowe13@360.cn,42 Lerdahl Plaza,Spokane,WA,99021",
            "8,Arthur,Myles,amyles1c@miibeian.gov.cn,4718 Thackeray Pass,Spokane,WA,99021",
            "9,Claudell,Leathe,cleathe1d@columbia.edu,30262 Steensland Way,Spokane,WA,99021"
            };
            using (StreamWriter SW = new StreamWriter(TempFilePathSpokane))
            {
                SW.WriteLine("Id,FirstName,LastName,Email,StreetAddress,City,State,Zip");
                foreach (string line in spokane)
                {
                    SW.WriteLine(line);
                }
            }

            SampleData SD = new SampleData(TempFilePathSpokane);

            IEnumerable<string> data = SD.GetUniqueSortedListOfStatesGivenCsvRows().Skip(1);

            string[] expected = {"WA" };

            string test = "";
            foreach (string line in data)
            {
                test += line + " ";
            }

            Assert.IsTrue(data.SequenceEqual(expected));

            if (File.Exists(TempFilePathSpokane))
            {
                File.Delete(TempFilePathSpokane);
            }
        }

        [TestMethod]
        public void GetAggregateSortedListOfStatesUsingCsvRows_()
        {
            SampleData SD = new SampleData(TempFilePath);

            string expected = "DC,MN,NC,NE,NH,NV,TX,UT,WA";
            string data = SD.GetAggregateSortedListOfStatesUsingCsvRows();

            Assert.AreEqual(expected, data);
        }

        [TestMethod]
        public void GetUniqueSortedListOfStatesGivenCsvRows_UsingLINQToTest()
        {
            SampleData SD = new SampleData(TempFilePath);

            IEnumerable<string> data = SD.GetUniqueSortedListOfStatesGivenCsvRows();

            IEnumerable<string> expected = SD.CsvRows
                .Select(line => line.Split(",")[6])
                .OrderBy(state => state)
                .Distinct();

            Assert.IsTrue(expected.SequenceEqual(data));
        }

        [TestMethod]
        public void ISampleData_People_PersonCollectionReturned_MissingAddressesIgnored()
        {
            string TempFilePathAddress = Path.GetTempFileName();
            string[] addresses =
            {
            "Id,FirstName,LastName,Email,StreetAddress,City,State,Zip",
            "1,Joly,Scneider,jscneider7@pagesperso-orange.fr,53 Grim Point,Spokane,WA,99022",
            "2,Phillida,Chastagnier,pchastagniere@reference.com,1 Rutledge Point,Spokane,WA,99021",
            "3,Fayette,Dougherty,fdoughertyi@stanford.edu",
            "4,Molly,Jeannot,mjeannotp@google.ca,00546 International Alley,Spokane,WA,99021",
            "5,Maria,Rawsthorne,mrawsthorneq@slate.com,609 Kedzie Alley,Spokane,WA,99021",
            "6,Celestyna,Robken,crobken12@t.co",
            "7,Selia,Bowe,sbowe13@360.cn,42 Lerdahl Plaza,Spokane,WA,99021",
            "8,Arthur,Myles,amyles1c@miibeian.gov.cn,4718 Thackeray Pass,Spokane,WA,99021",
            "9,Claudell,Leathe,cleathe1d@columbia.edu"
            };
            using (StreamWriter SW = new StreamWriter(TempFilePathAddress))
            {
                SW.WriteLine("Id,FirstName,LastName,Email,StreetAddress,City,State,Zip");
                foreach (string line in addresses)
                {
                    SW.WriteLine(line);
                }
            }

            SampleData SD = new SampleData(TempFilePathAddress);

            IEnumerable<IPerson> data = SD.People.Skip(1);

            Assert.IsTrue(data.ToArray().Length == 6);
        }

        [TestMethod]
        public void ISampleData_People_PersonCollectionReturned()
        {
            SampleData SD = new SampleData(TempFilePath);

            IEnumerable<IPerson> data = SD.People;

            IPerson[] dataAra  = data.ToArray();

            string[] expected =
            {
                "14,Editha,Loseke,eloseked@posterous.com,39431 Lotheville Pass,Washington,DC,73484",
                "34,Sayres,Rumble,srumblex@addthis.com,56 Milwaukee Park,Minneapolis,MN,17309",
                "13,Daile,Croser,dcroserc@reverbnation.com,3699 Duke Parkway,Winston Salem,NC,44527",
                "18,Iggy,Baughen,ibaughenh@addthis.com,95574 Pond Crossing,Winston Salem,NC,76871",
                "16,Ewart,Puckinghorne,epuckinghornef@indiatimes.com,9 Forster Lane,Lincoln,NE,40053",
                "37,Westley,Mesnard,wmesnard10@amazonaws.com,075 Pierstorff Road,Manchester,NH,66946",
                "17,Sancho,Mahony,smahonyg@stanford.edu,90 Birchwood Street,Las Vegas,NV,36230",
                "35,Marlow,Gossart,mgossarty@elpais.com,711 Cambridge Court,Pasadena,TX,73914",
                "36,Adrea,Lay,alayz@spotify.com,79 Pond Park,Salt Lake City,UT,63481",
                "15,Phillida,Chastagnier,pchastagniere@reference.com,1 Rutledge Point,Spokane,WA,99021",
                "19,Fayette,Dougherty,fdoughertyi@stanford.edu,6487 Pepper Wood Court,Spokane,WA,99021",
            };

            for(int i=0; i < dataAra.Length; i++)
            {
                IPerson temp = SD.ExternalCreatePerson(expected[i]);
                Assert.AreEqual(temp.Address.State, dataAra[i].Address.State);
                Assert.AreEqual(temp.Address.City, dataAra[i].Address.City);
                Assert.AreEqual(temp.Address.Zip, dataAra[i].Address.Zip);
            }
        }

        [TestMethod]
        public void FilterByEmailAddress_CorrectlyFindsPeopleFromEmail()
        {
            SampleData SD = new SampleData(TempFilePath);

            (string, string)[] expected =
            {
                ("Editha","Loseke"),
                ("Ewart","Puckinghorne")
            };

            IEnumerable<(string, string)> data = SD.FilterByEmailAddress(email => email.StartsWith("e", StringComparison.Ordinal));

            foreach((string,string) tuple in expected)
            {
                Assert.IsTrue(data.Contains(tuple));
            }

            Assert.AreEqual(2, data.Count());
        }

        [TestMethod]
        public void GetAggregateListOfStatesGivenPeopleCollection_CompareWithCsvRows()
        {
            SampleData SD = new SampleData(TempFilePath);

            string expected = "";

            IEnumerable<string> states = SD.GetUniqueSortedListOfStatesGivenCsvRows();

            foreach(string state in states)
            {
                expected += state + ",";
            }

            string result = SD.GetAggregateListOfStatesGivenPeopleCollection(SD.People);

            Assert.AreEqual(expected, result + ",");
        }

        [TestMethod]
        public void GetAggregateListOfStatesGivenPeopleCollection_CompareWithCsvRows_HardCoded()
        {
            SampleData SD = new SampleData(TempFilePath);

            IEnumerable<string> expectedIterable = SD.CsvRows;
            string expected = "DC,MN,NC,NE,NH,NV,TX,UT,WA";

            string result = SD.GetAggregateListOfStatesGivenPeopleCollection(SD.People);

            Assert.AreEqual(expected, result);
        }
    }
}
