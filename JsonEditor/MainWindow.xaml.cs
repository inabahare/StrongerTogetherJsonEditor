using JsonEditor.Views;

namespace JsonEditor
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var editor = new Editor();
            Content.Content = (editor);
        }
    }
}
