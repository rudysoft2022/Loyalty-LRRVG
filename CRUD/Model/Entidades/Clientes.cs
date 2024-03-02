using System;
using System.Security.Cryptography;
using System.Text;

namespace CRUD.Model.Entidades
{
    public class Clientes
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        private string password = string.Empty;
        public string Password
        {
            get { return password; }
            set { password = PasswordMD5.ComputeMD5(value); }
        }
    }
    public static class PasswordMD5
    {
        public static string ComputeMD5(string s)
        {
            StringBuilder sb = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
                foreach (byte b in hashValue)
                {
                    sb.Append($"{b:X2}");
                }
            }

            return sb.ToString();
        }
    }

}
