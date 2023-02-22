using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    /*IDisposable: Libera recursos de memoria no utilizados
     Los recursos no administrados pueden ser cualquier cosa que no sea administrada automáticamente por el recolector de basura de .NET,
     como una conexión de base de datos, un archivo o un socket de red.
     */

    public interface IUnidadTrabajo : IDisposable
    {
        IBodegaRepositorio Bodega { get; }
    }
}
