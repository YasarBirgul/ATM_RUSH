using UnityEngine;
using Data.ValueObject;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Camera", menuName = "AtmRush/CD_Camera", order = 0)]
    public class CD_Camera : ScriptableObject
    {
        public CameraData CameraData;
    }
}