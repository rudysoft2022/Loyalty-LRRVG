using System.Xml;
using System;

namespace CRUD.Model.NG
{
    public class ReaderSchema : XmlDocument
    {
        private string nameFile;
         public void ReaderSchemaDAO()
        {
            try
            {
                Load(string.Concat((string)AppDomain.CurrentDomain.GetData("SCHEMA"), "\\SCHEMA\\DAO.xml"));
            }
            catch
            {
                try
                {
                    Load(string.Concat((string)AppDomain.CurrentDomain.GetData("SCHEMA"), "\\SCHEMA\\DAO.xml"));
                }
                catch
                {

                    throw new Exception(string.Concat("No se ha encontrado el archivo de consultas :", nameFile, ".xml"));
                }
                //throw new Exception(string.Concat("No se ha encontrado el archivo de consultas :", nameFile, ".xml"));
            }
        }

        public string GetScript(string namenodo)
        {
            String strScript = string.Empty;
            XmlNodeList sql = GetElementsByTagName(namenodo);

            if (sql.Count <= 0)
            {
                sql = GetElementsByTagName(namenodo.ToLower());
            }
            strScript = sql[0].InnerText.Trim();
            return strScript;
        }
    }
}

