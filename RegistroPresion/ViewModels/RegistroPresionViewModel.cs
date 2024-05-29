using RegistroPresion.Models;
using RegistroPresion.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RegistroPresion.ViewModels
{
    public class RegistroPresionViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Registro> Registros { get; set; } = new();

        RegistrosRepository repository = new();
        public Registro? Registro { get; set; }
        public string Error { get; set; } = "";
        public DateTime FechaActual { get; set; }

        public ICommand NavigateCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }

        public RegistroPresionViewModel()
        {
            AgregarCommand = new Command(Agregar);
            NavigateCommand = new Command<string>(Navegar);
            EliminarCommand = new Command(Eliminar);

            foreach (var registro in repository.GetAll())
            {
                Registros.Add(registro);
            }
        }

        private void Eliminar()
        {
            if (Registro != null)
            {
                
                    repository.Delete(Registro);
                    Registros.Remove(Registro);
                    Navegar("MainPage");
                
            }
        }

        private void Navegar(string ruta)
        {
            if(ruta== "Registrar")
            {
                Registro = new();
            }

            Shell.Current.GoToAsync("//" + ruta);
            FechaActual = DateTime.Now;
            Error = "";
            Actualizar();
        }

        private void Agregar()
        {
            Error = "";
            if (Registro != null)
            {
                if (Registro.Sistolica <= 0 || Registro.Diastolica <= 0 || Registro.Pulso <= 0)
                {
                    Error = "Verifique los datos ingresados.";
                }

                if (Error == "")
                {
                    Registro.Fecha =FechaActual;
                    repository.Insert(Registro);
                    Registros.Insert(0, Registro);

                    Navegar("MainPage");//Ir a la lista de registros
                }

                Actualizar(nameof(Error));

            }
        }

        private void Actualizar(string? name = null)
        {
            PropertyChanged?.Invoke(this, new (name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
