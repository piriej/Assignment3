using System.Reflection;
using System.Windows;
using Autofac;

namespace Library
{
    public class AutofacDataBindingProvider : DependencyObject
    {
        // Define some custom Dependency Properties to facilitate the binding.
        public static readonly DependencyProperty AutofacContainerProperty =
            DependencyProperty.Register("AutofacContainer",
                typeof(IContainer),
                typeof(AutofacDataBindingProvider),
                new PropertyMetadata(null, AutofacValuesChanged));

        public IContainer AutofacContainer
        {
            get { return (IContainer)GetValue(AutofacContainerProperty); }
            set { SetValue(AutofacContainerProperty, value); }
        }

        public static readonly DependencyProperty DataTypeNameProperty =
            DependencyProperty.Register("DataTypeName",
            typeof(string),
            typeof(AutofacDataBindingProvider),
            new PropertyMetadata(null, AutofacValuesChanged));

        public string DataTypeName
        {
            get { return (string)GetValue(DataTypeNameProperty); }
            set { SetValue(DataTypeNameProperty, value); }
        }

        private static readonly DependencyPropertyKey DataPropertyKey =
            DependencyProperty.RegisterReadOnly("Data",
            typeof(object),
            typeof(AutofacDataBindingProvider),
            new PropertyMetadata(null));

        public static readonly DependencyProperty DataProperty =
            DataPropertyKey.DependencyProperty;

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            private set { SetValue(DataProperty, value); }
        }

        private static void AutofacValuesChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var container = d.GetValue(AutofacContainerProperty) as IContainer;
            var typeName = d.GetValue(DataTypeNameProperty) as string;
            if (container == null || string.IsNullOrEmpty(typeName))
                return;
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetType(typeName, false, true);
            if (type == null)
                return;
            var data = container.Resolve(type);
            d.SetValue(DataPropertyKey, data);
        }
    }
}
