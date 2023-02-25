using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class BodegaRepositorio : Repositorio<Bodega>, IBodegaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public BodegaRepositorio(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Actualizar(Bodega bodega)
        {
            ///Esta variable traera el dato de la bodega actual que luego queremos modificar
            /*
             * Usaremos FirstOrDefault: Retorna el primer elemento que coincide con la consulta, o el valor predeterminado si no hay ningún elemento
             * que cumpla con la consulta. Si la consulta devuelve varios elementos, solo se devuelve el primero.
             * Y ya que con SingleOrDefault: Si la consulta devuelve varios elementos, se lanza una excepción.
             */
            var bodegaDb = _db.Bodegas.FirstOrDefault(b => b.Id == bodega.Id);
            if (bodegaDb != null)
            {
                bodegaDb.Nombre = bodega.Nombre;
                bodegaDb.Descripcion = bodega.Descripcion;
                bodegaDb.Estado = bodega.Estado;

                ///Vamos usar un método que servirá para guardar en diferentes sitios y no solo con Actualizar
                
            }
        }
    }
}
