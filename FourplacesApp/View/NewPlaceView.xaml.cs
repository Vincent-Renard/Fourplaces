using FourplacesApp.ViewModel;
using Storm.Mvvm.Forms;

namespace FourplacesApp
{
    public partial class NewPlaceView : BaseContentPage
    {
        public NewPlaceView()
        {
            InitializeComponent();
            BindingContext = new NewPlaceViewViewModel(Navigation);
        }


    }
}
