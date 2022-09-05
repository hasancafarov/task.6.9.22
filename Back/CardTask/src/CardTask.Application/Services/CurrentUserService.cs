using CardTask.Core.Entities;
using CardTask.DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.Application.Services
{
    public interface ICurrentUserService
    {
        public User getCurrentUser(string userName);
    }
    public class CurrentUserService:ICurrentUserService
    {
        private readonly CardDbContext _context;
        public CurrentUserService(CardDbContext context)
        {
            _context = context;
        }

        public User getCurrentUser(string userName)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
