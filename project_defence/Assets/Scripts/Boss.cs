using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PHASE { ONE = 1, TWO = 2, THREE = 3 }

public class Boss : Enemy
{
    // ��ų �����̽ð�
    [SerializeField]
    private float delay_time;
    // ����Ǵ� Ŭ��
    [SerializeField]
    private GameObject boss_clone;

    // ��ũ���� ���ӽð�
    private float crouch_time = 0.5f;
    // ���� ����
    private EnemyHP state;

    private PHASE phase = PHASE.ONE;

    public override void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        this.state = GetComponent<EnemyHP>();

        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;

        // �� �̵� ��� WayPoints ���� ����
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // ���� ��ġ�� ù��° wayPoint ��ġ�� ����
        transform.position = wayPoints[currentIndex].position;

        // �� �̵�/��ǥ���� ���� �ڷ�ƾ �Լ� ����
        StartCoroutine("skill", delay_time);
        StartCoroutine("OnMove");
    }

    private IEnumerator skill(float delay_time)
    {
        float currentHPPercent = state.CurrentHP / state.MaxHP;
        // ü�� 30�ۼ�Ʈ ���� 3������
        if (currentHPPercent < 0.3f)
        {
            this.phase = PHASE.THREE;
        }
        // ü�� 70�ۼ�Ʈ ���� 2������
        else if(currentHPPercent < 0.7f)
        {
            this.phase = PHASE.TWO;
        }

        Debug.Log("���� ��ų!");
        // 1������
        if (phase == PHASE.ONE)
        {
            Debug.Log("��ũ����");
            StartCoroutine("crouch", delay_time);
        }
        // 2������
        else if (phase == PHASE.TWO)
        {
            Debug.Log("�ҷ�ó��̼�");
            StartCoroutine("hallucination", delay_time);
        }
        // 3������
        else if (phase == PHASE.THREE)
        {
            Debug.Log("����");
            recall();
        }
        yield return new WaitForSeconds(delay_time);
        StartCoroutine("skill", delay_time);
    }



    private IEnumerator crouch()
    {
        float speed = movement2D.MoveSpeed;
        float defense = state.getDefense();

        movement2D.MoveSpeed = 0;
        // ���� �÷��� ������0
        state.SetDefense(1000f);
        yield return new WaitForSeconds(crouch_time);
        state.SetDefense(defense);
        movement2D.MoveSpeed = speed;
    }


    private IEnumerator hallucination()
    {
        float speed = movement2D.MoveSpeed;
        float defense = state.getDefense();

        movement2D.MoveSpeed = 0;

        // ����
        GameObject clone = Instantiate(boss_clone);
        Enemy enemy = clone.GetComponent<Enemy>();	// ��� ������ ���� Enemy ������Ʈ
        // ������ Ŭ�� ��ġ ����
        enemy.Setup(enemySpawner, this);      // ������ way�����͸� ������ Ŭ���� ����.
        enemy.transform.position = this.transform.position;
        enemy.transform.rotation = this.transform.rotation;
        // HP �� ����
        enemySpawner.SpawnEnemyHPSlider(clone);

        enemySpawner.EnemyList.Add(enemy);
        enemy.GetComponent<EnemyHP>().TakeDamage((this.GetComponent<EnemyHP>().MaxHP - this.GetComponent<EnemyHP>().CurrentHP)*1.5f);

        yield return new WaitForSeconds(crouch_time);
        state.SetDefense(defense);
        movement2D.MoveSpeed = speed;
    }


    private void recall()
    {

    }
}
