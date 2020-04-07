using System;
using System.Windows;
using System.Windows.Media;
using ColorPickerWPF.Code;
using ColorPickerWPF.Properties;

namespace ColorPickerWPF
{
    /// <summary>
    /// Interaction logic for ColorPickerWindow.xaml
    /// </summary>
    public partial class ColorPickerWindow : Window
    {
        protected readonly int WidthMax = 574;
        protected readonly int WidthMin = 342;
        
        public ColorPickerWindow()
        {
            InitializeComponent();
        }
        
        public static bool ShowDialog(out Color color, ColorPickerControl.ColorPickerChangeHandler customPreviewEventHandler = null)
        {
            var instance = new ColorPickerWindow();
            color = instance.ColorPicker.Color;

            instance.Width = instance.WidthMax;

            if (customPreviewEventHandler != null)
            {
                instance.ColorPicker.OnPickColor += customPreviewEventHandler;
            }
            
            var result = instance.ShowDialog();
            if (result.HasValue && result.Value)
            {
                color = instance.ColorPicker.Color;
                return true;
            }

            return false;
        }
        
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Hide();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Hide();
        }
    }
}
