using UnityEngine;

public class Movement2D : MonoBehaviour
{
	[SerializeField]
	private	float	moveSpeed = 0.0f;
	[SerializeField]
	private	Vector3	moveDirection = Vector3.zero;
	private	float	baseMoveSpeed;

	// moveSpeed 변수의 프로퍼티(Property) (Get 가능)
	public float MoveSpeed
	{
		set => moveSpeed = Mathf.Max(0, value);
		get => moveSpeed;
	}

	private void Awake()
	{
		baseMoveSpeed = moveSpeed;
	}

	private void Update()
	{
		transform.position += moveDirection * moveSpeed * Time.deltaTime;
	}

	public void MoveTo(Vector3 direction)
	{
		moveDirection = direction;
	}

	public void ResetMoveSpeed()
	{
		moveSpeed = baseMoveSpeed;
	}
}


/*
 * File : Movement2D.cs
 * Desc
 *	: 이동이 가능한 모든 오브젝트에게 부착
 *	
 * Functions
 *	: MoveTo() - 외부에서 호출해 이동 방향을 설정
 *	
 */