using System.Runtime.CompilerServices;
using ApiCoreCrudDepartamentos.Data;
using ApiCoreCrudDepartamentos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCoreCrudDepartamentos.Repositories
{
    public class RepositoryDepartamentos
    {
        private DepartamentosContext context;

        public RepositoryDepartamentos(DepartamentosContext context)
        {
            this.context = context;
        }

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            return await this.context.Departamentos.ToListAsync();
        }

        public async Task<Departamento> FindDepartamentoAsync(int idDepartamento)
        {
            return await this.context.Departamentos.FirstOrDefaultAsync(x => x.IdDepartamento == idDepartamento);
        }

        public async Task InsertDepartamento(int id, string nombre, string localidad)
        {
            Departamento dept = new Departamento();
            dept.IdDepartamento = id;
            dept.Nombre = nombre;
            dept.Localidad = localidad;
            await this.context.Departamentos.AddAsync(dept);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateDepartamento(int id, string nombre, string localidad)
        {
            Departamento dept = await this.FindDepartamentoAsync(id);
            dept.Nombre = nombre;
            dept.Localidad = localidad;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteDepartamento(int id)
        {
            Departamento dept = await this.FindDepartamentoAsync(id);
            this.context.Departamentos.Remove(dept);
            await this.context.SaveChangesAsync();
        }
    }
}
