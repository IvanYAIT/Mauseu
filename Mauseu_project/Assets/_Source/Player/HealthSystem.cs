using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] int health;

    public bool IsOwner;

    public void GetDamage(int damage)
    {
        if (health > 0)
            health = damage;
        else 
            Death();
    }

    private void Death()
    {

    }
}
