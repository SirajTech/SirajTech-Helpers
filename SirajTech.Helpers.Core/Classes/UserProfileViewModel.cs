using System;
using System.Collections.Generic;

namespace SirajTech.Helpers.Core
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        private string displayName;

        public string DisplayName
        {
            get { return displayName ?? UserName; }
            set { displayName = value; }
        }

        public IEnumerable<string> Roles { get; set; }
    }
}