using apic.Coneccion;
using apic.Modelo;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;



namespace apic.Controlador
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        [HttpGet("{nombre}")]
        public IActionResult Consulta(string nombre)
        {
            try
            {
                DataSet res = Conectar.Mostrar(nombre);
                DataTable dt = res.Tables[0];
                string data = JsonConvert.SerializeObject(dt);
                return Ok(data);

            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult insertar(Usuario user)
        {
            try
            {
                object res = Conectar.insertar(user);
                string data = JsonConvert.SerializeObject(res);
                return Ok(data);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
            
        }
        [Route("juego")]
        [HttpPost]
        public IActionResult Usuario(Ruleta ruleta)
        {
            try
            {
                object nuevo = new object();
                nuevo = Metodos.obj(ruleta);
                string res = JsonConvert.SerializeObject(nuevo);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
