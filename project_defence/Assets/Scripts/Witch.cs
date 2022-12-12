using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy
{
    // ��ų ��Ÿ��
    [SerializeField]
    private float delay_time;
    // ȸ����
    [SerializeField]
    private float recovery_amount;
    // ��Ÿ�
    [SerializeField]
    private float range;
    // ���� ��ġ
    private Transform position;
    // ��Ÿ� �� enemy������Ʈ 
    Enemy[] enemy;

    public override void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;

        // �� �̵� ��� WayPoints ���� ����
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // ���� ��ġ�� ù��° wayPoint ��ġ�� ����
        transform.position = wayPoints[currentIndex].position;

        // �� �̵�/��ǥ���� ���� �ڷ�ƾ �Լ� ����
        StartCoroutine("Recovery", delay_time);
        StartCoroutine("OnMove");
    }


    protected override IEnumerator OnMove()
    {
        // ���� �̵� ���� ����
        NextMoveTo();

        while (true)
        {
            // �� ������Ʈ ȸ��
            //transform.Rotate(Vector3.forward * 10);

            // ���� ������ġ�� ��ǥ��ġ�� �Ÿ��� 0.02 * movement2D.MoveSpeed���� ���� �� if ���ǹ� ����
            // Tip. movement2D.MoveSpeed�� �����ִ� ������ �ӵ��� ������ �� �����ӿ� 0.02���� ũ�� �����̱� ������
            // if ���ǹ��� �ɸ��� �ʰ� ��θ� Ż���ϴ� ������Ʈ�� �߻��� �� �ִ�.
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                // ���� �̵� ���� ����
                NextMoveTo();
            }

            yield return null;
        }
    }

    // ��Ÿ� �� enemy������Ʈ ��������
    private int getEnemyHP()
    {
        int count = 0;
        int enemy_count = enemySpawner.EnemyList.Count;
        enemy = new Enemy[enemy_count];
        List<Enemy> enemies = enemySpawner.EnemyList;
        for (int i = 0; i < enemy_count; ++i)
        {
            float distance = Vector3.Distance(enemies[i].transform.position, transform.position);
            // �ڱ��ڽ��� �����ϰ�
            if (distance < range && this != enemies[i])
            {
                //Debug.Log(count + " : " + enemies[i]);
                enemy[count++] = enemies[i];
            }
        }
        return count;
    }

    // ȸ�� �Լ�
    private IEnumerator Recovery(float delay_time)
    {
        //Debug.Log("��!");
        int count = getEnemyHP();
        for (int i = 0; i < count; ++i)
        {
            enemy[i].GetComponent<EnemyHP>().TakeRecovery(recovery_amount);
        }
        yield return new WaitForSeconds(delay_time);
        StartCoroutine("Recovery", delay_time);
    }

}
