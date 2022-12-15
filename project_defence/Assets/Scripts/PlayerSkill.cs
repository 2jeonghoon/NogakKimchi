using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField]
    private Image instagram;
    [SerializeField]
    private Image toilet;
    [SerializeField]
    private Image friendChance;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private PlayerHP playerHP;

    private bool isInstagramCanUse;
    private bool isToiletCanUse;
    private bool isFriendChanceCanUse;

    public int instagramCoolTime;
    public int toiletCoolTime;
    public int friendChanceCoolTime;

    [SerializeField]
    private int deleteEnemyNum_friendChance;

    private bool isInstagramOn;
    public bool IsInstagramOn => isInstagramOn;

    private void Awake()
    {
        isInstagramCanUse = true;
        isToiletCanUse = true;
        isFriendChanceCanUse = true;
        isInstagramOn = false;
    }

    private IEnumerator CoolTime (Image image, float coolTime, int flag)
    {
        image.fillAmount = 0;
        while(image.fillAmount < 1)
        {
            image.fillAmount += 1 / coolTime * Time.deltaTime;

            yield return null;
        }

        if (flag == 0)
            isInstagramCanUse = true;
        else if (flag == 1)
            isToiletCanUse = true;
        else
            isFriendChanceCanUse = true;
    }

    private IEnumerator Skill_Instagram()
    {
        float[] enemySpeed = new float[enemySpawner.EnemyList.Count];

        // enemy의 MoveSpeed를 받아와서 저장하고 0으로 세팅
        int idx = 0;
        foreach (Enemy enemy in enemySpawner.EnemyList)
        {
            enemy.isInstagramOn = true;
            enemySpeed[idx] = enemy.GetBaseMoveSpeed();
            enemy.SetMoveSpeed(0);
            idx++;
        }

        yield return new WaitForSeconds(5);

        idx = 0;
        foreach (Enemy enemy in enemySpawner.EnemyList)
        {
            enemy.SetMoveSpeed(enemySpeed[idx]);
            enemy.isInstagramOn = false;
        }
    }

    private void Skill_Toilet()
    {
        playerHP.HealHP(1);
        Debug.Log(playerHP.CurrentHP);
    }

    // 맵 안 몬스터 3마리 랜덤으로 삭제
    private void Skill_FriendChance()
    {
        
        Random.InitState((int)(Time.time * 100f));                  // Randome Seed 초기화
        int maxEnemyIndex = enemySpawner.EnemyList.Count - 1;           // 리스트의 길이

        for (int i = 0; i < deleteEnemyNum_friendChance; i++)
        {
            try
            {
                Enemy enemy = enemySpawner.EnemyList[Random.Range(0, maxEnemyIndex)];
                enemySpawner.DestroyEnemy(EnemyDestroyType.Kill, enemy, 0);
                maxEnemyIndex--;
            }
            catch
            {
                Debug.Log("몬스터 없음");
            }
        }
        
    }

    public void OnSkill_Instagram()
    {
        if (isInstagramCanUse)
        {
            isInstagramCanUse = false;
            StartCoroutine(Skill_Instagram());
            StartCoroutine(CoolTime(instagram, instagramCoolTime, 0));
        }
    }
    public void OnSkill_Toilet()
    {
        if (isToiletCanUse && playerHP.CurrentHP != playerHP.MaxHP)
        {
            isToiletCanUse = false;
            Skill_Toilet();
            StartCoroutine(CoolTime(toilet, toiletCoolTime, 1));
        }
    }

    public void OnSkill_FriendChance()
    {
        if (isFriendChanceCanUse && enemySpawner.EnemyList.Count > 0)
        {
            isFriendChanceCanUse = false;
            Skill_FriendChance();
            StartCoroutine(CoolTime(friendChance, friendChanceCoolTime, 2));
        }
    }
}
