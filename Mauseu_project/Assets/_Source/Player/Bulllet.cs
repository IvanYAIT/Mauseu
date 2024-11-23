using EnemyAI;
using System.Collections;
using UnityEngine;

public class Bulllet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask enemyLayerMask;

    private int _enemyLayer;
    private void Awake()
    {
        _enemyLayer = (int)Mathf.Log(enemyLayerMask.value, 2);
        rb.AddForce(speed * transform.up, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == _enemyLayer)
        {
            collision.gameObject.GetComponent<Enemy>().IsCateched();
        }
    }

    private IEnumerator LifetTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(this);
    }
}
