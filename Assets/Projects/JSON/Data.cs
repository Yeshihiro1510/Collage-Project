using System;

namespace Projects.JSON
{
    [Serializable]
    public class Data
    {
        public Data(string name, int number)
        {
            Name = name;
            Number = number;
        }

        public string Name;
        public int Number;
    }
}