using System;
using System.Collections.Generic;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelData
    {
        public List<FinalAtmData> AtmList = new List<FinalAtmData>(3);
    }
}