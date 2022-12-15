using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;          // 최대 체력
    [SerializeField]
    private float currentHP;      // 현재 체력
    private bool isDie = false;  // 적이 사망 상태이면 isDie를 true로 설정
    [SerializeField]
    private float defense;        // 방어력
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;



    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;            // 현재 체력을 최대 체력과 같게 설정
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void setSpawnHP()
    {
        currentHP = maxHP;
    }

    public float getDefense()
    {
        return defense;
    }
    // 방어력 세팅
    public void SetDefense(float defense)
    {
        this.defense = defense;
    }


    // 회복
    public void TakeRecovery(float heal)
    {
        if (isDie == true) return;

        // 체력이 최대 체력을 넘어가지 않도록
        if (currentHP + heal >= maxHP)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += heal;
        }
    }

    public void TakeDamage(float damage)
    {
        // Tip. 적의 체력이 damage 만큼 감소해서 죽을 상황일 때 여러 타워의 공격을 동시에 받으면
        // enemy.OnDie() 함수가 여러 번 실행될 수 있다.

        // 현재 적의 상태가 사망 상태이면 아래 코드를 실행하지 않는다.
        if (isDie == true) return;

        // 현재 체력을 damage - defense(방어력)만큼 감소
        if (damage - defense >= 0)
        {
            currentHP -= (damage - defense);
        }

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        // 체력이 0이하 = 적 캐릭터 사망
        if (currentHP <= 0)
        {
            //isDie = true;
            // 적 캐릭터 사망
            //currentHP = maxHP;

            // 현재 적의 색상을 color 변수에 저장
            Color color = spriteRenderer.color;

            // 적의 투명도를 0%로 설정
            color.a = 1f;
            spriteRenderer.color = color;

            enemy.OnDie(EnemyDestroyType.Kill);
        }
    }

    //레이저 공격 방어력 무시
    public void TakeLaserDamage(float damage)
    {
        // Tip. 적의 체력이 damage 만큼 감소해서 죽을 상황일 때 여러 타워의 공격을 동시에 받으면
        // enemy.OnDie() 함수가 여러 번 실행될 수 있다.

        // 현재 적의 상태가 사망 상태이면 아래 코드를 실행하지 않는다.
        if (isDie == true) return;
        Debug.Log("레이저 " + damage);
        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");
        currentHP -= damage;
        // 체력이 0이하 = 적 캐릭터 사망
        if (currentHP <= 0)
        {
            //isDie = true;
            // 적 캐릭터 사망
            // 현재 적의 색상을 color 변수에 저장
            Color color = spriteRenderer.color;

            // 적의 투명도를 0%로 설정
            color.a = 1f;
            spriteRenderer.color = color;

            enemy.OnDie(EnemyDestroyType.Kill);
        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        if (gameObject.activeSelf)
        {
            // 현재 적의 색상을 color 변수에 저장
            Color color = spriteRenderer.color;

            // 적의 투명도를 40%로 설정
            color.a = 0.4f;
            spriteRenderer.color = color;

            // 0.05초 동안 대기
            yield return new WaitForSeconds(0.05f);

            // 적의 투명도를 100%로 설정
            color.a = 1.0f;
            spriteRenderer.color = color;
        }
        yield return new WaitForSeconds(0);
    }
}


/*
 * File : EnemyHP.cs
 * Desc
 *	: 적 캐릭터의 체력
 *	
 * Functions
 *	: TakeDamage() - 체력 감소
 *	: HitAlphaAnimation() - 투명도를 100% -> 40% -> 100%로 설정
 */