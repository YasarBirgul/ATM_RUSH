using System;
using System.Collections.Generic;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class CollectableData
    {
        public CollectableType CollectableType = CollectableType.Money;
        public List<CollectableParticleData> CollectableParticleSpriteList;
    }
}