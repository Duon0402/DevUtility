using DevUtility.Base;
using DevUtility.Models;
using DevUtility.Models.GenCode;
using DevUtility.Services;
using System.Windows;

namespace DevUtility
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            string cSharpCode = InputCSharpCode.Text;

            var genCodeService = new GenCodeService();
            var result = genCodeService.GetClassInfor(cSharpCode);

            if (result.IsOk())
            {
                var classInfo = result.Data;
                OutputClassInfo.Text = $"Class Name: {classInfo!.ClassName}\n";
                OutputClassInfo.Text += "Properties:\n";
                foreach (var prop in classInfo.Properties!)
                {
                    OutputClassInfo.Text += $"- {prop.Name}: {prop.Type}\n";
                }
            }
            else
            {
                OutputClassInfo.Text = $"Lỗi: {result.Message}";
            }
        }
    }
}
