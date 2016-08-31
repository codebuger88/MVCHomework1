using MvcPaging;

namespace MyAccounts.Models.ViewModels
{
    public class AccountsViewModels
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Page { get; set; }
        public AccountBook AccountBook { get; set; }
        public IPagedList<AccountsModels> AccountsModels { get; set; }
    }
}