using Microsoft.AspNetCore.Mvc;
using apisNET.Services;
using apisNET.Models;

namespace apisNET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    //Sera inyectado
    ICategoriaService categoriaService;

    //Lo recibimos usando el constructor
    public CategoriaController(ICategoriaService service)
    {
        categoriaService = service;
    }

    [HttpGet]
    //Lo utilizamos y devolvemos su función
    public IActionResult Get()
    {
        return Ok(categoriaService.Get());
    }

    [HttpPost]
    //FromBody porque lo recibimos del cuerpo del request
    public IActionResult Post([FromBody] Categoria categoria)
    {
        categoriaService.Save(categoria);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Categoria categoria)
    {
        categoriaService.Update(id, categoria);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        categoriaService.Delete(id);
        return Ok();
    }
}