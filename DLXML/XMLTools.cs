using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DLXML
{
    public class XMLTools
    {

        public static string dir = @"xml\";

        static XMLTools()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        /// <summary>
        /// loads the file 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>the root element of the file</returns>
        public static XElement LoadFile(string path)
        {
            try
            {
                if (File.Exists(dir + path))
                {
                    return XElement.Load(dir + path);
                }
                else
                {
                    XElement rootElement = new XElement(path);
                    rootElement.Save(dir + path);
                    return rootElement;
                }
            }
            catch (FileLoadException e)
            {
                throw new FileLoadException(e.Message, e.FileName);
            }
            catch (Exception e)
            {
                throw new FileLoadException(e.Message, dir + path);
            }
        }

        /// <summary>
        /// saves the root element to the file
        /// </summary>
        /// <param name="rootElem"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static bool SaveFile(XElement rootElem, string path)
        {
            try
            {
                rootElem.Save(dir + path);
                return true;
            }
            catch (Exception ex)
            {
                //throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
            return false;
        }
    }
}
