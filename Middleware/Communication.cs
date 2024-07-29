using Data2;
using System;
using System.Collections.Generic;
using Cassandra;
using System.ComponentModel.Composition;

namespace Middleware
{
    public class Communication
    {
        private readonly Conecction conexion;

        public Communication()
        {
            conexion = Conecction.Instance;
        }

        public int Login(string username, string password)
        {
            return conexion.Login(username, password);
        }

        public void Register(string email, string password, string name, string nomina, string birthday, string residence,
                             string homePhone, string personalPhone, bool isAdmin, DateTime dateNow)
        {
            conexion.Register(email, password, name, nomina, birthday, residence, homePhone, personalPhone, isAdmin, dateNow);
        }

        public void RegistrarHabitacion(int nCamas, Dictionary<int, string> camas, string precioNoche, int personasPorHabitacion,
                                        string nivel, string caracteristicasAdicionales, string amenidades, string idHotel)
        {
            conexion.RegistrarHabitacion(nCamas, camas, precioNoche, personasPorHabitacion, nivel, caracteristicasAdicionales, amenidades, idHotel);
        }

        public void RegistrarHotel(string nombreHotel, string ciudad, string estado, string pais, string domicilio, int nPisos,
            int nHabitaciones, bool esTuristica, List<string> serviciosAdicionales, string caracAdicionales, DateTime inicioOp)
        {
            conexion.RegistrarHotel(nombreHotel, ciudad, estado, pais, domicilio, nPisos, nHabitaciones, esTuristica, serviciosAdicionales, caracAdicionales, inicioOp);
        }

        public void RegistrarCliente(string nombre, string domicilio, string rfc, string email, string telCasa,
            string telPersonal, string referenciaHotel, DateTime fechaNacimiento, string estadoCivil)
        {
            conexion.RegistrarCliente(nombre, domicilio, rfc, email, telCasa, telPersonal, referenciaHotel, fechaNacimiento, estadoCivil);
        }
        public RowSet MostrarClientes(string apellidos, string rfc, string email)
        {
            return conexion.MostrarClientes(apellidos, rfc, email);
        }
        public RowSet MostrarCiudades()
        {
            return conexion.MostrarCiudad();
        }
        public RowSet MostrarHotelEnCiudad(string ciudad)
        {
            return conexion.MostrarHotelesEnCiudad(ciudad);
        }
        public RowSet MostrarHabitacionesEnHotel(Guid idHotel)
        {
            return conexion.MostrarHabitacionesDisponiblesEnHotel(idHotel);
        }
        public int ObtenerPersonasPorHabitacion(Guid idHab)
        {
            return conexion.ObtenerPersonasPorHab(idHab);
        }
        public void RegistrarReserva(List<Guid> habitaciones, int cantidadPersonasHospedadas, DateTime fechaInicio,
        DateTime fechaFin, Guid idCliente, string anticipo, string medioPago, string precioFinal)
        {
            conexion.GuardarReservacion(habitaciones, cantidadPersonasHospedadas, fechaInicio, fechaFin,
                idCliente, anticipo, medioPago, precioFinal);
        }
        public void CancelarReservacion(Guid id)
        {
            conexion.MarcarHabitacionesComoDisponibles(id);
        }
        public void Checin(Guid id)
        {
            conexion.CheckIN(id);
        }
        public void GuardarCancelacion(Guid id)
        {
            conexion.GuardarCancelacion(id);
        }

        public RowSet ObtenerReservacion(Guid codigoReservacion)
        {
            return conexion.ObtenerReservacion(codigoReservacion);
        }
        public RowSet ObtenerHotelesPorCiudadYPais(string ciudad, string pais)
        {
            return conexion.ObtenerHotelesPorCiudadYPais(ciudad, pais);
        }

        public RowSet ObtenerHabitacionesPorHoteles(List<Guid> hotelIds)
        {
            return conexion.ObtenerHabitacionesPorHoteles(hotelIds);
        }
        public void DeshabilitarUsuario(string correo)
        {
            conexion.DeshabilitarUsuario(correo);
        }
        public void HabilitarUsuario(string correo)
        {
            conexion.HabilitarUsuario(correo);
        }
        public int VerificarEmail(string email)
        {
            return conexion.CorreoExiste(email);
        }
        public int HotelExiste(string idHotel)
        {
            Guid hotelId;
            if (Guid.TryParse(idHotel, out hotelId))
            {
                return conexion.HotelExiste(hotelId);
            }
            return 0;
        }   
        public RowSet BuscarClientes(string nombre, string email, string rfc)
        {
            return conexion.BuscarClientes(nombre, email, rfc);
        }
        public RowSet ObtenerReservaciones()
        {
            return conexion.ObtenerReservaciones();
        }
        public RowSet BuscarHoteles(string ciudad, string pais, int anho)
        {
            return conexion.BuscarHoteles(ciudad, pais, anho);
        }
        public RowSet ObtenerHabitacionesPorHotel(Guid hotelId)
        {
            return conexion.ObtenerHabitacionesPorHotel(hotelId);
        }

        public RowSet ObtenerReservacionesPorHabitaciones(List<Guid> habitacionesIds)
        {
            return conexion.ObtenerReservacionesPorHabitaciones(habitacionesIds);
        }

        public RowSet ObtenerInfoHotel(Guid hotelId)
        {
            return conexion.ObtenerInfoHotel(hotelId);
        }
    }
}
