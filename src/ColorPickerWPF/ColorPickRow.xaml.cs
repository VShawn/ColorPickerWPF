﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using ColorPickerWPF.Code;
using UserControl = System.Windows.Controls.UserControl;

namespace ColorPickerWPF
{
    /// <summary>
    /// Interaction logic for ColorPickRow.xaml
    /// </summary>
    public partial class ColorPickRow : UserControl, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public event EventHandler OnPick;


        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(ColorPickRow),
          new PropertyMetadata(Colors.White, OnDataChanged));

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (Color)e.NewValue;
            ((ColorPickRow)d).ColorDisplayGrid.Background = new SolidColorBrush(value);
            ((ColorPickRow)d).HexLabel.Text = value.ToHexString();
            ((ColorPickRow)d).OnPropertyChanged(nameof(Color));
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set
            {
                SetValue(ColorProperty, value);
                OnPropertyChanged(nameof(Color));
            }
        }

        public ColorPickRow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void PickColorButton_OnClick(object sender, RoutedEventArgs e)
        {
            Color color;
            if (ColorPickerWindow.ShowDialog(out color))
            {
                SetColor(color);
                OnPick?.Invoke(this, EventArgs.Empty);
            }
        }

        public void SetColor(Color color)
        {
            Color = color;
            HexLabel.Text = color.ToHexString();
            ColorDisplayGrid.Background = new SolidColorBrush(color);
        }
    }
}
