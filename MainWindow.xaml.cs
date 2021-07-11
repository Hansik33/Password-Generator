using System;
using System.Text;
using System.Windows;

namespace Password_Generator
{
    public partial class MainWindow : Window
    {
        const string small_letters = "qwertyuiopasdfghjklzxcvbnm";
        const string big_letters = "QWERTYUIOPASDFGHJKLZXCVBNM";
        const string numbers = "1234567890";
        const string special_symbols = "!@#$%^&*()";

        string Password = String.Empty;

        StringBuilder stringBuilder = new StringBuilder();
        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void DoesContainSmallLetters()
        {
            var does_contain = false;

            for (int i = 0; i < small_letters.Length; i++) if (Password.Contains(small_letters[i])) does_contain = true;
            if (!does_contain) Generate();
        }

        public void DoesContainBigLetters()
        {
            var does_contain = false;

            for (int i = 0; i < big_letters.Length; i++) if (Password.Contains(big_letters[i])) does_contain = true;
            if (!does_contain) Generate();
        }

        public void DoesContainNumbers()
        {
            var does_contain = false;

            for (int i = 0; i < numbers.Length; i++) if (Password.Contains(numbers[i])) does_contain = true;
            if (!does_contain) Generate();
        }

        public void DoesContainSpecialSymbols()
        {
            var does_contain = false;

            for (int i = 0; i < special_symbols.Length; i++) if (Password.Contains(special_symbols[i])) does_contain = true;
            if (!does_contain) Generate();
        }

        public void StrongPassword()
        {
            ProgressBarPasswordStrength.Foreground = System.Windows.Media.Brushes.LightGreen;
            ProgressBarPasswordStrength.Value = 100;
        }

        public void GoodPassword()
        {
            ProgressBarPasswordStrength.Foreground = System.Windows.Media.Brushes.YellowGreen;
            ProgressBarPasswordStrength.Value = 75;
        }

        public void NormalPassword()
        {
            ProgressBarPasswordStrength.Foreground = System.Windows.Media.Brushes.Yellow;
            ProgressBarPasswordStrength.Value = 50;
        }

        public void WeakPassword()
        {
            ProgressBarPasswordStrength.Foreground = System.Windows.Media.Brushes.Crimson;
            ProgressBarPasswordStrength.Value = 25;
        }

        public void CheckPasswordStrength()
        {
            var Password_strength = 0;

            if (CheckBoxBigLetters.IsChecked.Value) Password_strength++;
            if (CheckBoxSmallLetters.IsChecked.Value) Password_strength++;
            if (CheckBoxNumbers.IsChecked.Value) Password_strength++;
            if (CheckBoxSpecialSymbols.IsChecked.Value) Password_strength++;
            Password_strength += (int)SliderLengthPassword.Value;

            if (Password_strength >= 20) StrongPassword();
            else if (Password_strength >= 16) GoodPassword();
            else if (Password_strength >= 12) NormalPassword();
            else WeakPassword();
        }

        public void Generate()
        {
            stringBuilder.Length = 0;

            var length = SliderLengthPassword.Value;
            var valid = String.Empty;

            if (CheckBoxBigLetters.IsChecked.Value) valid += big_letters;
            if (CheckBoxSmallLetters.IsChecked.Value) valid += small_letters;
            if (CheckBoxNumbers.IsChecked.Value) valid += numbers;
            if (CheckBoxSpecialSymbols.IsChecked.Value) valid += special_symbols;

            while (0 < length--) stringBuilder.Append(valid[random.Next(valid.Length)]);
            Password = stringBuilder.ToString();

            if (CheckBoxBigLetters.IsChecked.Value) DoesContainBigLetters();
            if (CheckBoxSmallLetters.IsChecked.Value) DoesContainSmallLetters();
            if (CheckBoxNumbers.IsChecked.Value) DoesContainNumbers();
            if (CheckBoxSpecialSymbols.IsChecked.Value) DoesContainSpecialSymbols();

            TextBoxPassword.Text = Password;
            CheckPasswordStrength();
        }

        public void CheckCorrectness()
        {
            if
            (
              !(CheckBoxBigLetters.IsChecked.Value)
              && !(CheckBoxSmallLetters.IsChecked.Value)
              && !(CheckBoxNumbers.IsChecked.Value)
              && !(CheckBoxSpecialSymbols.IsChecked.Value)
            ) MessageBox.Show("Trzeba zaznaczyć chociaż jedną opcję!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            else Generate();
        }

        public void CopyPassword()
        {
            var text = TextBoxPassword.Text;
            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Text, (Object)text);
            MessageBox.Show("Zrobione!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonGenerate_Click(object sender, RoutedEventArgs e)
        {
            CheckCorrectness();
        }

        private void ButtonCopyPassword_Click(object sender, RoutedEventArgs e)
        {
            CopyPassword();
        }
    }
}
