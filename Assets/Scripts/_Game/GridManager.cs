using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<Sprite> candyList = new List<Sprite>();  // ��ġ�� ��������Ʈ ���
    public GameObject candyPrefab;  // Ÿ���� �����ϱ� ���� ���ٽ�. ��������Ʈ�� �Ҵ��Ͽ� ����ȭ��
    public int gridDimension;  // �׸����� ũ��
    public float candyDistance;  // Ÿ���� �Ÿ�

    private GameObject[,] grid;  // 2���� �迭�� Grid

    public static GridManager instance { get; private set; }  // �̱��� ���� ���� ����

    private void Awake()
    {
        instance = this;  // �̱���
    }

    private void Start()
    {
        grid = new GameObject[gridDimension, gridDimension];
        InitGrid();
    }


    /* �׸��� �ʱ�ȭ */

    private void InitGrid()
    {
        float center = (gridDimension * candyDistance / 2.0f) - (candyDistance / 2.0f);
        Vector3 gridCenter = new Vector3(center, center, 0);  // �׸����� ���߾�
        Vector3 positionOffset = transform.position - gridCenter;  // �׸��尡 GameObject�� �߾ӿ� ������ �������� ���

        for (int row = 0; row < gridDimension; row++)
        {
            for (int column = 0; column < gridDimension; column++)  // 2���� �迭
            {
                /* ���� ��ġ�� ��������Ʈ ���� (�ߺ� ���� ���) */
                List<Sprite> availCandySprites = new List<Sprite>(candyList);  // ��������Ʈ ����� �����ؼ� ���� ���ο� List �ν��Ͻ�

                Sprite left1 = GetSpriteAt(column - 1, row);  // 1ĭ ���� ��
                Sprite left2 = GetSpriteAt(column - 2, row);  // 2ĭ ���� ��
                if (left2 != null && left1 == left2)  // ���� �� ���� �� ��ȿ���� && �� �� ��������Ʈ ������
                {
                    availCandySprites.Remove(left1);  // �ش� ��������Ʈ ����
                }
                Sprite down1 = GetSpriteAt(column, row - 1);
                Sprite down2 = GetSpriteAt(column, row - 2);
                if (down2 != null && down1 == down2)
                {
                    availCandySprites.Remove(down1);
                }

                /* ���� ��������Ʈ ���� ��ġ */
                GameObject newCandy = Instantiate(candyPrefab);  // �� ������ ����
                SpriteRenderer renderer = newCandy.GetComponent<SpriteRenderer>();  // SpriteRenderer�� �̿��� ���� ��������Ʈ�� ������ ����
                renderer.sprite = availCandySprites[Random.Range(0, availCandySprites.Count)];  // ��� ���� ��������Ʈ ��Ͽ��� �������� �Ҵ�

                //Tile tile = newTile.AddComponent<Tile>();  // ������ Tile �����տ� Tile ������Ʈ �߰�
                //tile.position = new Vector2Int(column, row);  // �׸��忡�� ���� ��ġ

                newCandy.transform.parent = transform;  // ��� ���� �׸��尡 �θ���
                newCandy.transform.position = new Vector3(column * candyDistance, row * candyDistance, 0) + positionOffset;  // ���� ��ġ ����
                grid[column, row] = newCandy;  // ������ ���� ���� ������ ����
            }
        }
    }


    /* �ߺ� ��������Ʈ Ȯ�� */

    private Sprite GetSpriteAt(int column, int row)
    {
        // �׸��带 ����� ����
        if (column < 0 || column >= gridDimension || row < 0 || row >= gridDimension)
            return null;  // ������� ����

        // �׷��� ������
        GameObject tile = grid[column, row];  // �ش� �� �����ͼ�
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
        return renderer.sprite;  // ��������Ʈ Ȯ���ϰ� ��ȯ
    }


    /* ������ �� �� ��ȯ */

    public void SwapTiles(Vector2Int targetPosition, Vector2Int selectedPosition)  // �� Ÿ���� ��ġ
    {
        // �� ���� ��������Ʈ ������ ��������
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
