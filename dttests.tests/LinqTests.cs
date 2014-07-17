using dttests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace dttests.tests
{
    [TestClass]
    public class LinqTests
    {
        private IQueryable<Person> persons;
        private IList<Col> cols;

        public LinqTests()
        {
            this.persons = new List<Person>()
            {
                new Person("Aurelio", "Lamberti", 43),
                new Person("Warren", "Brownstein", 20),
                new Person("Aaron", "Brownstein", 24),
                new Person("Katherine", "Consiglio", 45),
                new Person("Wally", "Barkley", 50),
                new Person("Artie", "Cessna", 34),
                new Person("Ji", "Oviatt", 65),
                new Person("Takisha", "Loftis", 16),
                new Person("Fonda", "Weaver", 19),
                new Person("Sheldon", "Christian", 27)
            }.AsQueryable();

            this.cols = new List<Col>() {
                new Col() { name = "firstName", isOrderable = true, orderNumber = 1, sortDir = "asc", search = "Warren" },
                new Col() { name = "lastName", isOrderable = true, orderNumber = 0, sortDir = "asc", search = "Brownstein" },
                new Col() { name = "age", isOrderable = true, orderNumber = 2, sortDir = "asc" }
            };
        }
       


        [TestMethod]
        public void GroupByTest()
        {

            var result = persons.GroupBy(x => x.age).Select(x => x.Key).OrderBy(x => x).ToList();
            Assert.AreEqual(8, result.Count);
            Assert.AreEqual(16, result.First());
        }

        [TestMethod]
        public void OrderByMany()
        {
            var orderByMany = this.cols.OrderBy(x => x.orderNumber).Select(x => string.Format("{0} {1}", x.name, x.sortDir.ToUpper())).ToArray();
            string orderBy = string.Join(",", orderByMany);

            var resultA = persons.OrderBy(x => x.lastName).ThenBy(x => x.firstName);
            var resultB = persons.OrderBy(orderBy);
            var resultC = persons.OrderBy("lastName ASC, firstName DESC");

            Assert.AreEqual("Aaron", resultA.Skip(1).First().firstName);
            Assert.AreEqual("Aaron", resultB.Skip(1).First().firstName);
            Assert.AreEqual("Warren", resultC.Skip(1).First().firstName);

        }

        [TestMethod]
        public void GetPropertyTest()
        {
            var data = this.persons;

            var searchCols = cols.Where(x => !string.IsNullOrWhiteSpace(x.search));
            foreach (Col col in searchCols)
            {
                data = data.FilterByValue(col.name, col.search);
            }

            Assert.AreEqual("Warren", data.First().firstName);
            Assert.AreEqual("Brownstein", data.First().lastName);
        }
    }

    public class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }

        public Person() { }
        public Person(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
        }
    }
    public class Col
    {
        public string name { get; set; }
        public bool isOrderable { get; set; }
        public string sortDir { get; set; }
        public int orderNumber { get; set; }
        public string search { get; set; }
    }

}
