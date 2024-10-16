using System;
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

namespace Iskhakova_Avtoservise
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            //добавляем строки
            //загрузить в список из бд
            var currentServies = iskhakova_avtoserviceEntities.GetContext().Service.ToList();
            //связать с нашим листвью
            ServiceListView.ItemsSource=currentServies;
            //добавили строки

            ComboType.SelectedIndex=0;
            UpdateServices();
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void UpdateServices()
        {
            var currentServices = iskhakova_avtoserviceEntities.GetContext().Service.ToList();

            if (ComboType.SelectedIndex == 0)
            {
                currentServices = currentServices.Where(p => (Convert.ToInt32(p.Discount) >= 0 && Convert.ToInt32(p.Discount) <= 100)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentServices = currentServices.Where(p => (Convert.ToInt32(p.Discount) >= 0 && Convert.ToInt32(p.Discount) < 5)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentServices = currentServices.Where(p => (Convert.ToInt32(p.Discount) >= 5 && Convert.ToInt32(p.Discount) < 15)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentServices = currentServices.Where(p => (Convert.ToInt32(p.Discount) >= 15 && Convert.ToInt32(p.Discount) < 30)).ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {
                currentServices = currentServices.Where(p => (Convert.ToInt32(p.Discount) >= 30 && Convert.ToInt32(p.Discount) < 70)).ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {
                currentServices = currentServices.Where(p => (Convert.ToInt32(p.Discount) >= 70 && Convert.ToInt32(p.Discount) < 100)).ToList();
            }
        ///fsdfdsfdsfdsfsd

            currentServices = currentServices.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            ServiceListView.ItemsSource = currentServices.ToList();

            if (RButtonDown.IsChecked.Value)
            {
                ServiceListView.ItemsSource=currentServices.OrderByDescending(p => p.Cost).ToList();
            }

            if (RButtonUP.IsChecked.Value)
            {
                ServiceListView.ItemsSource = currentServices.OrderBy(p => p.Cost).ToList();
            }
        }
    }
}
