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
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public ProductoRepositorio(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Actualizar(Producto producto)
        {
            var productoDb = _db.Productos.FirstOrDefault(p => p.Id== producto.Id);
            ///Existe un registro en la BD
            if(productoDb != null)
            {
                ///Si la imagen que estamos mandando es diferente de null
                if(producto.ImagenUrl != null)
                {
                    productoDb.ImagenUrl = producto.ImagenUrl;
                }
                ///Si el padre es igual a cero
                if(producto.PadreId == 0)
                {
                    productoDb.PadreId = null;
                }
                ///En el caso que padreId sea diferente de cero
                else
                {
                    productoDb.PadreId = producto.PadreId;
                }
                productoDb.NumeroSerie = producto.NumeroSerie;
                productoDb.Descripcion = producto.Descripcion;
                productoDb.Precio = producto.Precio;
                productoDb.Costo = producto.Costo;
                productoDb.CategoriaId = producto.CategoriaId;
                productoDb.MarcaId = producto.MarcaId;
            }
        }
    }
}
