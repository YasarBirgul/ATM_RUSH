using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class CollectableSignals : MonoSingleton<CollectableSignals>
    { 
        public UnityAction onMoneyCollection=delegate {  };
        public UnityAction onObstacleCollision=delegate {  };
        public UnityAction onUpgradeMOney=delegate {  };
        public UnityAction onChangeState=delegate {  };
        public UnityAction onDeposit=delegate {  };
    }
}