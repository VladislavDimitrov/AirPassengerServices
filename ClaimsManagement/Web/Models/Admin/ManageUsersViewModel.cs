using System.Collections.Generic;

namespace Web.Models.Admin
{
    public class ManageUsersViewModel
    {
        public ManageUsersViewModel(IEnumerable<UserViewModel> users)
        {
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }
        public ManageUsersViewModel()
        {

        }
        public string Input { get; set; }
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
