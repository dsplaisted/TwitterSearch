﻿using System;
using System.Windows.Input;

namespace MvvmPortable.Presentation.Navigation
{
    // Represents a ViewModel that is used in navigation-based applications
    public abstract partial class NavigatableViewModel : Bindable
    {
        private readonly INavigationService _navigationService;
        private ICommand _goBackCommand;
        private ICommand _goHomeCommand;

        protected NavigatableViewModel(INavigationService navigationService)
        {
            Requires.NotNull(navigationService, "navigationService");

            _navigationService = navigationService;
        }

        public virtual void Navigated(string navigationParameter)
        {

        }

        public ICommand GoBackCommand
        {
            get { return _goBackCommand ?? (_goBackCommand = new NavigationCommand(GoBack, _navigationService)); }
        }

        public ICommand GoHomeCommand
        {
            get { return _goHomeCommand ?? (_goHomeCommand = new NavigationCommand(GoHome, _navigationService)); }
        }

        protected void GoBack()
        {
            _navigationService.GoBack();
        }

        protected void GoHome()
        {
            while (_navigationService.CanGoBack)
            {
                _navigationService.GoBack();
            }
        }
    }
}
