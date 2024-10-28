using DevUtility.Base;
using DevUtility.Models;
using DevUtility.Services;
using System.Threading.Tasks;
using System.Windows;

namespace DevUtility
{
    public partial class MainWindow : Window
    {
        private GenCodeService _genCodeService;

        public MainWindow()
        {
            InitializeComponent();
            _genCodeService = new GenCodeService();
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            string indexInput = InputCSharpCode.Text;
            var indexModel = await _genCodeService.GetClassInfor(indexInput);
            var result = await _genCodeService.GenCodeCSharpToTS(indexModel.Data, null, null);


            if (result.IsOk())
            {
                OutputTypeScriptCode.Text = result.Data!.CodeIndexModel;
            }
            else
            {
                MessageBox.Show(result.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
