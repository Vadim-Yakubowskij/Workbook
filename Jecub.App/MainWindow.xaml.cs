﻿using DataBase.Repository;
using JecubNode.Repository;
using node;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jecub.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static AddWin AddWindow;
        public static UpdateWin UpdateWindow;
        TaskRepository taskRepository = new TaskRepository();
        TododoViewModel vm;
        public MainWindow(int userId)
        {
            InitializeComponent();
            vm = new TododoViewModel(taskRepository,userId);
            DataContext = vm;
        }

        private void MenuPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddWindow == null)
            {
                AddWindow = new AddWin(vm);
                AddWindow.Show();
            }
            else
            {
                AddWindow.Activate();
            }
        }

        public void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateWindow = new UpdateWin(vm);
            UpdateWindow.Show();
        }

        private void MoveRight_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MoveLeft_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuPanel3_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

    }
}