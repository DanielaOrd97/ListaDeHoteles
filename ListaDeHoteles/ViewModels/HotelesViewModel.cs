using ListaDeHoteles.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json;
using ListaDeHoteles.Views;

namespace ListaDeHoteles.ViewModels
{
    public class HotelesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Hotel> ListaHoteles { get; set; } = new ObservableCollection<Hotel>();

        public Hotel Hotel { get; set; }
        public string Error { get; set; }

        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand MostrarDetallesCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }

        public HotelesViewModel()
        {
            Deserializar();
            CambiarVistaCommand = new Command<string>(CambiarVista);
            AgregarCommand = new Command(Agregar);
            MostrarDetallesCommand = new Command<Hotel>(MostrarDetalles);
            EditarCommand = new Command<Hotel>(Editar);
            EliminarCommand = new Command<Hotel>(Eliminar);
            GuardarCommand = new Command(Guardar);
        }

        private void Eliminar(Hotel h)
        {
            if(h!= null)
            {
                ListaHoteles.Remove(h);
                Serializar();
            }
        }

        int indice;

        private void Editar(Hotel h)
        {


             indice = ListaHoteles.IndexOf(h);

            Hotel = new Hotel
            {
                Nombre = h.Nombre,
                Ubicacion = h.Ubicacion,
                Estrellas = h.Estrellas,
                Pisos = h.Pisos,
                NumeroHabitaciones = h.NumeroHabitaciones,
                Imagen = h.Imagen,
                Descripcion = h.Descripcion,
            };

            vistaEditar = new EditarHotelView()
            {
                BindingContext = this
            };

            App.Current.MainPage.Navigation.PushAsync(vistaEditar);

            }



        


        private void Guardar()
        {
            if(Hotel != null)
            {

            

            Error = "";

            if (string.IsNullOrWhiteSpace(Hotel.Nombre))
            {
                Error = "Escriba el nombre del hotel.";
            }
            if (string.IsNullOrWhiteSpace(Hotel.Ubicacion))
            {
                Error = "Escriba la ubicación del hotel.";
            }
            if (string.IsNullOrWhiteSpace(Hotel.Imagen))
            {
                Error = "Coloque la URL del hotel correspondiente.";
            }
            if (Hotel.Estrellas == 0 )
            {
                Error = "Escriba la cantidad de estrellas.";
            }
            if (Hotel.Estrellas > 5)
            {
                Error = "La cantidad de estrellas debe ser entre 1 y 5.";
            }
           

            if (!Uri.TryCreate(Hotel.Imagen, UriKind.Absolute, out var uri))
            {
                Error = "Escribe una URL válida.";

            }

            if (string.IsNullOrWhiteSpace(Error))
            {
                ListaHoteles[indice] = Hotel;
                Serializar();
                App.Current.MainPage.Navigation.PopToRootAsync();
            }

                Change();
          
            }

        }

        private void MostrarDetalles(Hotel h)
        {
            vistaDetalles = new DetallesHotelView()
            {
                BindingContext = h
            };

            App.Current.MainPage.Navigation.PushAsync(vistaDetalles);
        }

        //Pages

        AgregarHotelesView vistaHotel;
        DetallesHotelView vistaDetalles;
        EditarHotelView vistaEditar;
        

        private void Agregar()
        {
            if(Hotel != null)
            {

                Error = "";

                if (string.IsNullOrWhiteSpace(Hotel.Nombre))
                {
                    Error = "Escriba el nombre del hotel.";
                }
                if (string.IsNullOrWhiteSpace(Hotel.Ubicacion))
                {
                    Error = "Escriba la ubicación del hotel.";
                }
                if (string.IsNullOrWhiteSpace(Hotel.Imagen))
                {
                    Error = "Coloque la URL del hotel correspondiente.";
                }
                if(Hotel.Estrellas == 0)
                {
                    Error = "Escriba la cantidad de estrellas.";
                }
                if(Hotel.Estrellas > 5)
                {
                    Error = "La cantidad de estrellas debe ser entre 1 y 5.";
                }


                if(!Uri.TryCreate(Hotel.Imagen, UriKind.Absolute, out var uri))
                {
                    Error = "Escribe una URL válida.";

                }

                //Agregar
                if (string.IsNullOrWhiteSpace(Error))
                {
                    ListaHoteles.Add(Hotel);
                    CambiarVista("Ver");
                    Serializar();
                    
                }

                Change();
            }
        }

        private void Change()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        private void CambiarVista(string vista)
        {
            if(vista == "Agregar")
            {
                Hotel = new Hotel();
                vistaHotel = new AgregarHotelesView() { BindingContext = this };  
                Application.Current.MainPage.Navigation.PushAsync(vistaHotel);

            }
            else if(vista == "Ver")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        void Serializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal)+"lista.json";
            File.WriteAllText(file,JsonConvert.SerializeObject(ListaHoteles));
        }


        void Deserializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "lista.json";
            if (File.Exists(file))
            {
                ListaHoteles = JsonConvert.DeserializeObject<ObservableCollection<Hotel>>(File.ReadAllText(file));

            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
