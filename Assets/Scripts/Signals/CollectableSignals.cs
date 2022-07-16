using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CollectableSignals : MonoSingleton<CollectableSignals>
    { 
        public UnityAction<GameObject> onMoneyCollection=delegate {  };
        public UnityAction<GameObject> onObstacleCollision=delegate {  };
        public UnityAction onUpgradeMOney=delegate {  };
        public UnityAction onChangeState=delegate {  };
        public UnityAction onDeposit=delegate {  };
    }
}