using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        T Obtener(int id);

        IEnumerable<T> ObtenerTodos(
            /*
             * Expression se utilizan para definir filtros y órdenes de selección en las consultas que se realizan a la fuente de datos subyacente
             * se utilizan a menudo en LINQ para construir consultas dinámicas que se pueden compilar y ejecutar en tiempo de ejecución. 
             * Esto permite crear consultas que se ajusten a diferentes criterios de búsqueda o que cambien en función de la entrada del usuario.
             */
            Expression<Func<T, bool>> filter = null,
            /*
             * IQueryable permite consultar colecciones de objetos de manera flexible y eficiente. Se utiliza para construir consultas de LINQ
             * que se pueden traducir en consultas de SQL en una base de datos
             */
            /*
             * IOrderedEnumerable representa una colección de objetos que se han ordenado de acuerdo con un criterio específico.
             */
            Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null,
            string incluirPropiedades = null
            );

        T ObtenerPrimero(
            Expression<Func<T, bool>> filter = null,
            string incluirPropiedades = null
            );

        void Agregar(T entidad);
        void Remover(int id);
        void Remover(T entidad);
        void RemoverRango(IEnumerable<T> entidad);
    }
}
