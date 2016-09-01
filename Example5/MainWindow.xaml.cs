﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Example5
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<Button> _buttons = new List<Button>();
        private Button _empty;
        private const int SIZE = 4;
        private const int SHUFFLE_DEPTH = 1000;

        public MainWindow()
        {
            InitializeComponent();

            int l = SIZE*SIZE - 1;
            for (int i = 0; i < l; ++i)
            {
                var b = new Button {Content = (i+1).ToString(), FontSize = 20};
                b.Click += ButtonClick;
                _buttons.Add(b);
            }
            _empty = new Button {Content = "", IsEnabled = false};
            _buttons.Add(_empty);

            FillGrid();
        }

        private void FillGrid()
        {
            Pane.Children.Clear();
            foreach (var b in _buttons)
                Pane.Children.Add(b);
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            int i = _buttons.IndexOf((Button) sender);
            int j = _buttons.IndexOf(_empty);
            int xi = i%SIZE, yi = i/SIZE;
            int xj = j%SIZE, yj = j/SIZE;

            if (Math.Abs(xi - xj) + Math.Abs(yi - yj) == 1)
            {
                _buttons[j] = _buttons[i];
                _buttons[i] = _empty;
            }
            FillGrid();
        }

        private void ShufflePane(object sender, RoutedEventArgs e)
        {
            var rnd = new Random();
            for (int i = 0; i < SHUFFLE_DEPTH; ++i)
            {
                int j = rnd.Next(_buttons.Count);
                ButtonClick(_buttons[j], null);
            }
        }
    }
}
