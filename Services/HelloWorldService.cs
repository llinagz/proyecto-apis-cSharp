public class HelloWorldService : IHelloWorldService
{
    //Metodos o funciones
    public string GetHelloWorld()
    {
        return "Hello World!";
    }
}

//Vamos a crear una interfaz. Esto nos ayudara a manejar un tipo abstracto que podremos cambiar facilmente si queremos. Implementandola en la clase, nos permitirá usar la función que le indiquemos.
public interface IHelloWorldService
{
    string GetHelloWorld();
}