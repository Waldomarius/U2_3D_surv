using damage;

namespace env
{
    public class TreeComponent :  Damage
    {
        private void Update()
        {
            if (_weight <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}