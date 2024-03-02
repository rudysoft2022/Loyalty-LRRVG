using System.Collections.Generic;

namespace CRUD.Model.Entidades
{
    public class Ventas
    {
        public int IdTienda { get; set; }
        public int IdCliente { get; set; }

        public List<DetalleVenta> Articulos { get; set; }
    }
    public class DetalleVenta
    {
        public string Codigo { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
    }
}
