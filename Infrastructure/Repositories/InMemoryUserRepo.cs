using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities;
using Domain.Services.Repositories;
using Microsoft.VisualBasic;

namespace Infrastructure.Repositories
{
    public class InMemoryUserRepo : IUserRepository
    {

        private List<User> users = new List<User>();

        public void Create(User newUser)
        {
            users.Add(newUser);
        }

        public void Delete(int id)
        {
            var x = from user in users where user.Id == id select user;
            users.Remove(x.First());
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User GetOne(int id)
        {
            var x = from user in users where user.Id == id select user;
            return x.First();
        }

        public void Update(int id ,User update)
        {
            users[id] = update;
        }
    }
}
