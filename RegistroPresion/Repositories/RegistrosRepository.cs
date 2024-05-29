using RegistroPresion.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//SQLite es un ORM : Object Relation Mapper
namespace RegistroPresion.Repositories
{
    public class RegistrosRepository
    {
        SQLiteConnection conexion;

        public RegistrosRepository()
        {
            var ruta = FileSystem.AppDataDirectory + "/presion.sqlite";
            conexion = new SQLiteConnection(ruta);
            conexion.CreateTable<Registro>();
        }

        public IEnumerable<Registro> GetAll()
        {
            var datos = conexion.Table<Registro>()
                .OrderByDescending(x => x.Fecha).ToList();

            foreach (var d in datos)
            {
                d.Categoria = ObtenerCategoria(d.Sistolica, d.Diastolica);
            }

            return datos;
        }

        public string ObtenerCategoria(int sistolica, int diastolica)
        {
            if (sistolica < 80 && diastolica < 60)
            {
                return "Hipotensión";
            }
            else if (sistolica >= 80 && sistolica <= 120 && diastolica >= 60 && diastolica <= 80)
            {
                return "Normal";
            }
            else if (sistolica >= 120 && sistolica <= 139 || diastolica >= 80 && diastolica <= 89)
            {
                return "Prehipertensión";
            }
            else if (sistolica >= 140 && sistolica <= 159 || diastolica >= 90 && diastolica <= 99)
            {
                return "Hipertensión grado 1";
            }
            else if (sistolica >= 160 && sistolica <= 180 || diastolica >= 100 && diastolica<=110)
            {
                return "Hipertensión grado 2";
            }
            else if (sistolica > 180 || diastolica > 110)
            {
                return "Crisis hipertensiva";
            }
            else
            {
                return "Valores no categorizados";
            }
        }

        public void Insert(Registro r)
        {
            conexion.Insert(r);
            r.Categoria = ObtenerCategoria(r.Sistolica, r.Diastolica);
        }

        public void Delete(Registro r)
        {
            conexion.Delete(r);
        }

    }
}
