using Hotel.WebApi.core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.WebApi.core.Interfaces
{
    public interface IUserService:IBaseService<User>
    {
        public void Login(User user);
        public bool RegisterAsync(User user);
        public bool Identify(User user);

    }
}
