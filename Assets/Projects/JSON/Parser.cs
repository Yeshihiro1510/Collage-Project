using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Projects.JSON
{
    public class Parser : MonoBehaviour
    {
        public const string PATH1 = "Assets/Resources/DataBase_ParsedToJson.txt";
        public const string PATH2 = "Assets/Resources/Untitled spreadsheet - Sheet1.csv";

        public DataBase DataBase;
        public DataBase DataBase2;

        public void Start()
        {
            ParseToCVS(DataBase);
            DataBase2 = DeserializeFromCVS();
        }

        public static void ParseToCVS(DataBase dataBase)
        {
            using var sw = new StreamWriter(PATH1);
            var s = JsonSerializer.CreateDefault();
            s.Serialize(sw, dataBase);
        }

        public static DataBase DeserializeFromCVS()
        {
            using var sr = new StreamReader(PATH1);
            var jr = new JsonTextReader(sr);
            var s = new JsonSerializer();
            return s.Deserialize<DataBase>(jr);
            
            // var dataBase = new DataBase();
            // var array = sr.ReadToEnd().Split(',');
            // dataBase.Data = new Data[array.Length / 2];
            // for (var i = 0; i < array.Length; i += 2)
            // {
            //     print(array[i + 1]);
            //     dataBase.Data[i / 2] = new Data(array[i], int.Parse(array[i + 1]));
            // }
            // return dataBase;
        }
    }
}