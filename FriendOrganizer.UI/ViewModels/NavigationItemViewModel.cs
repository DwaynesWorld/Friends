﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModels
{
    public class NavigationItemViewModel: ViewModelBase
    {
        private string _displayMember;

        public int Id { get; set; }

        public string DisplayMember
        {
            get { return _displayMember; }
            set { _displayMember = value; OnPropertyChanged(); }
        }

        public NavigationItemViewModel(int id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
        }
    }
}
