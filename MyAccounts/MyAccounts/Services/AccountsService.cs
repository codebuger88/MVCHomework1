using System;
using System.Collections.Generic;
using System.Linq;
using MyAccounts.Models;

namespace MyAccounts.Services
{
    public class AccountsService
    {
        private SkillTreeHomeworkEntities _db;

        public AccountsService()
        {
            _db = new SkillTreeHomeworkEntities();
        }

        public IEnumerable<AccountsModels> GetLists()
        {
            List<AccountsModels> lists = new List<AccountsModels>();

            lists = _db.AccountBook.Take(10).OrderByDescending(o => o.Dateee).ToList()
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
            _db.AccountBook.Add(model);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}