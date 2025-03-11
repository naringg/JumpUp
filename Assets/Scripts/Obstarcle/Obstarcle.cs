using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damage = 10;
    public float damageRate = 1f;
    public float knockbackForce = 5f; // �˹� �� ����
    private List<IDamagable> things = new List<IDamagable>();
    public AudioClip DamageSound;   // ���� ȿ����
    private AudioSource audioSource;

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
        audioSource = GetComponent<AudioSource>();
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
            if (DamageSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(DamageSound);
            }
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
            knockbackDir.y = 0.2f; // ��¦ ���� Ƣ�� ȿ�� �߰�

            rb.velocity = new Vector3(0, rb.velocity.y, 0); // ���� X, Z �ӵ��� �ʱ�ȭ�Ͽ� ���� ����
            rb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);
        }
    }
}
