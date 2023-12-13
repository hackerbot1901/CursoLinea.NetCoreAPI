using CursoLineaAPI.DAO;
using CursoLineaAPI.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace CursoLineaAPI.Controllers
{
    [ApiController]
    [Route("profesor")]
    
    public class ProfesorController : Controller
    {
        [HttpGet("obtenerProfesor")]
        public dynamic ObtenerProfesor([FromQuery] int idProfesor)
        {
            try
            {
                var profesor = DAOFactory.GetFactory().GetEFProfesorDAO().Read(idProfesor);

                if (profesor == null)
                {
                    return NotFound(new { message = "Profesor no encontrado" });
                }

                var cursoSerializado = new { profesor, message = "Profesor obtenido exitosamente" };

                return Ok(cursoSerializado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Se produjo un error al obtener el curso", error = ex.Message });
            }
        }

        [HttpGet("listado")]
        public dynamic ListarProfesores()
        {
            CursoLineaContext contexto = new CursoLineaContext();
            var profesores = contexto.Profesors;

            return Ok(new
            {
                profesores,
                message = "Listado de profesores"
            });
        }
    }
}
