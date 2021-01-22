using RouteToCode.Models;
using System.Collections.Generic;

namespace RouteToCode.Services
{
    public interface IPeopleService
    {
        void Add(Person person);
        IEnumerable<Person> Get();
        Person Get(int id);
    }
}