using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Policy;
using Cassandra;
using System.Text.RegularExpressions;
using Cassandra.Data.Linq;

namespace Data2
{
    public class Conecction
    {
        private static Conecction _instance;
        private static readonly object _lock = new object();
        private Cluster _cluster;
        private ISession _session;
        private Guid idUser;

        private Conecction()
        {
            try
            {
                string user = ConfigurationManager.AppSettings["CASSANDRA_USER"];
                string pwd = ConfigurationManager.AppSettings["CASSANDRA_PASSWORD"];
                string[] nodes = ConfigurationManager.AppSettings["CASSANDRA_NODES"].Split(',');
                string keyspace = "hotel";

                QueryOptions queryOptions = new QueryOptions().SetConsistencyLevel(ConsistencyLevel.One);

                _cluster = Cluster.Builder()
                    .AddContactPoints(nodes)
                    .WithCredentials(user, pwd)
                    .WithQueryOptions(queryOptions)
                    .Build();
                _session = _cluster.Connect(keyspace);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar con Cassandra: " + ex.Message);
            }
        }
        public static Conecction Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Conecction();
                    }
                    return _instance;
                }
            }
        }
        public int Login(string correo, string contrasena)
        {
            try
            {
                var preparedStatement = _session.Prepare("SELECT estahabilitado, contrasena, esadministrador, id FROM hotel.Usuarios WHERE correo_electronico = ? ALLOW FILTERING");
                var boundStatement = preparedStatement.Bind(correo);

                var rows = _session.Execute(boundStatement);

                foreach (var row in rows)
                {
                    bool estaHabilitado = row.GetValue<bool>("estahabilitado");
                    if (!estaHabilitado)
                    {
                        return -1; // Usuario deshabilitado
                    }

                    List<string> contrasenas = row.GetValue<List<string>>("contrasena");

                    if (contrasenas[contrasenas.Count - 1] == contrasena)
                    {
                        bool isAdmin = row.GetValue<bool>("esadministrador");
                        idUser = row.GetValue<Guid>("id");
                        return isAdmin ? 2 : 1;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar login: " + ex.Message);
            }
        }


        public void Register(string email, string password, string name, string nomina, string birthday, string residence,
                     string homePhone, string personalPhone, bool isAdmin, DateTime dateNow)
        {
            try
            {
                if (!PasswordValidator.ValidatePassword(password))
                {
                    throw new Exception("La contraseña debe tener al menos 8 caracteres, incluir una mayúscula, una minúscula y un carácter especial.");
                }

                var preparedStatement = _session.Prepare("INSERT INTO hotel.Usuarios (id, correo_electronico, contrasena, nombre_completo, numero_nomina, fecha_nacimiento, domicilio, telefono_casa, telefono_celular, esAdministrador, fecha_registro, estaHabilitado) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)");

                Guid id = Guid.NewGuid();
                List<string> contrasena = new List<string> { password };
                LocalDate fechaNacimiento = LocalDate.Parse(birthday);
                LocalDate fechaRegistro = new LocalDate(dateNow.Year, dateNow.Month, dateNow.Day);

                var boundStatement = preparedStatement.Bind(id, email, contrasena, name, nomina, fechaNacimiento, residence, homePhone, personalPhone, isAdmin, fechaRegistro, true);

                _session.Execute(boundStatement);

                Console.WriteLine("Registro exitoso.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar usuario: " + ex.Message);
            }
        }


        public void RegistrarHabitacion(int nCamas, Dictionary<int, string> Camas, string precioNoche, int personasPorHabitacion,
                                        string nivel, string carac, string amenidades, string idHotel)
        {
            if (string.IsNullOrEmpty(idHotel))
            {
                throw new ArgumentNullException(nameof(idHotel), "El ID del hotel no puede ser nulo o vacío.");
            }
            var preparedStatement = _session.Prepare("INSERT INTO hotel.Habitacion (id_habitacion, id_hotel, amenidades, numero_camas, cama, precio_noche, cantidad_personas, nivel, caracteristicas_adicionales, idUsuario, fechaRegistro, horaRegistro, reservada) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)");

            Guid idHabitacion = Guid.NewGuid();
            LocalDate fechaRegistro = new LocalDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            LocalTime horaDeRegistro = new LocalTime(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 0);

            var boundStatement = preparedStatement.Bind(
                    idHabitacion,
                    Guid.Parse(idHotel),
                    amenidades,
                    nCamas,
                    Camas,
                    precioNoche,
                    personasPorHabitacion,
                    nivel,
                    carac,
                    idUser,
                    fechaRegistro,
                    horaDeRegistro,
                    false
                );

            _session.Execute(boundStatement);
        }
        public void RegistrarHotel(string nombreHotel, string ciudad, string estado, string pais, string domicilio, int nPisos,
    int nHabitaciones, bool esTuristica, List<string> serviciosAdicionales, string caracAdicionales, DateTime inicioOp)
        {
            // Prepare the CQL statement
            var preparedStatement = _session.Prepare(
                "INSERT INTO Hoteles (id, nombre_hotel, ciudad, estado, pais, domicilio, num_pisos, num_habitaciones, esTuristica, servicios, carac_adicionales, idUsuario, fecha_registro, hora_registro, fecha_inicio_operaciones) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
            );

            // Generate new UUID for the hotel
            Guid idHotel = Guid.NewGuid();

            // Current date and time for registration
            LocalDate fechaRegistro = new LocalDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            LocalTime horaRegistro = new LocalTime(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 0);

            // Date for start of operations
            LocalDate fechaInicioOperaciones = new LocalDate(inicioOp.Year, inicioOp.Month, inicioOp.Day);

            // Bind the values to the statement
            var boundStatement = preparedStatement.Bind(
                idHotel,
                nombreHotel,
                ciudad,
                estado,
                pais,
                domicilio,
                nPisos,
                nHabitaciones,
                esTuristica,
                new HashSet<string>(serviciosAdicionales),
                caracAdicionales,
                idUser,
                fechaRegistro,
                horaRegistro,
                fechaInicioOperaciones
            );

            // Execute the statement
            _session.Execute(boundStatement);
        }
        public void RegistrarCliente(string nombre, string domicilio, string rfc, string email, string telCasa,
    string telPersonal, string referenciaHotel, DateTime fechaNacimiento, string estadoCivil)
        {
            try
            {
                // Preparar la declaración CQL
                var preparedStatement = _session.Prepare("INSERT INTO clientes (id_cliente, nombre, domicilio, email, rfc, telefono_casa, telefono_celular, referencia, fecha_nacimiento, estado_civil, id_usuario, fecha_registro, hora_registro) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)");

                // Generar un nuevo UUID para el cliente
                Guid idCliente = Guid.NewGuid();

                // Fecha y hora actuales para el registro
                LocalDate fechaRegistro = new LocalDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                LocalTime horaRegistro = new LocalTime(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 0);

                // Convertir la fecha de nacimiento a LocalDate
                LocalDate fechaNacimientoCql = new LocalDate(fechaNacimiento.Year, fechaNacimiento.Month, fechaNacimiento.Day);

                // Asignar los valores a la declaración
                var boundStatement = preparedStatement.Bind(
                    idCliente,
                    nombre,
                    domicilio,
                    email,
                    rfc,
                    telCasa,
                    telPersonal,
                    referenciaHotel,
                    fechaNacimientoCql,
                    estadoCivil,
                    idUser,
                    fechaRegistro,
                    horaRegistro
                );

                // Ejecutar la declaración
                _session.Execute(boundStatement);

                Console.WriteLine("Cliente registrado exitosamente.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar cliente: " + ex.Message);
            }
        }

        public RowSet MostrarClientes(string apellidos, string rfc, string email)
        {
            string cqlCommand = "";

            if (apellidos == "" && rfc == "" && email == "")
            {
                cqlCommand = "SELECT id_cliente, nombre, domicilio, email, rfc, telefono_casa, telefono_celular, referencia, fecha_nacimiento, estado_civil, id_usuario, fecha_registro, hora_registro FROM hotel.clientes";
            }
            else if (apellidos != "")
            {
                cqlCommand = "SELECT id_cliente, nombre, domicilio, email, rfc, telefono_casa, telefono_celular, referencia, fecha_nacimiento, estado_civil, id_usuario, fecha_registro, hora_registro FROM clientes WHERE nombre = ? allow filtering";
            }
            else if (rfc != "")
            {
                cqlCommand = "SELECT id_cliente, nombre, domicilio, email, rfc, telefono_casa, telefono_celular, referencia, fecha_nacimiento, estado_civil, id_usuario, fecha_registro, hora_registro FROM clientes WHERE rfc = ? allow filtering";
            }
            else if (email != "")
            {
                cqlCommand = "SELECT id_cliente, nombre, domicilio, email, rfc, telefono_casa, telefono_celular, referencia, fecha_nacimiento, estado_civil, id_usuario, fecha_registro, hora_registro FROM clientes WHERE email = ? allow filtering";
            }

            var statement = _session.Prepare(cqlCommand);
            BoundStatement boundStatement = null;

            if (!string.IsNullOrEmpty(apellidos))
            {
                boundStatement = statement.Bind(apellidos);
            }
            else if (!string.IsNullOrEmpty(rfc))
            {
                boundStatement = statement.Bind(rfc);
            }
            else if (!string.IsNullOrEmpty(email))
            {
                boundStatement = statement.Bind(email);
            }

            RowSet resultSet;
            if (boundStatement != null)
                resultSet = _session.Execute(boundStatement);
            else
                resultSet = _session.Execute(statement.Bind());

            return resultSet;
        }

        public RowSet MostrarCiudad()
        {
            string cqlCommand = "SELECT ciudad FROM hotel.hoteles";
            var statement = _session.Prepare(cqlCommand);
            var resultSet = _session.Execute(statement.Bind());
            return resultSet;
        }
        public RowSet MostrarHotelesEnCiudad(string ciudad)
        {
            try
            {
                // Preparar la consulta CQL para seleccionar todos los hoteles en una ciudad específica
                var preparedStatement = _session.Prepare("SELECT id, nombre_hotel, ciudad, estado, pais, domicilio, num_pisos, num_habitaciones, esTuristica, servicios, carac_adicionales, idUsuario, fecha_registro, hora_registro, fecha_inicio_operaciones FROM hotel.hoteles WHERE ciudad = ? ALLOW FILTERING");

                // Vincular el nombre de la ciudad al statement preparado
                var boundStatement = preparedStatement.Bind(ciudad);

                // Ejecutar la consulta y devolver el resultado
                return _session.Execute(boundStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar hoteles en la ciudad: " + ex.Message);
            }
        }
        public RowSet MostrarHabitacionesDisponiblesEnHotel(Guid idHotel)
        {
            try
            {
                // Preparar la consulta CQL para seleccionar las habitaciones no reservadas en un hotel específico
                var preparedStatement = _session.Prepare("SELECT id_habitacion, id_hotel, amenidades, numero_camas, cama, precio_noche, cantidad_personas, nivel, caracteristicas_adicionales, idUsuario, fechaRegistro, horaRegistro, reservada FROM hotel.habitacion WHERE id_hotel = ? AND reservada = false allow filtering");

                // Vincular el ID del hotel al statement preparado
                var boundStatement = preparedStatement.Bind(idHotel);

                // Ejecutar la consulta y devolver el resultado
                return _session.Execute(boundStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar habitaciones disponibles en el hotel: " + ex.Message);
            }
        }
        public int ObtenerPersonasPorHab(Guid idHabitacion)
        {
            try
            {
                // Preparar la consulta CQL para seleccionar la cantidad de personas por habitación
                var preparedStatement = _session.Prepare("SELECT cantidad_personas FROM hotel.habitacion WHERE id_habitacion = ? allow filtering");

                // Vincular el ID de la habitación al statement preparado
                var boundStatement = preparedStatement.Bind(idHabitacion);

                // Ejecutar la consulta
                var row = _session.Execute(boundStatement).FirstOrDefault();

                // Verificar si se encontró una fila
                if (row != null)
                {
                    // Obtener el valor de la columna 'cantidad_personas'
                    int cantidadPersonas = row.GetValue<int>("cantidad_personas");
                    return cantidadPersonas;
                }
                else
                {
                    // No se encontró la habitación
                    throw new Exception("No se encontró la habitación con el ID especificado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cantidad de personas por habitación: " + ex.Message);
            }
        }
        public void GuardarReservacion(List<Guid> habitaciones, int cantidadPersonasHospedadas, DateTime fechaInicio,
    DateTime fechaFin, Guid idCliente, string anticipo, string medioPago, string precioFinal)
        {
            try
            {
                // Preparar la declaración CQL para insertar una nueva reservación
                var preparedStatement = _session.Prepare(
                    "INSERT INTO hotel.Reservaciones (codigo_reservacion, habitaciones, fecha_inicio, fecha_fin, id_cliente, cantidad_personas_hospedadas, anticipo, restante_pagar, medio_pago, id_usuario_registro, hora_registro, fecha_registro) " +
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
                );

                // Generar un nuevo UUID para el código de reservación
                Guid codigoReservacion = Guid.NewGuid();

                // Obtener la fecha y hora actuales para el registro
                LocalDate fechaRegistro = new LocalDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                LocalTime horaRegistro = new LocalTime(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 0);

                LocalDate FechaInicio = new LocalDate(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day);
                LocalDate FechaFin = new LocalDate(fechaFin.Year, fechaFin.Month, fechaFin.Day);

                //string restante = ();

                // Bind the values to the statement
                var boundStatement = preparedStatement.Bind(
                    codigoReservacion,
                    habitaciones,
                    FechaInicio,
                    FechaFin,
                    idCliente,
                    cantidadPersonasHospedadas,
                    anticipo,
                    (Convert.ToInt32(precioFinal) - Convert.ToInt32(anticipo)).ToString(), // restante_pagar (inicialmente vacío)
                    medioPago,
                    idUser,
                    horaRegistro,
                    fechaRegistro
                );

                // Ejecutar la declaración
                _session.Execute(boundStatement);

                Console.WriteLine("Reservación guardada exitosamente.");
                MarcarHabitacionesComoReservadas(habitaciones);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la reservación: " + ex.Message);
            }
        }
        public void MarcarHabitacionesComoReservadas(List<Guid> habitaciones)
        {
            try
            {
                var preparedStatement = _session.Prepare("UPDATE hotel.habitacion SET reservada = ? WHERE id_habitacion = ?");

                foreach (var idHabitacion in habitaciones)
                {
                    var boundStatement = preparedStatement.Bind(true, idHabitacion);
                    _session.Execute(boundStatement);
                }

                Console.WriteLine("Habitaciones marcadas como reservadas exitosamente.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al marcar habitaciones como reservadas: " + ex.Message);
            }
        }

        public void MarcarHabitacionesComoDisponibles(Guid idReservacion)
        {
            try
            {
                // Preparar la declaración CQL para obtener las habitaciones asociadas a la reservación
                var selectStatement = _session.Prepare("SELECT habitaciones FROM hotel.Reservaciones WHERE codigo_reservacion = ? ALLOW FILTERING");
                var boundSelectStatement = selectStatement.Bind(idReservacion);

                // Ejecutar la consulta
                var rows = _session.Execute(boundSelectStatement);

                // Verificar si se encontró la reservación
                var row = rows.FirstOrDefault();
                if (row == null)
                {
                    throw new Exception("No se encontró la reservación con el ID especificado.");
                }

                // Obtener la lista de habitaciones asociadas a la reservación
                var habitaciones = row.GetValue<List<Guid>>("habitaciones");

                // Preparar la declaración CQL para actualizar el estado de las habitaciones a no reservadas
                var updateStatement = _session.Prepare("UPDATE hotel.habitacion SET reservada = ? WHERE id_habitacion = ?");

                // Marcar cada habitación como no reservada
                foreach (var idHabitacion in habitaciones)
                {
                    var boundUpdateStatement = updateStatement.Bind(false, idHabitacion);
                    _session.Execute(boundUpdateStatement);
                }

                Console.WriteLine("Habitaciones marcadas como disponibles exitosamente.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al marcar habitaciones como disponibles: " + ex.Message);
            }
        }
        public void CheckIN(Guid codigoReservacion)
        {
            try
            {
                // Preparar la declaración CQL para insertar un nuevo check-in
                var preparedStatement = _session.Prepare(
                    "INSERT INTO hotel.Checkin (id_checkin, codigo_reservacion, fecha_checkin) VALUES (?, ?, ?)"
                );

                // Generar un nuevo UUID para el id_checkin
                Guid idCheckin = Guid.NewGuid();

                // Obtener la fecha actual para el check-in
                LocalDate fechaCheckin = new LocalDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                // Asignar los valores a la declaración
                var boundStatement = preparedStatement.Bind(
                    idCheckin,
                    codigoReservacion,
                    fechaCheckin
                );

                // Ejecutar la declaración
                _session.Execute(boundStatement);

                Console.WriteLine("Check-in guardado exitosamente.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar el check-in: " + ex.Message);
            }
        }
        public void GuardarCancelacion(Guid idReservacion)
        {
            try
            {
                // Preparar la declaración CQL para insertar una nueva cancelación
                var preparedStatement = _session.Prepare(
                    "INSERT INTO hotel.Cancelacion (id_cancelacion, id_usuario, fecha_cancelacion, hora_cancelacion, id_reserva) VALUES (?, ?, ?, ?, ?)"
                );

                // Generar un nuevo UUID para el id_cancelacion
                Guid idCancelacion = Guid.NewGuid();

                // Obtener la fecha y hora actuales para el registro
                LocalDate fechaCancelacion = new LocalDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                LocalTime horaCancelacion = new LocalTime(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 0);

                // Asignar los valores a la declaración
                var boundStatement = preparedStatement.Bind(
                    idCancelacion,
                    idUser,
                    fechaCancelacion,
                    horaCancelacion,
                    idReservacion
                );

                // Ejecutar la declaración
                _session.Execute(boundStatement);

                Console.WriteLine("Cancelación guardada exitosamente.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la cancelación: " + ex.Message);
            }
        }


        public RowSet ObtenerReservacion(Guid codigoReservacion)
        {
            try
            {
                var preparedStatement = _session.Prepare("SELECT codigo_reservacion, habitaciones, fecha_inicio, fecha_fin, id_cliente, cantidad_personas_hospedadas, anticipo, restante_pagar, medio_pago, id_usuario_registro, hora_registro, fecha_registro FROM hotel.Reservaciones WHERE codigo_reservacion = ?");
                var boundStatement = preparedStatement.Bind(codigoReservacion);

                var rows = _session.Execute(boundStatement);

                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la reservación: " + ex.Message);
            }
        }

        public RowSet ObtenerHotelesPorCiudadYPais(string ciudad, string pais)
        {
            var query = "SELECT id, nombre_hotel, ciudad, estado, pais, num_habitaciones FROM Hoteles WHERE ciudad = ? AND pais = ?";
            var preparedStatement = _session.Prepare(query);
            var boundStatement = preparedStatement.Bind(ciudad, pais);
            return _session.Execute(boundStatement);
        }

        public RowSet ObtenerHabitacionesPorHoteles(List<Guid> hotelIds)
        {
            var query = "SELECT id_habitacion, id_hotel, numero_camas, cantidad_personas, precio_noche FROM Habitacion WHERE id_hotel IN ?";
            var preparedStatement = _session.Prepare(query);
            var boundStatement = preparedStatement.Bind(hotelIds);
            return _session.Execute(boundStatement);
        }
        public void DeshabilitarUsuario(string correo)
        {
            try
            {
                var preparedStatement = _session.Prepare("UPDATE hotel.Usuarios SET estahabilitado = false WHERE correo_electronico = ? ALLOW FILTERING");
                var boundStatement = preparedStatement.Bind(correo);
                _session.Execute(boundStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al deshabilitar usuario: " + ex.Message);
            }
        }
        public void HabilitarUsuario(string correo)
        {
            try
            {
                var preparedStatement = _session.Prepare("UPDATE hotel.Usuarios SET estahabilitado = true WHERE correo_electronico = ? ALLOW FILTERING");
                var boundStatement = preparedStatement.Bind(correo);
                _session.Execute(boundStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al habilitar usuario: " + ex.Message);
            }
        }
        public int CorreoExiste(string correo)
        {
            try
            {
                var preparedStatement = _session.Prepare("SELECT COUNT(*) FROM hotel.Usuarios WHERE correo_electronico = ? ALLOW FILTERING");
                var boundStatement = preparedStatement.Bind(correo);

                var row = _session.Execute(boundStatement).FirstOrDefault();

                // Verifica si se encontró una fila y cuenta las coincidencias
                if (row != null)
                {
                    long count = row.GetValue<long>("count");
                    return count > 0 ? 1 : 0;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la existencia del correo: " + ex.Message);
            }
        }

        public int ValidarCambioPassword(string newPassword)
        {
            //funcion para validar que la nueva contrasena sea diferente a las 3 anteriores que el usuario ha tenido
            var preparedStatement = _session.Prepare("SELECT contrasena FROM hotel.Usuarios WHERE id = ?");
            var boundStatement = preparedStatement.Bind(idUser);
            var rows = _session.Execute(boundStatement);

            var row = rows.FirstOrDefault();

            // Obtener la lista de contraseñas
            List<string> contrasenas = row.GetValue<List<string>>("contrasena");
            if (contrasenas.Count >= 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (contrasenas[i] == newPassword)
                        return 1;
                }
                contrasenas.Insert(0, newPassword);
                var updatePreparedStatement = _session.Prepare("UPDATE hotel.Usuarios SET contrasena = ? WHERE id = ?");
                var boundUpdateStatement = updatePreparedStatement.Bind(contrasenas, idUser);

                _session.Execute(boundUpdateStatement);
            }
            return 0;
        }
        public int HotelExiste(Guid idHotel)
        {
            try
            {
                var preparedStatement = _session.Prepare("SELECT COUNT(*) FROM hotel.hoteles WHERE id = ?");
                var boundStatement = preparedStatement.Bind(idHotel);

                var row = _session.Execute(boundStatement).FirstOrDefault();

                // Verifica si se encontró una fila y cuenta las coincidencias
                if (row != null)
                {
                    long count = row.GetValue<long>("count");
                    return count > 0 ? 1 : 0;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la existencia del hotel: " + ex.Message);
            }
        }
        //inicio
        public RowSet BuscarClientes(string nombre, string email, string rfc)
        {
            try
            {
                string query = "SELECT id_cliente, nombre, email, rfc FROM hotel.clientes WHERE ";

                List<string> conditions = new List<string>();
                if (!string.IsNullOrEmpty(nombre))
                    conditions.Add("nombre = '" + nombre + "'");
                if (!string.IsNullOrEmpty(email))
                    conditions.Add("email = '" + email + "'");
                if (!string.IsNullOrEmpty(rfc))
                    conditions.Add("rfc = '" + rfc + "'");

                if (conditions.Count == 0)
                    throw new Exception("Debe especificar al menos un criterio de búsqueda.");

                query += string.Join(" AND ", conditions) + " ALLOW FILTERING";

                var preparedStatement = _session.Prepare(query);
                var boundStatement = preparedStatement.Bind();

                return _session.Execute(boundStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar clientes: " + ex.Message);
            }
        }
        public RowSet ObtenerReservaciones()
        {
            try
            {
                var query = "SELECT codigo_reservacion, habitaciones, fecha_inicio, fecha_fin, id_cliente, cantidad_personas_hospedadas, anticipo, restante_pagar, medio_pago, id_usuario_registro, hora_registro, fecha_registro FROM hotel.Reservaciones";
                var statement = _session.Prepare(query);
                var boundStatement = statement.Bind();
                return _session.Execute(boundStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reservaciones: " + ex.Message);
            }
        }

        //fin

        //inicio hoteles parte 2
        public RowSet BuscarHoteles(string ciudad, string pais, int anho)
        {
            try
            {
                string query = "SELECT id, pais, ciudad, fecha_inicio_operaciones FROM hotel.hoteles WHERE ";

                List<string> conditions = new List<string>();
                if (!string.IsNullOrEmpty(ciudad))
                    conditions.Add("ciudad = '" + ciudad + "'");
                if (!string.IsNullOrEmpty(pais))
                    conditions.Add("pais = '" + pais + "'");
                if (anho > 0)
                    conditions.Add("EXTRACT(YEAR FROM fecha_inicio_operaciones) = " + anho);

                if (conditions.Count == 0)
                    throw new Exception("Debe especificar al menos un criterio de búsqueda.");

                query += string.Join(" AND ", conditions) + " ALLOW FILTERING";

                var preparedStatement = _session.Prepare(query);
                var boundStatement = preparedStatement.Bind();

                return _session.Execute(boundStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar hoteles: " + ex.Message);
            }
        }
        public RowSet ObtenerHabitacionesPorHotel(Guid hotelId)
        {
            try
            {
                var query = "SELECT id_habitacion FROM hotel.Habitacion WHERE id_hotel = ? allow filtering";
                var preparedStatement = _session.Prepare(query);
                var boundStatement = preparedStatement.Bind(hotelId);
                return _session.Execute(boundStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener habitaciones por hotel: " + ex.Message);
            }
        }

        public RowSet ObtenerReservacionesPorHabitaciones(List<Guid> habitacionesIds)
        {
            try
            {
                var query = "SELECT codigo_reservacion, habitaciones, fecha_inicio, fecha_fin, id_cliente, cantidad_personas_hospedadas, anticipo, restante_pagar, medio_pago, id_usuario_registro, hora_registro, fecha_registro FROM hotel.Reservaciones WHERE habitaciones CONTAINS ? ALLOW FILTERING";
                var preparedStatement = _session.Prepare(query);
                var boundStatement = preparedStatement.Bind(habitacionesIds);
                return _session.Execute(boundStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reservaciones por habitaciones: " + ex.Message);
            }
        }

        public RowSet ObtenerInfoHotel(Guid hotelId)
        {
            try
            {
                var query = "SELECT nombre_hotel, ciudad FROM hotel.Hoteles WHERE id = ?";
                var preparedStatement = _session.Prepare(query);
                var boundStatement = preparedStatement.Bind(hotelId);
                return _session.Execute(boundStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener información del hotel: " + ex.Message);
            }
        }

        //fin
        public static class PasswordValidator
        {
            public static bool ValidatePassword(string password)
            {
                if (password.Length < 8)
                    return false;

                bool hasUpperCase = false;
                bool hasLowerCase = false;
                bool hasSpecialChar = false;

                foreach (char c in password)
                {
                    if (char.IsUpper(c))
                        hasUpperCase = true;
                    else if (char.IsLower(c))
                        hasLowerCase = true;
                    else if (!char.IsLetterOrDigit(c))
                        hasSpecialChar = true;

                    if (hasUpperCase && hasLowerCase && hasSpecialChar)
                        return true;
                }

                return false;
            }
        }

        public ISession GetSession()
        {
            return _session;
        }
        public void Close()
        {
            _cluster?.Dispose();
        }
    }
}