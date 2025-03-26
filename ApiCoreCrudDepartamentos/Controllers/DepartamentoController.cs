using ApiCoreCrudDepartamentos.Models;
using ApiCoreCrudDepartamentos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreCrudDepartamentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //LA RUTA DE L AAPI API/DEPARTAMENTO -> LO COGE DE AQUI DEL NOMBRE DEL CONTROLLER
    public class DepartamentoController : ControllerBase
    {
        private RepositoryDepartamentos repo;

        public DepartamentoController(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Departamento>>> GetDepartamentos()
        {
            return await this.repo.GetDepartamentosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>> FindDepartamento(int id)
        {
            return await this.repo.FindDepartamentoAsync(id);
        }

        //LOS METODOS POR DEFECTO DE POST O PUT RECIBEN UN OBJETO
        //SI QUEREMOS ENVIAR PARAMETROS DBEMOS MAPEARLOS CON ROUTING
        [HttpPost]
        public async Task<ActionResult> InsertDepartamento(Departamento dept) //recibe el objeto directamente y por debajo, lo transforma a json
        {
            await this.repo.InsertDepartamento(dept.IdDepartamento, dept.Nombre, dept.Localidad);
            return Ok();
        }

        //EL METODO POR DEFECTO DE DELETE RECIBE UN ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartamento(int id)
        {
            await this.repo.DeleteDepartamento(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDepartamento(Departamento dept)
        {
            await this.repo.UpdateDepartamento(dept.IdDepartamento, dept.Nombre, dept.Localidad);
            return Ok();
        }

        //PODEMOS PERSONALIZAR METODOS POST, PUT O DELETE
        //EJ -> POST CON PARAMETROS

        [HttpPost]
        //CUANDO TENGA POCAS PROPIEDADES, SINO NO MERECE LA PENA
        [Route("[action]/{id}/{nombre}/{localidad}")]
        public async Task<ActionResult> PostDepartamento(int id, string nombre, string localidad)
        {
            await this.repo.InsertDepartamento(id, nombre, localidad);
            return Ok();
        }

        //SI LO NECESITAMOS, PODEMOS COMBINAR OBJETOS CON PARAMETROS -> EL OBJETO ES EL ÚLTIMO ELEMENTO QUE SE INCLUYE EN LA PETICION DEL METODO
        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<ActionResult> PutDepartamento(int id, Departamento departamento)
        {
            await this.repo.UpdateDepartamento(id, departamento.Nombre, departamento.Localidad);
            return Ok();
        }
    }
}
