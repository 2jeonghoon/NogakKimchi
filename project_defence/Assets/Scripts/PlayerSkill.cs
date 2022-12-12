using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    private Image instagram;
    [SerializeField]
    private Image toilet;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private PlayerHP playerHP;

    private bool isInstagramOn;
    private bool isToiletOn;

    public int instagramCoolTime;
    public int toiletCoolTime;

    private void Awake()
    {
        isInstagramOn = true;
        isToiletOn = true;
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
            isInstagramOn = true;
        else if (flag == 1)
            isToiletOn = true;
    }

    private IEnumerator Skill_Instagram()
    {
       

        float[] enemySpeed = new float[enemySpawner.EnemyList.Count];

        // enemy의 MoveSpeed를 받아와서 저장하고 0으로 세팅
        int idx = 0;
        foreach (Enemy enemy in enemySpawner.EnemyList)
        {
            enemySpeed[idx] = enemy.GetMoveSpeed();
            enemy.SetMoveSpeed(0);
            idx++;
        }

        yield return new WaitForSeconds(5);

        idx = 0;
        foreach (Enemy enemy in enemySpawner.EnemyList)
        {
            enemy.SetMoveSpeed(enemySpeed[idx]);
        }
    }

    private void Skill_Toilet()
    {
        playerHP.HealHP(1);
        Debug.Log(playerHP.CurrentHP);
    }


    public void OnSkill_Instagram()
    {
        if (isInstagramOn)
        {
            isInstagramOn = false;
            StartCoroutine(Skill_Instagram());
            StartCoroutine(CoolTime(instagram, instagramCoolTime, 0));
        }
    }
    public void OnSkill_Toilet()
    {
        if (isToiletOn && playerHP.CurrentHP != playerHP.MaxHP)
        {
            isToiletOn = false;
            Skill_Toilet();
            StartCoroutine(CoolTime(toilet, toiletCoolTime, 1));
        }
    }
}
