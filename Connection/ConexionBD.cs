// Estamos definiendo un espacio de nombres llamado BabelAPI.Connection
namespace BabelAPI.Connection
{
    // Aquí estamos creando una clase llamada ConexionBD
    public class ConexionBD
    {
        // Declaramos una cadena de conexión privada y la inicializamos como una cadena vacía
        private string connectionstring = string.Empty;

        // Este es el constructor de la clase ConexionBD
        public ConexionBD()
        {
            // Creamos un constructor para la configuración utilizando appsettings.json
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            // Obtenemos la cadena de conexión del archivo de configuración (appsettings.json) y la asignamos a nuestra variable connectionstring
            connectionstring = builder.GetSection("ConnectionStrings:masterconnection").Value;
        }

        // Este es un método público que devuelve la cadena de conexión almacenada
        public string ConnectionString()
        {
            return connectionstring;
        }
    }
}
