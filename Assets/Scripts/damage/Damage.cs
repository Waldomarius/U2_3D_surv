using UnityEngine;

namespace damage
{
    public class Damage :  MonoBehaviour
    {
        [SerializeField] protected float _weight = 100f;
        
        public void UpdateWeight(float value)
        {
            _weight -= value;
        }

        public bool CheckAddDamage(float value)
        {
            float result = _weight - value;
            return result >= 0;
        }
    }
}