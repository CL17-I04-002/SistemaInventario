using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IBodegaRepositorio : IRepositorio<Bodega>
    {
        ///Aca aplicaremos el método update ya que se debe realizar de forma individual 
        /// de acuerdo a las propiedades de cada tabla
        void Actualizar(Bodega bodega);
    }
}
