using AutoMapper;
using CardTask.Application.Authorization;
using CardTask.Application.Dtos;
using CardTask.Application.Helpers;
using CardTask.Core.Entities;
using CardTask.DataAccess.Persistence;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Br = BCrypt.Net;

namespace CardTask.Application.Services
{
    public interface IAccountService
    {
        IEnumerable<AccountResponse> GetAll();
        AccountResponse GetById(int id);
        AccountResponse Create(AccountCreateRequest model,string userName);
        AccountResponse Update(int id, AccountUpdateRequest model);
        void Delete(int id);
    }
    public class AccountService:IAccountService
    {
        private readonly CardDbContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AccountService(
            CardDbContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        public IEnumerable<AccountResponse> GetAll()
        {
            var accounts = _context.Accounts;
            return _mapper.Map<IList<AccountResponse>>(accounts);
        }
        private Account GetAccount(int id)
        {
            var account = _context.Accounts.Find(id);
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        public AccountResponse GetById(int id)
        {
            var account = GetAccount(id);
            return _mapper.Map<AccountResponse>(account);
        }

        public AccountResponse Create(AccountCreateRequest model,string userName)
        {
            var user=_context.Users.FirstOrDefault(x => x.UserName == userName);
            // validate

            var account = _mapper.Map<Account>(model);
            account.User = user;

           
            // save account
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return _mapper.Map<AccountResponse>(account);
        }

        public AccountResponse Update(int id, AccountUpdateRequest model)
        {
            var account = GetAccount(id);

            _mapper.Map(model, account);
            _context.Accounts.Update(account);
            _context.SaveChanges();

            return _mapper.Map<AccountResponse>(account);
        }

        public void Delete(int id)
        {
            var account = GetAccount(id);
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }

    }
}
