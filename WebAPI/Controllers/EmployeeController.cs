using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private TestApiContext _context;

        public EmployeeController(TestApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Employee> Get() => _context.Employees.ToList();

        [HttpGet("{id}")]
        public Employee Get(int id) => _context.Employees.FirstOrDefault(o => o.EmployeeID == id);

        [HttpPost]
        public IActionResult Add(Employee nuevoEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(nuevoEmployee);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Employee employeeEditado)
        {
            if (ModelState.IsValid)
            {
                var employeeExistente = _context.Employees.AsNoTracking().FirstOrDefault(o => o.EmployeeID == id);
                if (employeeExistente != null)
                {
                    employeeEditado.EmployeeID = id;
                    _context.Employees.Update(employeeEditado);
                    _context.SaveChanges();

                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employeeExistente = _context.Employees.Find(id);
            if (employeeExistente != null)
            {
                _context.Employees.Remove(employeeExistente);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
