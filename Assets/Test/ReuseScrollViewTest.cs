using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[RequireComponent(typeof(ScrollRect))]
public class ReuseScrollViewTest : MonoBehaviour
{
    public ScrollItemTest slotsAreaPrefab;    // ������ ������
    public RectTransform slotsAreaRect;       // �������� RectTransform
    public float itemHeight;                  // �������� ���� ��
    public List<int> slotAreaList;            // ���� ����Ʈ
    public int slotAreaListCnt = 2;           // ���� ����Ʈ ����

    public ScrollRect _scrollRect;           // ScrollView�� Scroll Rect
    public RectTransform _rectTransform;     // ScrollView�� RectTransform
    public List<ScrollItemTest> itemList;    // ������ ������ ����Ʈ
    public float offset;


    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();  // Scroll View�� Scroll Rect ������Ʈ
        _rectTransform = GetComponent<RectTransform>();
        slotsAreaRect = slotsAreaPrefab.GetComponent<RectTransform>();  // SlotPrefab�� RectTransform
    }


    private void Start()
    {
        itemHeight = slotsAreaRect.sizeDelta.y;

        // dataList ����
        slotAreaList.Clear();

        for (int i = 0; i < slotAreaListCnt; i++)
            slotAreaList.Add(i);

        CreateSlotArea();
        //SetContentHeight();
    }


    private void CreateSlotArea()
    {
        // slotArea �������� �ν��Ͻ��� 2���� ����

        itemList = new List<ScrollItemTest>();

        for (int i = 0; i < slotAreaListCnt; i++)
        {
            ScrollItemTest item = Instantiate<ScrollItemTest>(slotsAreaPrefab, _scrollRect.content);
            itemList.Add(item);
            item.transform.localPosition = new Vector3(0, i * itemHeight);
        }
        offset = itemList.Count * itemHeight;
    }


    /*private void SetContentHeight()
    {
        // Content ũ�� ���̱�
        //_scroll.content.sizeDelta = new Vector2(_scroll.content.sizeDelta.x, dataList.Count * itemHeight);
        _scroll.content.sizeDelta = new Vector2(_scroll.content.sizeDelta.x, 30000);
    }*/


    private void SetContentPos()
    {
        // Content ��ġ ����
    }


    private bool RelocationItem(ScrollItemTest item, float contentY, float scrollHeight)
    {
        // ��ũ���� �� ������ �ν��Ͻ� ���ġ

        if (item.transform.localPosition.y + contentY > itemHeight * 2f)
        {
            item.transform.localPosition -= new Vector3(0, offset);
            RelocationItem(item, contentY, scrollHeight);
            return true;
        }
        else if (item.transform.localPosition.y + contentY < -scrollHeight - itemHeight)
        {
            item.transform.localPosition += new Vector3(0, offset);
            RelocationItem(item, contentY, scrollHeight);
            return true;
        }
        return false;
    }




    private void Update()
    {
        float scrollHeight = _rectTransform.rect.height;
        float contentY = _scrollRect.content.anchoredPosition.y;
        Debug.Log(contentY);
        RelocationItem(slotsAreaPrefab, contentY, scrollHeight);

    }
}
