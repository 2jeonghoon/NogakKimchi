using UnityEngine;

public class Tile : MonoBehaviour
{
	private	SpriteRenderer	spriteRenderer;

	// 타일에 타워가 건설되어 있는지 검사하는 변수
	public bool IsBuildTower { set; get; }

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		OnColorReset();

		IsBuildTower = false;
	}

	public void OnSelectedTile()
	{
		// 현재 타일이 선택되었을 때 바뀌는 색상
		spriteRenderer.color = Color.blue;
	}

	public void OnColorReset()
	{
		// 원래 TileWall의 색상
		spriteRenderer.color = new Color(0, 0.69f, 0.31f);
	}
}


/*
 * File : Tile.cs
 * Desc
 *	: 타워 배치가 가능한 TileWall 오브젝트에 부착
 *	
 */