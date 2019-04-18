using FourplacesApp.ViewModel;
using Storm.Mvvm.Forms;

namespace FourplacesApp
{
    public partial class UpdatePassword : BaseContentPage
    {
        public UpdatePassword()
        {
            InitializeComponent();
            BindingContext = new UpdatePasswordViewModel(Navigation);
        }
    }
}
