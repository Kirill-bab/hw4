using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;



namespace Task_1._1
{
/// <summary>
///  I tried to include this class into Library, but it does not contain System.Text.Json
/// </summary>
    [Serializable]
    public class Settings
    {
        public int PrimesFrom { get; set; }
        public int PrimesTo { get; set; }

        public Settings(int from, int to)
        {
            PrimesFrom = from;
            PrimesTo = to;
        }

        public Settings() { }

    }
}
