using Extentions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Signals
{
    public class CollectableSignals : MonoSingleton<CollectableSignals>
    { 
        public UnityAction<GameObject> onMoneyCollection=delegate {  };
        public UnityAction<GameObject,int> onObstacleCollision=delegate {  };
        public UnityAction<GameObject> onPlayerObstacleCollision=delegate {  };
        public UnityAction<GameObject> onUpgradeMOney=delegate {  };
        public UnityAction onChangeState=delegate {  };
        public UnityAction<GameObject,int> onDeposit = delegate {  };
        public UnityAction<GameObject,int> onPlayerAtmCollision = delegate {  };
        public UnityAction<GameObject> onFinalAtmCollision = delegate { };
    }
}