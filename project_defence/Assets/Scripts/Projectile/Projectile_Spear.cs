using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Spear : Projectile
{
	private float range;
	private Vector3 direction;		// ���� ����

	private Vector3 start_pos;	// ���� ��ġ
	private Vector3 cur_pos;		// ���� ��ġ


	public void Setup(Transform target, float damage, float range)
	{
		// �߻� ���� ���
		SoundManager.instance.SFXPlay("Spear", clip);
		movement2D = GetComponent<Movement2D>();
		this.target = target;                       // Ÿ���� �������� target
		this.damage = damage;                       // Ÿ���� ���ݷ�
		this.range = range;                         // Ÿ�� ��Ÿ�

		direction = (target.position - transform.position).normalized;
		start_pos = this.transform.position;
	}

	private void Update()
	{
		cur_pos = this.transform.position;

		// �߻�ü�� target�� ��ġ�� �̵�
		movement2D.MoveTo(direction);

		// ��Ÿ��� ������� ����
		if (Vector3.Distance(start_pos, cur_pos) >= range)
        {
			Destroy(gameObject);
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("�浹");
		if (!collision.CompareTag("Enemy")) return;         // ���� �ƴ� ���� �ε�����
		collision.GetComponent<EnemyHP>().TakeDamage(damage);   // �� ü���� damage��ŭ ����
	}
}
