using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    <SpriteRenderer>
    ��������Ʈ�� �������ϰ� ��������Ʈ�� ���� �ð������� ǥ�õǴ� ����� ����
*/

namespace JH
{
    public class GridManager : MonoBehaviour
    {
        public List<Sprite> sprites = new List<Sprite>();  // ��ġ�� ��������Ʈ ���
        public GameObject tilePrefab;  // ���� �����ϱ� ���� "stencil". ��������Ʈ�� �Ҵ��Ͽ� ����ȭ��
        public int gridDimension = 8;  // �׸����� ũ��
        public float distance = 2.0f;  // ���� �Ÿ�

        private GameObject[,] grid;  // 2���� �迭�� Grid

        private void Start()
        {
            grid = new GameObject[gridDimension, gridDimension];
            InitGrid();
        }


        /* �׸��� �ʱ�ȭ */

        private void InitGrid()
        {
            Vector3 gridCenter = new Vector3(gridDimension * distance / 2.0f, gridDimension * distance / 2.0f, 0);  // �׸����� ���߾�
            Vector3 positionOffset = transform.position - gridCenter;  // �׸��尡 GameObject�� �߾ӿ� ������ �������� ���

            for (int row = 0; row < gridDimension; row++)
            {
                for (int column = 0; column < gridDimension; column++)  // 2���� �迭
                {
                    /* ���� ��ġ�� ��������Ʈ ���� (�ߺ� ���� ���) */
                    List<Sprite> availableSprites = new List<Sprite>(sprites);  // ��������Ʈ ����� �����ؼ� ���� ���ο� List �ν��Ͻ�

                    Sprite left1 = GetSpriteAt(column - 1, row);  // 1ĭ ���� ��
                    Sprite left2 = GetSpriteAt(column - 2, row);  // 2ĭ ���� ��
                    if (left2 != null && left1 == left2)  // ���� �� ���� �� ��ȿ���� && �� �� ��������Ʈ ������
                    {
                        availableSprites.Remove(left1);  // �ش� ��������Ʈ ����
                    }
                    Sprite down1 = GetSpriteAt(column, row - 1);
                    Sprite down2 = GetSpriteAt(column, row - 2);
                    if (down2 != null && down1 == down2)
                    {
                        availableSprites.Remove(down1);
                    }

                    /* ���� ��������Ʈ ���� ��ġ */
                    GameObject newTile = Instantiate(tilePrefab);  // ������ ����
                    SpriteRenderer renderer = newTile.GetComponent<SpriteRenderer>();  // SpriteRenderer�� �̿��� ���� ��������Ʈ�� ������ ����
                    renderer.sprite = availableSprites[Random.Range(0, availableSprites.Count)];  // ��� ���� ��������Ʈ ��Ͽ��� �������� �Ҵ�

                    Tile tile = newTile.GetComponent<Tile>();

                    newTile.transform.parent = transform;  // ��� ���� �׸��尡 �θ���
                    newTile.transform.position = new Vector3(column * distance, row * distance, 0) + positionOffset;  // ���� ��ġ ����
                    grid[column, row] = newTile;  // ������ ���� ���� ������ ����
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

        private void Update()
        {

        }
    }
}


