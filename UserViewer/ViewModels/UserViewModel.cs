using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserViewer.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Avatar { get; set; }
        public List<RepoViewModel> Repos { get; set; }
    }
}