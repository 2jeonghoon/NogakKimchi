using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{   
    [SerializeField]
    private EnemySpawner enemySpawner;

    private IEnumerator Skill_Instagram()
    {
       

        float[] enemySpeed = new float[enemySpawner.EnemyList.Count];

        // enemy�� MoveSpeed�� �޾ƿͼ� �����ϰ� 0���� ����
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

    public void OnSkill_Instagram()
    {
        StartCoroutine(Skill_Instagram());
    }

}
