using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damage = 10;
    public float damageRate = 1f;
    public float knockbackForce = 5f; // 넉백 힘 조절
    private List<IDamagable> things = new List<IDamagable>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
            ApplyKnockback(things[i] as MonoBehaviour); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            things.Add(damagable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            things.Remove(damagable);
        }
    }

    private void ApplyKnockback(MonoBehaviour target)
    {
        if (target != null && target.TryGetComponent(out Rigidbody rb))
        {
            Vector3 knockbackDir = (target.transform.position - transform.position).normalized;
            knockbackDir.y = 0.2f; // 살짝 위로 튀는 효과 추가

            rb.velocity = new Vector3(0, rb.velocity.y, 0); // 기존 X, Z 속도를 초기화하여 방향 조정
            rb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);
        }
    }
}
