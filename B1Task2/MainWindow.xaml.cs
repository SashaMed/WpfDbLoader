using B1Task2.Models;
using B1Task2.Repository;
using B1Task2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace B1Task2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// D:\education\.net\B1Task2\ОСВ_для_тренинга.xls

    public partial class MainWindow : Window
    {
        ObservableCollection<FileTableData> files = new ObservableCollection<FileTableData>();
        DataTransferService _dataTransferService;
        ApplicationDbContext _dpContext;


        public MainWindow()
        {
            InitializeComponent();
            _dpContext = new ApplicationDbContext();
            _dataTransferService = new DataTransferService(_dpContext);
            FilesDataGrid.ItemsSource = null;
            FilesDataGrid.ItemsSource = files;
            //_dataTransferService.DeleteAll();
            AddDataToFilesDataGrid();
        }

        private void AddDataToFilesDataGrid()
        {
            _dataTransferService.GetFilesData().ForEach(file => { files.Add(file); });
        }

        private void ImportButtonClick(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text; 
            if (!string.IsNullOrEmpty(filePath))
            {
                var result = _dataTransferService.ImportFromSheetToDb(filePath);
                if (result != null)
                {
                    files.Add(result.fileData);
                    ExcelDataDataGrid.ItemsSource = result.finDatas;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите путь к файлу.");
            }
        }

        private void FilesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid table = (DataGrid)e.OriginalSource;
            var currentItem = (FileTableData)table.CurrentItem;
            var currentItemId = currentItem.FileId;
            var finData = _dataTransferService.GetFinancialDatas(currentItemId);
            ExcelDataDataGrid.ItemsSource = finData;
        }
    }
}
