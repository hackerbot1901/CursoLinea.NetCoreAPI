using CursoLineaAPI.DAO;
using CursoLineaAPI.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoLineaAPI.Controllers
{
    [ApiController]
    [Route("estudiante")]
    public class EstudianteController : ControllerBase
    {
        [HttpPost("inscribir")]
        public dynamic InscribirEstudiante([FromQuery]int idCurso, Estudiante estudiante)
        {
            try
            {
                var cursoSeleccionado = DAOFactory.GetFactory().GetCursoDAO().Read(idCurso);
                if (cursoSeleccionado == null)
                {
                    return NotFound("Curso inexistente");
                }
                estudiante.CursoId = idCurso;
                DAOFactory.GetFactory().GetEstudianteDAO().Create(estudiante);
                return Ok(new { message = "Estudiante inscrito exitosamente", estudiante });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Error al inscribir al estudiante", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Se produjo un error inesperado", error = ex.Message });
            }
        }
    }
}
