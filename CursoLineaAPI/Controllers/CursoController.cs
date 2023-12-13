using CursoLineaAPI.DAO;
using CursoLineaAPI.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoLineaAPI.Controllers
{
    [ApiController]
    [Route("curso")]
    public class CursoController : ControllerBase
    {
        [HttpPost("agregar")]
        public IActionResult AgregarNuevoCurso(Curso curso)
        {
            try
            {
                Curso nuevoCurso = new()
                {
                    Nombre = curso.Nombre,
                    Descripcion = curso.Descripcion,
                    DuracionHoras = curso.DuracionHoras
                };
                DAOFactory.GetFactory().GetCursoDAO().Create(nuevoCurso);
                return Ok(new { message = "Curso agregado exitosamente", curso = nuevoCurso });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al agregar el curso", error = ex.Message });
            }
        }

        [HttpGet("obtener")]
        public IActionResult ObtenerCurso([FromQuery] int idCurso)
        {
            try
            {
                var curso = DAOFactory.GetFactory().GetCursoDAO().Read(idCurso);
                if (curso == null)
                {
                    return NotFound(new { message = "Curso no encontrado" });
                }
                return Ok(new { curso, message = "Curso obtenido exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Se produjo un error al obtener el curso", error = ex.Message });
            }
        }

        [HttpGet("listado")]
        public dynamic ListarCursos()
        {
            try
            {
                var cursos = DAOFactory.GetFactory().GetCursoDAO().GetAll();
                return Ok(new { cursos, message = "Cursos listados exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Se produjo un error al listar los cursos", error = ex.Message });
            }
        }

        [HttpPut("editar")]
        public dynamic EditarCursoExistente([FromQuery] int idCurso, Curso cursoActualizado)
        {
            try
            {
                var cursoEnBaseDeDatos = DAOFactory.GetFactory().GetCursoDAO().Read(idCurso);
                if (cursoEnBaseDeDatos == null)
                {
                    return NotFound(new { message = "Curso no encontrado" });
                }
                cursoEnBaseDeDatos.Nombre = cursoActualizado.Nombre;
                cursoEnBaseDeDatos.Descripcion = cursoActualizado.Descripcion;
                cursoEnBaseDeDatos.DuracionHoras = cursoActualizado.DuracionHoras;
                DAOFactory.GetFactory().GetCursoDAO().Update(cursoEnBaseDeDatos);
                return Ok(new
                {
                    curso = cursoEnBaseDeDatos,
                    message = "Curso actualizado exitosamente"
                });

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Error al guardar en la base de datos", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Se produjo un error inesperado", error = ex.Message });
            }
        }

        [HttpPut("asignarProfesor")]
        public dynamic AsignarProfesorCurso([FromQuery] int idCurso, Profesor profesor)
        {
            try
            {
                var cursoEnBaseDeDatos = DAOFactory.GetFactory().GetCursoDAO().Read(idCurso);
                var profesorEnBaseDeDatos = DAOFactory.GetFactory().GetEFProfesorDAO().Read(profesor.ProfesorId);
                {
                    if (cursoEnBaseDeDatos == null)
                    {
                        return NotFound(new { message = "Curso no encontrado" });
                    }
                    if (profesorEnBaseDeDatos == null)
                    {
                        return NotFound(new { message = "Profesor no encontrado" });
                    }
                }
                {
                    if (cursoEnBaseDeDatos.ProfesorId != null)
                    {
                        return BadRequest(new { message = "El curso ya tiene un profesor asignado" });
                    }
                    cursoEnBaseDeDatos.ProfesorId = profesorEnBaseDeDatos.ProfesorId;
                    DAOFactory.GetFactory().GetCursoDAO().Update(cursoEnBaseDeDatos);

                }
                return Ok(new { cursoEnBaseDeDatos, message = "Profesor asignado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al asignar profesor al curso", error = ex.Message });
            }
        }
    }
}