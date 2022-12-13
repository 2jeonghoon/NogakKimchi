using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PHASE { ONE = 1, TWO = 2, THREE = 3 }

public class Boss : Enemy
{
    // 스킬 딜레이시간
    [SerializeField]
    private float delay_time;
    // 복사되는 클론
    [SerializeField]
    private GameObject boss_clone;
    // 소환하는 enemy
    [SerializeField]
    private GameObject[] enemys;
    [SerializeField]
    private GameObject skillEffect;
    private Animator Bossanimator;

    // 웅크리기 지속시간
    [SerializeField]
    private float crouch_time;
    // 보스 상태
    private EnemyHP state;

    private PHASE phase = PHASE.ONE;

    public override void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        Bossanimator = GetComponent<Animator>();

        this.state = GetComponent<EnemyHP>();

        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;

        // 적 이동 경로 WayPoints 정보 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // 적의 위치를 첫번째 wayPoint 위치로 설정
        transform.position = wayPoints[currentIndex].position;

        // 적 이동/목표지점 설정 코루틴 함수 시작
        StartCoroutine("skill", delay_time);
        StartCoroutine("OnMove");
    }

    private IEnumerator skill(float delay_time)
    {
        float currentHPPercent = state.CurrentHP / state.MaxHP;


        // 체력 30퍼센트 이하 3페이즈
        if (currentHPPercent < 0.3f)
        {
            this.phase = PHASE.THREE;
        }
        // 체력 70퍼센트 이하 2페이즈
        else if (currentHPPercent < 0.7f)
        {
            Bossanimator.SetTrigger("Phase2");
            this.phase = PHASE.TWO;
        }

        //Debug.Log("보스 스킬!");
        // 1페이즈
        skillEffect.SetActive(true);
        
        skillEffect.GetComponent<EnemyDieEffect>().BossSkillEffect();
        if (phase == PHASE.ONE)
        {
            Bossanimator.SetTrigger("Phase1_skill");
            Debug.Log("웅크리기");
            //StartCoroutine("hallucination", delay_time);
            StartCoroutine("crouch", delay_time);
        }
        // 2페이즈
        else if (phase == PHASE.TWO)
        {
            Bossanimator.SetTrigger("Phase2_skill");
            Debug.Log("할루시네이션");
            StartCoroutine("hallucination", delay_time);
        }
        // 3페이즈
        else if (phase == PHASE.THREE)
        {
            Bossanimator.SetTrigger("Phase2_skill");
            Debug.Log("리콜");
            //StartCoroutine("hallucination", delay_time);
            StartCoroutine("recall", delay_time);
        }

        yield return new WaitForSeconds(delay_time);
        skillEffect.SetActive(false);
        StartCoroutine("skill", delay_time);
    }



    private IEnumerator crouch()
    {

        float speed = movement2D.MoveSpeed;
        float defense = state.getDefense();

        movement2D.MoveSpeed = 0;
        // 방어력 올려서 데미지0
        state.SetDefense(1000f);
        yield return new WaitForSeconds(crouch_time);
        Bossanimator.SetTrigger("Phase1_idle");
        state.SetDefense(defense);
        movement2D.MoveSpeed = speed;
    }


    private IEnumerator hallucination()
    {
        float speed = movement2D.MoveSpeed;
        float defense = state.getDefense();

        movement2D.MoveSpeed = 0;

        // 복사
        GameObject clone = Instantiate(boss_clone);
        Enemy enemy = clone.GetComponent<Enemy>();	// 방금 생성된 적의 Enemy 컴포넌트
        clone.GetComponent<Animator>().SetBool("isClone", true);
        // 생성된 클론 위치 세팅
        enemy.Setup(enemySpawner, this);      // 보스의 way데이터를 가지고 클론을 만듬.
        enemy.transform.position = this.transform.position;
        enemy.transform.rotation = this.transform.rotation;
        // HP 바 생성
        enemySpawner.SpawnEnemyHPSlider(clone);

        // 적 리스트에 추가
        enemySpawner.EnemyList.Add(enemy);

        // 소환된 클론 체력 깎기
        enemy.GetComponent<EnemyHP>().TakeDamage((this.GetComponent<EnemyHP>().MaxHP * 0.5f));
        enemy.GetComponent<EnemyHP>().SetDefense(GetComponent<EnemyHP>().getDefense() * 0.5f);


        yield return new WaitForSeconds(crouch_time);
        Bossanimator.SetTrigger("Phase2_idle");
        state.SetDefense(defense);
        movement2D.MoveSpeed = speed;
    }

    private IEnumerator recall()
    {
        float speed = movement2D.MoveSpeed;
        float defense = state.getDefense();

        movement2D.MoveSpeed = 0;

        // 복사
        for(int i = 0; i < enemys.Length; i++)
        {
            GameObject clone = Instantiate(enemys[i]);
            Enemy enemy = clone.GetComponent<Enemy>();  // 방금 생성된 적의 Enemy 컴포넌트된 클론 위치 세팅
            enemy.Setup(enemySpawner, this);      // 보스의 way데이터를 가지고 클론을 만듬.
            enemy.transform.position = this.transform.position;
            enemy.transform.rotation = this.transform.rotation;

            // HP 바 생성
            enemySpawner.SpawnEnemyHPSlider(clone);

            // 적 리스트에 추가
            enemySpawner.EnemyList.Add(enemy);
        }

        yield return new WaitForSeconds(crouch_time);
        Bossanimator.SetTrigger("Phase2_idle");
        state.SetDefense(defense);
        movement2D.MoveSpeed = speed;
    }

}
