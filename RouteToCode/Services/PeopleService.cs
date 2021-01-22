using Microsoft.Extensions.Logging;
using RouteToCode.Models;
using System.Collections.Generic;
using System.Linq;

namespace RouteToCode.Services
{
    public class PeopleService : IPeopleService
    {
        private static readonly IList<Person> people = new List<Person>() { new(1, "Donald", "Duck"), new(2, "Mickey", "Mouse") };
        private readonly ILogger<PeopleService> logger;

        public PeopleService(ILogger<PeopleService> logger) => this.logger = logger;

        public IEnumerable<Person> Get() => people;

        public Person Get(int id)
        {
            logger.LogInformation("Trying to get person with ID {Id}...", id);

            var person = people.FirstOrDefault(p => p.Id == id);
            return person;
        }

        public void Add(Person person) => people.Add(person);
    }
}
