using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        /*
         * En este caso usamos la interfaz Al definir una propiedad de tipo concreto BodegaRepositorio en lugar de una interfaz IBodegaRepositorio,
         * estás acoplándote directamente a esa implementación concreta, lo que hace que tu código sea menos flexible y más difícil de mantener.
         * Si en el futuro decides cambiar la implementación de BodegaRepositorio, tendrías que cambiar el código en muchos lugares. Pero si usas la interfaz
         * IBodegaRepositorio en su lugar, puedes cambiar la implementación sin cambiar el código que la utiliza, ya que las interfaces definen solo el contrato
         * que debe cumplir la implementación, no los detalles de la implementación en sí.
         */
        public IBodegaRepositorio Bodega { get; private set; }
        public ICategoriaRepsoitorio Categoria { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            this.Bodega = new BodegaRepositorio(_db);
            this.Categoria = new CategoriaRepositorio(_db);
            
        }
        ///Aqui vamos aplicar el guardado persistente en la BD
        public void Guardar()
        {
            _db.SaveChanges();
        }

        /*
         * Al implementar la interfaz IDisposable, la clase debe implementar un método llamado Dispose()
         * que libera los recursos no administrados utilizados por la clase.
         * Esto se aplico en la Interfaz IUnidadTrabajo
         */
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
