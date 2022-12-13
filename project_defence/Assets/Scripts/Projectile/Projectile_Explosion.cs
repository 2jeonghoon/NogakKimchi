using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Explosion : Projectile
{
    //���� ����
    private float explosionRange;
    [SerializeField]
    private GameObject explosionPrefab;

    private int pool_idx = 2;

    // ��Ÿ��� ����� �������� ��Ÿ� ��������
    float range;
    float move_distance;
    Vector3 direction;
    Vector3 start_position;


    public void Setup(Transform target, float damage, float range, float explosionRange)
    {
        // �߻� ���� ���
        //SoundManager.instance.SFXPlay("ExplosionShot", clip);
        //Debug.Log("�߻�");
        movement2D = GetComponent<Movement2D>();
        this.damage = damage;                       // Ÿ���� ���ݷ�
        this.target = target;                       // Ÿ���� �������� target
        this.range = range;                         // Ÿ���� �������� range
        this.explosionRange = explosionRange;
        start_position = transform.position;
        direction = (target.position - transform.position).normalized;
        gameObject.SetActive(true);					// ObjectPool�� ����ϸ鼭 SetActive(true)�� �ʿ�����
    }

    private void Update()
    {
        move_distance = (transform.position - start_position).magnitude;
        if(move_distance > range)
        {
            boom();
        }
        if (target != null) // target�� �����ϸ�
        {
            // �߻�ü�� target�� ��ġ�� �̵�
            movement2D.MoveTo(direction);
        }
        else                    // ���� ������ target�� �������
        {
            //boom();                    
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
        ProjectileReturn(pool_idx);
    }

}
