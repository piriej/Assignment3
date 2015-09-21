using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Prism.Mvvm;
using ShortBus;

namespace Library.Features.CardReader
{
    public class ValidatedBindableBase : BindableBase, INotifyDataErrorInfo
    {
        readonly CardReaderModel _cardReaderModel = new CardReaderModel();
        readonly Dictionary<string, ICollection<string>>
            _validationErrors = new Dictionary<string, ICollection<string>>();

        protected ValidatedBindableBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected ValidatedBindableBase()
        {
        }

        protected ShortBus.IMediator Mediator { get; set; }

        protected void ValidateModelProperty(object value, string propertyName)
        {
            if (_validationErrors.ContainsKey(propertyName))
                _validationErrors.Remove(propertyName);

            var propertyInfo = _cardReaderModel.GetType().GetProperty(propertyName);

            IList<string> validationErrors =
                (from validationAttribute in propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>()
                    where !validationAttribute.IsValid(value)
                    select validationAttribute.FormatErrorMessage(string.Empty))
                    .ToList();

            _validationErrors.Add(propertyName, validationErrors);
            RaiseErrorsChanged(propertyName);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey(propertyName))
                return null;

            return _validationErrors[propertyName];
        }

        public bool HasErrors => _validationErrors.Count > 0;
    }
}