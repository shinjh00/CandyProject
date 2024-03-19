using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<Sprite> candyList = new List<Sprite>();  // 배치할 스프라이트 목록
    public GameObject candyPrefab;  // 타일을 생성하기 위한 스텐실. 스프라이트를 할당하여 개인화됨
    public int gridDimension;  // 그리드의 크기
    public float candyDistance;  // 타일의 거리

    private GameObject[,] grid;  // 2차원 배열의 Grid

    public static GridManager instance { get; private set; }  // 싱글톤 정적 변수 선언

    private void Awake()
    {
        instance = this;  // 싱글톤
    }

    private void Start()
    {
        grid = new GameObject[gridDimension, gridDimension];
        InitGrid();
    }


    /* 그리드 초기화 */

    private void InitGrid()
    {
        float center = (gridDimension * candyDistance / 2.0f) - (candyDistance / 2.0f);
        Vector3 gridCenter = new Vector3(center, center, 0);  // 그리드의 정중앙
        Vector3 positionOffset = transform.position - gridCenter;  // 그리드가 GameObject의 중앙에 오도록 오프셋을 계산

        for (int row = 0; row < gridDimension; row++)
        {
            for (int column = 0; column < gridDimension; column++)  // 2차원 배열
            {
                /* 셀에 배치할 스프라이트 선택 (중복 제외 기능) */
                List<Sprite> availCandySprites = new List<Sprite>(candyList);  // 스프라이트 목록을 복사해서 담은 새로운 List 인스턴스

                Sprite left1 = GetSpriteAt(column - 1, row);  // 1칸 왼쪽 셀
                Sprite left2 = GetSpriteAt(column - 2, row);  // 2칸 왼쪽 셀
                if (left2 != null && left1 == left2)  // 왼쪽 두 개의 셀 유효한지 && 두 셀 스프라이트 같은지
                {
                    availCandySprites.Remove(left1);  // 해당 스프라이트 제거
                }
                Sprite down1 = GetSpriteAt(column, row - 1);
                Sprite down2 = GetSpriteAt(column, row - 2);
                if (down2 != null && down1 == down2)
                {
                    availCandySprites.Remove(down1);
                }

                /* 셀에 스프라이트 랜덤 배치 */
                GameObject newCandy = Instantiate(candyPrefab);  // 셀 프리팹 생성
                SpriteRenderer renderer = newCandy.GetComponent<SpriteRenderer>();  // SpriteRenderer를 이용해 셀의 스프라이트를 설정할 것임
                renderer.sprite = availCandySprites[Random.Range(0, availCandySprites.Count)];  // 사용 가능 스프라이트 목록에서 랜덤으로 할당

                //Tile tile = newTile.AddComponent<Tile>();  // 생성된 Tile 프리팹에 Tile 컴포넌트 추가
                //tile.position = new Vector2Int(column, row);  // 그리드에서 셀의 위치

                newCandy.transform.parent = transform;  // 모든 셀은 그리드가 부모임
                newCandy.transform.position = new Vector3(column * candyDistance, row * candyDistance, 0) + positionOffset;  // 셀의 위치 지정
                grid[column, row] = newCandy;  // 생성된 셀에 대한 참조를 저장
            }
        }
    }


    /* 중복 스프라이트 확인 */

    private Sprite GetSpriteAt(int column, int row)
    {
        // 그리드를 벗어나는 셀은
        if (column < 0 || column >= gridDimension || row < 0 || row >= gridDimension)
            return null;  // 고려하지 않음

        // 그렇지 않으면
        GameObject tile = grid[column, row];  // 해당 셀 가져와서
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
        return renderer.sprite;  // 스프라이트 확인하고 반환
    }


    /* 인접한 두 셀 교환 */

    public void SwapTiles(Vector2Int targetPosition, Vector2Int selectedPosition)  // 두 타일의 위치
    {
        // 두 셀의 스프라이트 렌더러 가져오기
        GameObject targetTile = grid[targetPosition.x, targetPosition.y];
        SpriteRenderer targetRender = targetTile.GetComponent<SpriteRenderer>();

        GameObject selectedTile = grid[selectedPosition.x, selectedPosition.y];
        SpriteRenderer selectedRender = selectedTile.GetComponent<SpriteRenderer>();

        // Swap
        Sprite temp = targetRender.sprite;
        targetRender.sprite = selectedRender.sprite;
        selectedRender.sprite = temp;
    }

    private void Update()
    {

    }
}
