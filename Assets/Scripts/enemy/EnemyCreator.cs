using UnityEngine;

namespace enemy
{
    public class EnemyCreator : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        
        public GameObject CreateNewObject(GameObject prefab, Vector2 position, float moveSpeed, float damage)
        {
            float tempZPos = 5;
            Vector3 pos = new Vector3(position.x, tempZPos , position.y);
            
            RaycastHit hit;
            Ray downRay = new Ray(pos, -Vector3.up);
            
            if (Physics.Raycast(downRay, out hit, _groundLayer))
            {
                Debug.Log("Hit: hit.distance: " + hit.distance);
                pos = new Vector3(position.x, tempZPos - hit.distance , position.y);
            }
            
            GameObject newObj = Instantiate(prefab, pos, Quaternion.identity);

            EnemyController controller = newObj.GetComponent<EnemyController>();
            controller.SetStartPosition(pos);
            controller.SetMoveSpeed(moveSpeed);
            controller.SetDamage(damage);

            return newObj;
        }
    }
}