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
        public IBodegaRepositorio Bodega { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            this.Bodega = new BodegaRepositorio(_db);
            
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
