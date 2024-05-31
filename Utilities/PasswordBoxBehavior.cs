using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FanaticMotors.Utilities
{
    public static class PasswordBoxBehavior
    {
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxBehavior), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static string GetBoundPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BoundPasswordProperty);
        }

        public static void SetBoundPassword(DependencyObject obj, string value)
        {
            obj.SetValue(BoundPasswordProperty, value);
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                if (!(bool)passwordBox.GetValue(UpdatingPasswordProperty))
                {
                    passwordBox.Password = (string)e.NewValue;
                }
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        private static readonly DependencyProperty UpdatingPasswordProperty =
            DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool), typeof(PasswordBoxBehavior), new PropertyMetadata(false));

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                passwordBox.SetValue(UpdatingPasswordProperty, true);
                SetBoundPassword(passwordBox, passwordBox.Password);
                passwordBox.SetValue(UpdatingPasswordProperty, false);
            }
        }

        // Placeholder Dependency Property
        public static readonly DependencyProperty Placeholder =
            DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(PasswordBoxBehavior), new PropertyMetadata(string.Empty));

        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(Placeholder);
        }

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(Placeholder, value);
        }
    }
}
