using UnityEngine;

namespace TradeMarket.Characrer
{
    public class InteractionComponent : MonoBehaviour
    {
        [SerializeField] private Transform _head;
        [SerializeField] private float _distanceToInterract;
        [SerializeField] private LayerMask _interactionLayer;
        
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E))
                return;

            if (!Physics.Raycast(_head.position, _head.forward, out var hit, _distanceToInterract, _interactionLayer))
                return;

            var interactable = hit.transform.gameObject.GetComponent<InteractableItemComponentBase>();

            if (interactable == null)
                return;

            interactable.Interract();
        }
    }
}