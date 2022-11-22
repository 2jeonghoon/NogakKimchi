using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSystem : MonoBehaviour
{
	[SerializeField]
	private	WaveEnemy[]			waves;					// 현재 스테이지의 모든 웨이브 정보
	[SerializeField]
	private	EnemySpawner	enemySpawner;
	private	int				currentWaveIndex = -1;  // 현재 웨이브 인덱스
	[SerializeField]
	private TextMeshProUGUI textGameSpeed; // 배속
	bool isfause = false;
	float gameSpeed= 1;


	static public int spawnEnemyCount; // 스폰한 몬스터 숫자

	// 웨이브 정보 출력을 위한 Get 프로퍼티 (현재 웨이브, 총 웨이브)
	public	int				CurrentWave => currentWaveIndex+1;		// 시작이 0이기 때문에 +1
	public	int				MaxWave => waves.Length;

	public void StartWave()
	{
		// 현재 맵에 적이 없고, Wave가 남아있으면
		if ( enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length-1 )
		{
			Debug.Log("wave 시작");
			// 인덱스의 시작이 -1이기 때문에 웨이브 인덱스 증가를 제일 먼저 함
			currentWaveIndex ++;
			// EnemySpawner의 StartWave() 함수 호출. 현재 웨이브 정보 제공
			Debug.Log(waves[currentWaveIndex].wave.Length);
			StartCoroutine("WaveSpawnEnemy");
		}
	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StopCoroutine("WaveSpawnEnemy");
		}
	}

    private IEnumerator WaveSpawnEnemy()
    {
		int wave_enemy_amount = 0 ;
		for(int i = 0; i < waves[currentWaveIndex].wave.Length; i++)
        {
			wave_enemy_amount += waves[currentWaveIndex].wave[i].maxEnemyCount;

		}
		waves[currentWaveIndex].maxEnemyCount = wave_enemy_amount;
		spawnEnemyCount = 0;
		enemySpawner.StartWave(waves[currentWaveIndex]);
		// 현재 wave에서 소환이 다 끝날 때까지 기다리기
		while (waves[currentWaveIndex].maxEnemyCount > spawnEnemyCount)
        {
			yield return new WaitForSeconds(0.5f);
		}
	}

	public void SpeedChange() {

        if (!isfause)
        {
			if (Time.timeScale < 2)
			{
				Time.timeScale *= 2f;
			}
			else if (Time.timeScale >= 2)
			{
				Time.timeScale = 0.5f;
			}
			gameSpeed = Time.timeScale;
			textGameSpeed.text = "x" + Time.timeScale.ToString();
		}
	}

	public void GamePause()
    {
        if (!isfause)
        {
			Time.timeScale = 0f;
			isfause = true;
		}
        else
        {
			Debug.Log("gameSpeed : " + gameSpeed);
			Time.timeScale = gameSpeed;
			isfause = false;
		}
    }
}

[System.Serializable]
public struct WaveEnemy
{
	public Wave[] wave;
	public int maxEnemyCount; // 현재 웨이브 적 등장 숫자
}

[System.Serializable]
public struct Wave
{
    public  float        spawnTime;     // 현재 웨이브 적 생성 주기
    public  int          maxEnemyCount; // 현재 웨이브 적 등장 숫자
    public  GameObject[] enemyPrefabs;  // 현재 웨이브 적 등장 종류
}


/*
 * File : WaveSystem.cs
 * Desc
 *	: 웨이브 시스템 정보
 *
 */