using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities;

namespace Domain.Services.Repositories
{
    public interface IUserRepository
    {
        public User GetOne(int id);
        public List<User> GetAll();
        public void Update(int id, User update);
        public void Delete(int id);
        public void Create(User newUser);

    }
}
