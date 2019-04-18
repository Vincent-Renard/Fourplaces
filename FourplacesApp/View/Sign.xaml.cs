using FourplacesApp.ViewModel;
using Storm.Mvvm.Forms;

namespace FourplacesApp
{
    public partial class Sign : BaseContentPage
    {
        public Sign()
        {
            InitializeComponent();
            BindingContext = new SignViewModel(Navigation);
        }
    }
}
