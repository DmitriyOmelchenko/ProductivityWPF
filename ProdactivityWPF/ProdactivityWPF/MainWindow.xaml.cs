using Microsoft.Win32;
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
using Microsoft.Win32;
using System.IO;
namespace ProdactivityWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text file|*.txt";
            if (openFileDialog.ShowDialog()==true)
            {
                try
                {
                    Result.Items.Clear();
                    Dictionary<string, int> resultDictionary = new Dictionary<string, int>();
                    //get data from file
                    foreach (var line in File.ReadAllLines(openFileDialog.FileName))
                    {
                        var lineSplitted = line.Split(new[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        if (lineSplitted.Length < 2)
                        {
                            continue;
                        }
                        var lineDay = lineSplitted.First();
                        //prodactivity per day

                        var lineSum = lineSplitted.Skip(1).Select(int.Parse).Sum();
                        resultDictionary.Add(lineDay, lineSum);
                    }
                    
                        var maxProductiveDays = resultDictionary.Where(d => d.Value == resultDictionary.Values.Max()).ToDictionary(d => d.Key, d => d.Value);

                        var leastProductiveDays = resultDictionary.Where(d => d.Value == resultDictionary.Values.Min()).ToDictionary(d => d.Key, d => d.Value);

                        var averageProductivity = resultDictionary.Values.Average();
                        Result.Items.Add(string.Format("Max productive day:" + " ({0}) {1}", maxProductiveDays.Values.First(), string.Join(",", maxProductiveDays.Select(d => d.Key))));
                        Result.Items.Add(string.Format("Least productive day: " + " ({0}) {1}", leastProductiveDays.Values.First(), string.Join(",", leastProductiveDays.Select(d => d.Key))));
                        Result.Items.Add(string.Format("Averag productivity: {0:0.000}", averageProductivity));
                    
                    
                    

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error-" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                

            }
            

        }
    }
}
