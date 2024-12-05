using EnemyAI;
using UnityEngine;

namespace PlayerInventory
{
    public class PickUpSystem : MonoBehaviour
    {
        [SerializeField] private Transform head;
        [SerializeField] private float distanceToPickUp;
        [SerializeField] private LayerMask itemLayerMask;
        [SerializeField] private LayerMask monsterLayerMask;
        [SerializeField] private Inventory inventory;

        private Item _item;


        void Update()
        {
            if (Input.GetKey(KeyCode.E))
            {
                RaycastHit hit;
                if (Physics.Raycast(head.position, head.forward, out hit, distanceToPickUp, itemLayerMask))
                {
                    _item = hit.transform.gameObject.GetComponent<Item>();
                    if (inventory.AddItem(_item))
                        _item.gameObject.SetActive(false);
                }

                if (Physics.Raycast(head.position, head.forward, out hit, distanceToPickUp, monsterLayerMask))
                {
                    Enemy monster = hit.transform.gameObject.GetComponent<Enemy>();
                    if (monster.IsCatched)
                        if (inventory.TakeMonster(monster.GetData()))
                            monster.gameObject.SetActive(false);
                }
            }
            
        }
    }
}