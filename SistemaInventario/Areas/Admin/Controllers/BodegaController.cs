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
    public class BodegaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public BodegaController(IUnidadTrabajo unidadTrabajo)
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
            Bodega bodega = new Bodega();
            ///Si es null el id entonces estamos agregando una nueva bodega
            if (id == null)
            {
                ///Esto es para Crear nuevo Registro
                return View(bodega);
            }
            else
            {
                ///Esto es para Actualizar
                bodega = _unidadTrabajo.Bodega.Obtener(id.GetValueOrDefault());
                if(bodega == null)
                {
                    return NotFound();
                }
                return View(bodega);
            }
        }
        ///Guardar
        [HttpPost]
        ///Esta anotación nos sirve para evitar falsificaciones de solicitudes de un sitio cargado 
        ///de otra pagina que puede intentar datos cargar en nuestra página
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                ///Guardar
                if(bodega.Id == 0)
                {
                    _unidadTrabajo.Bodega.Agregar(bodega);

                }
                ///Actualizar
                else
                {
                    _unidadTrabajo.Bodega.Actualizar(bodega);
                }
                ///Este método se implemento en la interfaz IUnidadTrabajo para poder guardar tanto Agregar y Actualizar de manera persistente
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(bodega);
        }


        ///Crearemos una region
        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var todos = _unidadTrabajo.Bodega.ObtenerTodos();
            return Json(new { data = todos });
        }
        #endregion
    }
}