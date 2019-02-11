using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserViewer.ViewModels
{
    public class RepoViewModel
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int StargazerCount { get; set; }
    }
}