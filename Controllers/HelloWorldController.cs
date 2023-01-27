using Microsoft.AspNetCore.Mvc;

namespace apisNET.Controllers;

[ApiController]
[Route("api/[controller]")]

public class HelloWorldController : ControllerBase
{
    //Sera inyectado
    IHelloWorldService helloWorldService;

    //Recibimos el contexto de TareasContext, este nos permitira conectar a la BBDD
    TareasContext dbcontext;

    //Lo recibimos usando el constructor
    public HelloWorldController(IHelloWorldService helloWorld, TareasContext db)
    {
        helloWorldService = helloWorld;
        dbcontext = db;
    }

    [HttpGet]
    //Lo utilizamos y devolvemos su función
    public IActionResult Get()
    {
        return Ok(helloWorldService.GetHelloWorld());
    }

    //Endpoint que nos permitirá hacer la comprobación que la BBDD esta creada
    [HttpGet]
    [Route("createdb")]
    public IActionResult CreateDatabase()
    {
        //Para que no salten errores con los DateTime a la hora de hacer Migrations en PostgreSQL
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        dbcontext.Database.EnsureCreated();
        return Ok();
    }
}