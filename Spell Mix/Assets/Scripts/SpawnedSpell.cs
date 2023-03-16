using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SpawnedSpell : MonoBehaviour
{
    public string type;
    public int dmg;
    public float speed;
    public float radius;

    public GameObject hitParticle;

    public void Init(Spell spell)
    {
        type = spell.type;
        dmg = spell.dmg;
        speed = spell.speed;
        radius = spell.radius;

        hitParticle = spell.hitParticle;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] voxelsColliders = Physics.OverlapSphere(transform.position, radius);

        Player player = FindObjectOfType<Player>();
        player.isShooting = false;

        foreach(Collider nearbyObject in voxelsColliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(1500 * dmg/2, transform.position, radius);
            }
        }
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(dmg);
        }
    }
}
