using Microsoft.AspNetCore.Mvc;
using apisNET.Services;
using apisNET.Models;

namespace apisNET.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TareaController : ControllerBase
{
    //Sera inyectado
    ITareasService tareasService;

    public TareaController(ITareasService service)
    {
        tareasService = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(tareasService.Get());
    }

    [HttpPost]
    //FromBody porque lo recibimos del cuerpo del request
    public IActionResult Post([FromBody] Tarea tarea)
    {
        tareasService.Save(tarea);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Tarea tarea)
    {
        tareasService.Update(id, tarea);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        tareasService.Delete(id);
        return Ok();
    }
}