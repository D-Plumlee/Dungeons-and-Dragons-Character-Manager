using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DnDEntityLibrary;

public class DnDEntityCollectionXmlConverter
{
    public static async Task DnDEntityCollectionToXmlAsync(DnDEntityCollection col, string fileName)
    {
        await Task.Run(() =>
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(DnDEntityCollection));
            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, col);
            }
        });
    }


    public static async Task<DnDEntityCollection> LoadXmlCollectionFromFileAsync(string fileName)
    {
        return await Task.Run(async () =>
        {
            try
            {
                using (Stream fStream = new FileStream(fileName, FileMode.Open))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(DnDEntityCollection));
                    DnDEntityCollection obj = default;
                    obj = (DnDEntityCollection)xmlFormat.Deserialize(fStream);
                    obj.FileContainedIn = fileName;
                    return obj;
                }
            }
            catch (FileNotFoundException ex)
            {
                return new DnDEntityCollection();
            }
        });
    }

        public static void DnDEntityCollectionToXml(DnDEntityCollection col, string fileName)
    {
        if(col.Entities.Count == 0)
        {
            var path = new FileInfo(fileName);
            path.Delete();
            return;
        }
            XmlSerializer xmlFormat = new XmlSerializer(typeof(DnDEntityCollection));
            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, col);
            }
        
    }


    public static  DnDEntityCollection LoadXmlCollectionFromFile(string fileName)
    {
            try
            {
                using (Stream fStream = new FileStream(fileName, FileMode.Open))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(DnDEntityCollection));
                    DnDEntityCollection obj = default;
                    obj = (DnDEntityCollection)xmlFormat.Deserialize(fStream);
                    return obj;
                }
            }
            catch (FileNotFoundException ex)
            {
                return new DnDEntityCollection();
            }
        
    }
}

