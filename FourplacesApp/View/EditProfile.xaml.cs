using FourplacesApp.ViewModel;
using Storm.Mvvm.Forms;

namespace FourplacesApp
{
    public partial class EditProfile : BaseContentPage
    {
        public EditProfile()
        {
            InitializeComponent();
            BindingContext = new EditProfileViewModel(Navigation);
        }
    }
}
