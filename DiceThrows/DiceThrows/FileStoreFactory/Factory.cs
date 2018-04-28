using DiceThrows.EntityFramework.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DiceThrows.FileStoreFactory
{
    class Factory
    {
        public static FileStore CreateFileStore(string fileStoreName)
        {
            switch (fileStoreName)
            {
                case "JSON":
                    return new JsonContent();
                case "XML":
                    return new XmlContent();
                default:
                    throw new Exception("unknown FileStore request");
            }
        }
    }
    public class JsonContent : FileStore
    {
        public override List<RollResult> DeserializedRollResults(string filename)
        {
            var deserializedLists = new
            {
                RollResultList = new List<RollResult>()
            };
            deserializedLists = JsonConvert.DeserializeAnonymousType(File.ReadAllText(filename), deserializedLists);

            return deserializedLists.RollResultList;
        }
        public override string SerializedRollResults(List<RollResult> rollResultList)
        {
            return JsonConvert.SerializeObject(new { rollResultList });
        }
    }
    public class XmlContent : FileStore
    {
        public override List<RollResult> DeserializedRollResults(string filename)
        {
            var xmlDeserializer = new XmlSerializer(typeof(List<RollResult>));
            List<RollResult> rollResultList;
            using (var myFileStream = new FileStream(filename, FileMode.Open))
            {
                rollResultList = (List<RollResult>)xmlDeserializer.Deserialize(myFileStream);
            }

            return rollResultList;
        }
        public override string SerializedRollResults(List<RollResult> rollResultList)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<RollResult>));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            xmlSerializer.Serialize(xmlTextWriter, rollResultList);

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
    public abstract class FileStore
    {
        public abstract List<RollResult> DeserializedRollResults(string filename);
        public abstract string SerializedRollResults(List<RollResult> rollResultList);
    }
}
