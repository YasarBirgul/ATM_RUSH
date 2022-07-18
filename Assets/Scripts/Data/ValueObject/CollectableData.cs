using System;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class CollectableData
    {
        public CollectableType CollectableType = CollectableType.Money;
    }
}