using FriendOrganizer.UI.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FriendOrganizer.UI.Wrapper
{
    public class NotifyDataErrorsInfoBase: ViewModelBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errorsDictionary = new Dictionary<string, List<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => _errorsDictionary.Any();

        public IEnumerable GetErrors(string propertyName) => _errorsDictionary.ContainsKey(propertyName) ? _errorsDictionary[propertyName] : null;

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            base.OnPropertyChanged(nameof(HasErrors));
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errorsDictionary.ContainsKey(propertyName))
            {
                _errorsDictionary[propertyName] = new List<string>();
            }

            if (!_errorsDictionary[propertyName].Contains(error))
            {
                _errorsDictionary[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errorsDictionary.ContainsKey(propertyName))
            {
                _errorsDictionary.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
