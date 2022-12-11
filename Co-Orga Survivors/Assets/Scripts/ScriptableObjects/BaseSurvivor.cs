using UnityEngine;

namespace ScriptableObjects
{
    public abstract class BaseSurvivor : ScriptableObject
    {
        public Sprite     sprite;
        
        public abstract void ApplyModifiers<T>(T unit)
                where T : MonoBehaviour;

        public abstract void ApplyAbilities<T>(T unit)
                where T : MonoBehaviour;
    }
}