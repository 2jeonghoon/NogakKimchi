using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Explosion : Projectile
{
    //���� ����
    [SerializeField]
    private float explosionRange;
    [SerializeField]
    private GameObject explosionPrefab;


    public void Setup(Transform target, float damage)
    {
        // �߻� ���� ���
        SoundManager.instance.SFXPlay("ExplosionShot", clip);
        Debug.Log("�߻�");
        movement2D = GetComponent<Movement2D>();
        this.damage = damage;                       // Ÿ���� ���ݷ�
        this.target = target;                       // Ÿ���� �������� target
    }

    private void Update()
    {
        if (target != null) // target�� �����ϸ�
        {
            // �߻�ü�� target�� ��ġ�� �̵�
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else                    // ���� ������ target�� �������
        {
            boom();                    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;         // ���� �ƴ� ���� �ε�����
        if (collision.transform != target) return;          // ���� target�� ���� �ƴ� ��

        boom();
    }

    // ����
    private void boom()
    {
        // ���� ���� ��� �ش� ��ġ���� ����
        GameObject clone = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        clone.GetComponent<Explosion>().Setup(damage, explosionRange);
        Destroy(gameObject);                                    // �߻�ü ������Ʈ ����
    }

}
