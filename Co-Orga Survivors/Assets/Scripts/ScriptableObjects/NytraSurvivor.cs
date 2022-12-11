using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Nytra", menuName = "Survivors/Nytra", order = 0)]
    public class NytraSurvivor : BaseSurvivor
    {
        public override void ApplyModifiers<T>(T unit) => throw new System.NotImplementedException();

        public override void ApplyAbilities<T>(T unit) => throw new System.NotImplementedException();
    }
}