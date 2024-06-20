using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public interface IUserService
    {
        public void SendMessage(User user, string message);
        public void CoupleToFirstAvailable(Item item, User user);
        public void CoupleToFirstAvailable(ActivityItem activity, User user);
    }
}
