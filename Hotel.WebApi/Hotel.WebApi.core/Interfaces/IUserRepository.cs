using Hotel.WebApi.core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.WebApi.core.Interfaces
{
    public interface IUserRepository:IBaseRepository<User>
    {
        public User Login(User user);
        public int Register(User user);
        public User GetByUserName(string userName);
        public User CheckUsername(string userName);
        public int Identify(User user);

    }
}
