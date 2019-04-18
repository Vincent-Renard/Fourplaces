using FourplacesApp.ViewModel;
using Storm.Mvvm.Forms;

namespace FourplacesApp
{
    public partial class Connection : BaseContentPage
    {
        //[XamlCompilation(XamlCompilationOptions.Compile)]
        public Connection()
        {
            InitializeComponent();
            BindingContext = new ConnectionViewModel(Navigation);
        }
    }
}
