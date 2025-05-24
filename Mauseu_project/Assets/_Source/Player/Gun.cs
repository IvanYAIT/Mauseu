using UnityEngine;

namespace Player
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject gun;

        public bool IsOwner;

        private bool isGunHidden;

        void Update()
        {
            if (!IsOwner)
                return;

            if (Input.GetMouseButtonDown(0) && !isGunHidden)
            {
                Shoot();
            }
        }

        public void Hide(bool value)
        {
            isGunHidden = value;
            gun.SetActive(!isGunHidden);
        }

        private void Shoot()
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }
    }
}