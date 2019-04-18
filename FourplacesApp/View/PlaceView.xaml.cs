using FourplacesApp.ViewModel;
using Storm.Mvvm.Forms;

namespace FourplacesApp
{
    public partial class PlaceView : BaseContentPage
    {
        public int Id_sel;
        public PlaceView(int id_selected_place)
        {

            InitializeComponent();
            Id_sel = id_selected_place;
            BindingContext = new PlaceViewViewModel(Id_sel ,Navigation);
        }


    }

}
