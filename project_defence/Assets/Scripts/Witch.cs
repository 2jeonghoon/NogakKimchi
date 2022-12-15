using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy
{
    // 스킬 쿨타임
    [SerializeField]
    private float delay_time;
    // 회복량
    [SerializeField]
    private float recovery_amount;
    // 사거리
    [SerializeField]
    private float range;
    // 현재 위치
    private Transform position;
    // 사거리 내 enemy오브젝트 
    Enemy[] enemy;

    public override void Setup(EnemySpawner enemySpawner, Transform[] wayPoints, int pool_idx)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;

        // 적 이동 경로 WayPoints 정보 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;
        this.pool_idx = pool_idx;
        // 적의 위치를 첫번째 wayPoint 위치로 설정
        transform.position = wayPoints[currentIndex].position;
        gameObject.SetActive(true);					// ObjectPool을 사용하면서 SetActive(true)가 필요해짐
        // 적 이동/목표지점 설정 코루틴 함수 시작
        StartCoroutine("Recovery", delay_time);
        NextMoveTo();
    }

    /*
    protected override IEnumerator OnMove()
    {
        // 다음 이동 방향 설정
        NextMoveTo();
        while (true)
        {
            // 적 오브젝트 회전
            //transform.Rotate(Vector3.forward * 10);
            // 적의 현재위치와 목표위치의 거리가 0.02 * movement2D.MoveSpeed보다 작을 때 if 조건문 실행
            // Tip. movement2D.MoveSpeed를 곱해주는 이유는 속도가 빠르면 한 프레임에 0.02보다 크게 움직이기 때문에
            // if 조건문에 걸리지 않고 경로를 탈주하는 오브젝트가 발생할 수 있다.
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                // 다음 이동 방향 설정
                NextMoveTo();
            }
            yield return null;
        }
    }
    */
    // 사거리 내 enemy오브젝트 가져오기
    private int getEnemyHP()
    {
        int count = 0;
        int enemy_count = enemySpawner.EnemyList.Count;
        enemy = new Enemy[enemy_count];
        List<Enemy> enemies = enemySpawner.EnemyList;
        for (int i = 0; i < enemy_count; ++i)
        {
            float distance = Vector3.Distance(enemies[i].transform.position, transform.position);
            // 자기자신을 제외하고
            if (distance < range && this != enemies[i])
            {
                //Debug.Log(count + " : " + enemies[i]);
                enemy[count++] = enemies[i];
            }
        }
        return count;
    }

    // 회복 함수
    private IEnumerator Recovery(float delay_time)
    {
        //Debug.Log("힐!");
        int count = getEnemyHP();
        for (int i = 0; i < count; ++i)
        {
            enemy[i].GetComponent<EnemyHP>().TakeRecovery(recovery_amount);
        }
        yield return new WaitForSeconds(delay_time);
        StartCoroutine("Recovery", delay_time);
    }

}