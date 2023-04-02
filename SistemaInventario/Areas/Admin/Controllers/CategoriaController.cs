using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CategoriaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            ///Usaremos DataTable
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Categoria categoria = new Categoria();
            ///Si es null el id entonces estamos agregando una nueva bodega
            if (id == null)
            {
                ///Esto es para Crear nuevo Registro
                return View(categoria);
            }
            else
            {
                ///Esto es para Actualizar
                categoria = _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
                if(categoria == null)
                {
                    return NotFound();
                }
                return View(categoria);
            }
        }
        ///Guardar
        [HttpPost]
        ///Esta anotación nos sirve para evitar falsificaciones de solicitudes de un sitio cargado 
        ///de otra pagina que puede intentar datos cargar en nuestra página
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                ///Guardar
                if(categoria.Id == 0)
                {
                    _unidadTrabajo.Categoria.Agregar(categoria);

                }
                ///Actualizar
                else
                {
                    _unidadTrabajo.Categoria.Actualizar(categoria);
                }
                ///Este método se implemento en la interfaz IUnidadTrabajo para poder guardar tanto Agregar y Actualizar de manera persistente
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }


        ///Crearemos una region
        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var todos = _unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var categoriaDb = _unidadTrabajo.Categoria.Obtener(id);
            if(categoriaDb == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            _unidadTrabajo.Categoria.Remover(categoriaDb);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categoria Exitosamente" });
        }
        #endregion
    }
}