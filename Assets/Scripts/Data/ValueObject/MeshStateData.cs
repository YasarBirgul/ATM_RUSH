using System;


namespace Data.ValueObject
{
    [Serializable]
    public class MeshStateData
    { public enum MeshTypes 
        { 
            MoneyFilter,
            GoldFilter,
            DiamondFilter
        }
        public MeshTypes meshType=MeshTypes.MoneyFilter;
    }
}