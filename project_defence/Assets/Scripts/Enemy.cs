using System.Collections;
using UnityEngine;

public enum EnemyDestroyType { Kill = 0, Arrive }


public class Enemy : MonoBehaviour
{
	protected	int				wayPointCount;      // 이동 경로 개수
	protected Transform[]		wayPoints;          // 이동 경로 정보
	protected int				currentIndex = 0;   // 현재 목표지점 인덱스
	protected Movement2D		movement2D;			// 오브젝트 이동 제어
	protected	EnemySpawner	enemySpawner;       // 적의 삭제를 본인이 하지 않고 EnemySpawner에 알려서 삭제
	public GameObject enemyHPSliderPrefab;
	[SerializeField]
	protected int				gold;           // 적 사망 시 획득 가능한 골드

	// 적 사망 사운드
	public AudioClip clip;

	// 클론 여부
	[SerializeField]
	public bool isClone;

	[SerializeField]
	public int enemyIndex;
	public bool isTarget;

	// 클론을 위한 셋업
	public virtual void Setup(EnemySpawner enemySpawner, Enemy enemy)
	{
		movement2D = GetComponent<Movement2D>();
		this.enemySpawner = enemySpawner;
		// 적 이동 경로 WayPoints 정보 설정
		wayPointCount = enemy.wayPoints.Length;
		this.wayPoints = new Transform[wayPointCount];
		this.wayPoints = enemy.wayPoints;
		this.currentIndex = enemy.currentIndex;
		if (currentIndex - 1 >= 0)
        {
			this.currentIndex = enemy.currentIndex - 1;
		}

		// 클론 위치를 복사한 객체 위치로.
		this.transform.position = enemy.transform.position;
		// 적 이동/목표지점 설정 코루틴 함수 시작
		StartCoroutine("OnMove");
	}

	public virtual void setupHPSlider(GameObject HPSliderPrefab)
    {
		this.enemyHPSliderPrefab = HPSliderPrefab;
	}

	public virtual void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
	{
		//Debug.Log("setUp");
		movement2D			= GetComponent<Movement2D>();
		this.enemySpawner	= enemySpawner;

		// 적 이동 경로 WayPoints 정보 설정
		wayPointCount		= wayPoints.Length;
		this.wayPoints		= new Transform[wayPointCount];
		this.wayPoints		= wayPoints;
		this.isTarget = true;

		currentIndex = 0;
		// 적의 위치를 첫번째 wayPoint 위치로 설정
		transform.position	= wayPoints[currentIndex].position;

		// 적 이동/목표지점 설정 코루틴 함수 시작
		StartCoroutine("OnMove");
	}

	protected virtual IEnumerator OnMove()
	{
		// 다음 이동 방향 설정
		NextMoveTo();
		while ( true )
		{
			// 적 오브젝트 회전
			//transform.Rotate(Vector3.forward * 10);

			// 적의 현재위치와 목표위치의 거리가 0.02 * movement2D.MoveSpeed보다 작을 때 if 조건문 실행
			// Tip. movement2D.MoveSpeed를 곱해주는 이유는 속도가 빠르면 한 프레임에 0.02보다 크게 움직이기 때문에
			// if 조건문에 걸리지 않고 경로를 탈주하는 오브젝트가 발생할 수 있다.
			if ( Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.1f * movement2D.MoveSpeed )
			{
				// 다음 이동 방향 설정
				NextMoveTo();
			}
			yield return null;
		}
	}

	protected void NextMoveTo()
	{
		// 아직 이동할 wayPoints가 남아있다면
		if ( currentIndex < wayPointCount - 1 )
		{
			// 적의 위치를 정확하게 목표 위치로 설정
			transform.position = wayPoints[currentIndex].position;
			
			// 이동 방향 설정 => 다음 목표지점(wayPoints)
			currentIndex ++;
			Vector3 direction = (wayPoints[currentIndex].position-transform.position).normalized;

            if (wayPoints[currentIndex].position.x < transform.position.x)
            {
				transform.rotation = Quaternion.Euler(0, 0, 180);
				transform.GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
				transform.rotation = Quaternion.Euler(0, 0, 0);
				transform.GetComponent<SpriteRenderer>().flipY = false;
			}
			
			movement2D.MoveTo(direction);
		}
		// 현재 위치가 마지막 wayPoints이면
		else
		{
			// 목표지점에 도달해서 사망할 때는 돈을 주지 않도록
			gold = 0;
			// 적 오브젝트 삭제
			OnDie(EnemyDestroyType.Arrive);
		}
	}

	public void OnDie(EnemyDestroyType type)
	{

		// 적 사망 사운드 재생
		SoundManager.instance.SFXPlay("EnemyDie", clip);
		GetComponent<EnemyHP>().setSpawnHP();

		// Enemy
		//
		//
		// er에서 리스트로 적 정보를 관리하기 때문에 Destroy()를 직접하지 않고
		// EnemySpawner에게 본인이 삭제될 때 필요한 처리를 하도록 DestroyEnemy() 함수 호출
		enemySpawner.DestroyEnemy(type, this, gold);
	}

    public float GetMoveSpeed()
    {
        return movement2D.MoveSpeed;
    }

    public void SetMoveSpeed(float speed)
    {
        movement2D.MoveSpeed = speed;
    }
}


/*
 * File : Enemy.cs
 * Desc
 *	: 지정된 경로(wayPoints)를 따라 이동하는 적
 *	
 * Functions
 *	: Setup() - 초기화 함수
 *	: OnMove() - 경로를 따라 적 이동
 *	: NextMoveTo() - 다음 목표 위치 설정 or 마지막 위치이면 적 삭제
 *	: OnDie() - 적 사망 시 호출. EnemySpawner에게 본인을 삭제하도록 요청
 *	: GetMoveSpeed() - protected movement2D.MoveSpeed 값을 return해주는 public method
 *	: SetMoveSpeed() - protected movement2D.MoveSpeed 값을 Set해주는 public method
 */