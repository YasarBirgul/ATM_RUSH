using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CollectableSignals : MonoSingleton<CollectableSignals>
    { 
        public UnityAction<GameObject> onMoneyCollection=delegate {  };
        public UnityAction<GameObject> onObstacleCollision=delegate {  };
        public UnityAction<GameObject> onUpgradeMOney=delegate {  };
        public UnityAction onChangeState=delegate {  };
        public UnityAction<GameObject,int> onDeposit=delegate {  };
    }
}