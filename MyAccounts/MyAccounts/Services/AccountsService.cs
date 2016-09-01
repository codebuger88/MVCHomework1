using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyAccounts.Models;
using MyAccounts.Repositories;

namespace MyAccounts.Services
{
    public class AccountsService : Repository<AccountsModels>
    {
        private readonly IRepository<AccountBook> _accountBookRepo;

        public AccountsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _accountBookRepo = new Repository<AccountBook>(unitOfWork);
        }

        public IEnumerable<AccountsModels> GetLists(int? year, int? month)
        {
            var query = _accountBookRepo.LookupAll();

            if (year.HasValue)
            {
                query = query.Where(x => x.Dateee.Year == year.Value);
            }

            if (month.HasValue)
            {
                query = query.Where(x => x.Dateee.Month == month.Value);
            }

            var lists = new List<AccountsModels>();

            lists = query.OrderByDescending(o => o.Dateee).ToList()
                    .Select((s, index) => new AccountsModels()
                    {
                        ID = index + 1,
                        GId = s.Id,
                        AccountTypes = s.Categoryyy.ToString(),
                        AccountDate = s.Dateee,
                        Amount = s.Amounttt,
                        Msg = s.Remarkkk
                    }).ToList();

            return lists;
        }

        public void Add(AccountBook model)
        {
            model.Id = Guid.NewGuid();
            _accountBookRepo.Create(model);
        }

        public void Update(AccountBook model, AccountBook oldData)
        {
            oldData.Amounttt = model.Amounttt;
            oldData.Categoryyy = model.Categoryyy;
            oldData.Dateee = model.Dateee;
            oldData.Remarkkk = model.Remarkkk;
        }

        public void Delete(AccountBook model)
        {
            _accountBookRepo.Remove(model);
        }

        public AccountBook GetSingleOrDefault(Expression<Func<AccountBook, bool>> filter)
        {
            return _accountBookRepo.GetSingle(filter);
        }

        public void Save()
        {
            _accountBookRepo.Commit();
        }
    }
}