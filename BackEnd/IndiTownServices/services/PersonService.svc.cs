using Database.DAC.CRUD;
using Interfaces.DataContracts;
using Interfaces.ServiceContracts;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace IndiTownServices.services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PersonService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PersonService.svc or PersonService.svc.cs at the Solution Explorer and start debugging.
    public class PersonService : IPersonService
    {
        public void CreatePerson(Person person)
        {
            IPersonCRUD<Person> crud = new PersonCRUD<Person>();
            crud.Initialize();
            crud.Create(person);
        }

        public void UpdatePerson(Person person)
        {
            IPersonCRUD<Person> crud = new PersonCRUD<Person>();
            crud.Initialize();
            crud.Update(person);
        }

        public Person GetPerson(string userId)
        {
            IPersonCRUD<Person> crud = new PersonCRUD<Person>();
            crud.Initialize();
            return crud.Read(x => x.UserId == userId).FirstOrDefault();
        }

        public PersonProfile GetPersonProfile(string userId)
        {
            Person person = GetPerson(userId);
            ReviewService reveiwSvc = new ReviewService();
            IEnumerable<Review> reviews = reveiwSvc.GetUserReviews(userId);
            PersonProfile personProfile = new PersonProfile();
            personProfile.Person = person;
            personProfile.Reviews = reviews;
            return personProfile;
        }

        public void DeletePerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
