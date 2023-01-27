public class TimeMiddleware
{
    //Nos ayuda a invocar el Middleware dentro del ciclo.
    readonly RequestDelegate next;

    //Constructor para poder recibir esta dependencia y llamar al siguiente Request
    public TimeMiddleware(RequestDelegate nextRequest)
    {
        next = nextRequest;
    }

    //Aqui viene toda la informacion del request
    public async Task Invoke(HttpContext context)
    {
        //Si en el request dentro del query existe algun parametro que tenga una key igual a time y si esta key existe hacemos un await.Response vamos a escribir la hora actual
        if(context.Request.Query.Any(p => p.Key == "time"))
        {
            await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
            return;
        }

        //Invoca el middleware que sigue. Si pusieramos esto antes de la anterior función siempre obtendremos la respuesta del ultimo middleware y luego agregamos al logica. Al colocarlo aquí, primero añadiremos nuestro middleware, y luego se ejecuta el siguiente middleware. Primero agregaremos la fecha y luego el siguiente middleware.
        await next (context);
    }

}

//Creamos una pequeña clase que nos ayudará a agregar el middleware dentro de Program.cs
public static class TimeMiddlewareExtension
{
    //Recibimos como parámetro el contexto actual del builder, tomamos el builder, agregamos el middleware de nosotros y retornamos con el siguiente middleware de la secuencia de comandos
    public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TimeMiddleware>();
    }
}