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
    public class MarcaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public MarcaController(IUnidadTrabajo unidadTrabajo)
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
            Marca marca = new Marca();
            ///Si es null el id entonces estamos agregando una nueva bodega
            if (id == null)
            {
                ///Esto es para Crear nuevo Registro
                return View(marca);
            }
            else
            {
                ///Esto es para Actualizar
                marca = _unidadTrabajo.Marca.Obtener(id.GetValueOrDefault());
                if(marca == null)
                {
                    return NotFound();
                }
                return View(marca);
            }
        }
        ///Guardar
        [HttpPost]
        ///Esta anotación nos sirve para evitar falsificaciones de solicitudes de un sitio cargado 
        ///de otra pagina que puede intentar datos cargar en nuestra página
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Marca marca)
        {
            if (ModelState.IsValid)
            {
                ///Guardar
                if(marca.Id == 0)
                {
                    _unidadTrabajo.Marca.Agregar(marca);

                }
                ///Actualizar
                else
                {
                    _unidadTrabajo.Marca.Actualizar(marca);
                }
                ///Este método se implemento en la interfaz IUnidadTrabajo para poder guardar tanto Agregar y Actualizar de manera persistente
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }


        ///Crearemos una region
        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var todos = _unidadTrabajo.Marca.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var marcaDb = _unidadTrabajo.Marca.Obtener(id);
            if(marcaDb == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            _unidadTrabajo.Marca.Remover(marcaDb);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categoria Exitosamente" });
        }
        #endregion
    }
}