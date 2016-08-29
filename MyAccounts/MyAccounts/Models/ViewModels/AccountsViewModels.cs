using MvcPaging;

namespace MyAccounts.Models.ViewModels
{
    public class AccountsViewModels
    {
        public AccountBook AccountBook { get; set; }
        public IPagedList<AccountsModels> AccountsModels { get; set; }
    }
}