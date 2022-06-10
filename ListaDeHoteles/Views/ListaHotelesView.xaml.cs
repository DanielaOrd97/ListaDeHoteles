using ListaDeHoteles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListaDeHoteles.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaHotelesView : ContentPage
    {
        public ListaHotelesView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarHotelesView());
        }

        private async void SwipeItem_Clicked(object sender, EventArgs e)
        {

            var sw = (SwipeItem)sender;

            if (await DisplayAlert("Confirme", $"¿Esta seguro de eliminar a {((Hotel)sw.CommandParameter).Nombre}?","Si","No")==true)
            {
                

                HotelVM.EliminarCommand.Execute(sw.CommandParameter);
            }
        }
    }
}