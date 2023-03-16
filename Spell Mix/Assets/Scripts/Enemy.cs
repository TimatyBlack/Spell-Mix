using System.Collections;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
    [SerializeField] private Collider mainCollider;
    [SerializeField] private Rigidbody[] voxelsRb;
    [SerializeField] private Healthbar healthbar;

    public int health, maxHealth = 10;

    void Start()
    {
        health = maxHealth;
        mainCollider = GetComponent<Collider>();
        voxelsRb = GetComponentsInChildren<Rigidbody>();

        healthbar.UpdateHealthBar(maxHealth, health);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthbar.UpdateHealthBar(maxHealth, health);

        if (health <= 0)
        {
            for(int i = 0; i < voxelsRb.Length; i++)
            {   
                voxelsRb[i].isKinematic = false;
                voxelsRb[i].AddExplosionForce(700, transform.position, 5f);
                StartCoroutine(destroyDelay(voxelsRb[i].gameObject));
            }

            Destroy(healthbar);
        }
    }

    IEnumerator destroyDelay(GameObject shatter)
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

        shatter.transform.DOScale(new Vector3(0, 0, 0), 0.5f)
            .SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(2f);

        Destroy(shatter);
    }

}
