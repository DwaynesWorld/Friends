using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FriendOrganizer.UI.Wrapper
{
    public class ModelWrapper<T>: NotifyDataErrorsInfoBase
    {
        public T Model { get; }
        public ModelWrapper(T model) => Model = model;

        protected virtual S GetValue<S>([CallerMemberName]string propertyName = null)
        {
            return (S)typeof(T).GetProperty(propertyName).GetValue(Model);
        }

        protected virtual void SetValue<S>(S value, [CallerMemberName]string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            OnPropertyChanged(propertyName);
            ValidatePropertyInternal(propertyName, value);
        }

        private void ValidatePropertyInternal(string propertyName, object currentValue)
        {
            ClearErrors(propertyName);
            ValidateDataAnnotationErrors(propertyName, currentValue);
            ValidateCustomErrors(propertyName);
        }

        private void ValidateDataAnnotationErrors(string propertyName, object currentValue)
        {
            var valContext = new ValidationContext(Model) { MemberName = propertyName };
            var valResults = new List<ValidationResult>();
            Validator.TryValidateProperty(currentValue, valContext, valResults);

            foreach (var result in valResults)
                AddError(propertyName, result.ErrorMessage);
        }

        private void ValidateCustomErrors(string propertyName)
        {
            var errors = ValidateProperty(propertyName);
            if (errors != null)
            {
                foreach (var error in errors)
                    AddError(propertyName, error);
            }
        }

        protected virtual IEnumerable<string> ValidateProperty(string propertyName)
        {
            return null;
        }
    }
}
