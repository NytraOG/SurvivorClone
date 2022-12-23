using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Survivor", order = 0)]
    public class Survivor : ScriptableObject
    {
        public Sprite sprite;
        public string survivorName;

        private void ApplyModifiers<T>(T unit)
                where T : MonoBehaviour { }

        private void ApplyAbilities<T>(T unit)
                where T : MonoBehaviour { }
    }
}