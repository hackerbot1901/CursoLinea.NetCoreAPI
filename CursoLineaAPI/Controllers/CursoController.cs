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
                //1. Obtener únicamente los parámetros de interés para la creación
                var nombre = curso.Nombre;
                var descripcion = curso.Descripcion;
                var duracionHoras = curso.DuracionHoras;
                Curso nuevoCurso = new()
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    DuracionHoras = duracionHoras
                };

                //2. Enviar al modelo
                DAOFactory.GetFactory().GetCursoDAO().Create(nuevoCurso);

                //3. Respuesta
                return Ok(new { message = "Curso agregado exitosamente", curso = nuevoCurso });
            }
            catch (Exception ex)
            {
                // Manejar la excepción, puedes registrarla, devolver un mensaje genérico o personalizado, etc.
                return StatusCode(500, new { message = "Error al agregar el curso", error = ex.Message });
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
        public dynamic EditarCursoExistente([FromQuery] int id, Curso cursoActualizado)
        {
            try
            {
       
                var cursoEnBaseDeDatos = DAOFactory.GetFactory().GetCursoDAO().Read(id);

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
        public dynamic AsignarProfesorCurso([FromQuery]int idCurso, Profesor profesor)
        {
            try
            {
                /*Buscar el curso que exista*/
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
                /*Si la propiedad es nula quiere decir que no tiene asignado un profesor al Curso*/
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
