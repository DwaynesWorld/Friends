﻿using FriendOrganizer.Model;
using FriendOrganizer.UI.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        public INavigationViewModel NavigationViewModel { get; }
        public IFriendDetailViewModel FriendDetailViewModel { get; }

        public MainViewModel(
            INavigationViewModel navigationViewModel,
            IFriendDetailViewModel friendDetailViewModel
            )
        {
            NavigationViewModel = navigationViewModel;
            FriendDetailViewModel = friendDetailViewModel;
        }

        public async Task LoadAsync() => await NavigationViewModel.LoadAsync();
    }
}
