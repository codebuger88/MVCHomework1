using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<AccountsModels> GetLists()
        {
            List<AccountsModels> lists = new List<AccountsModels>();

            lists = _accountBookRepo.LookupAll().OrderByDescending(o => o.Dateee).ToList()
                    .Select((s, index) => new AccountsModels()
                    {
                        ID = index + 1,
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

        public void Save()
        {
            _accountBookRepo.Commit();
        }
    }
}