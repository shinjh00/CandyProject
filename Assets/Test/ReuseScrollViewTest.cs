using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[RequireComponent(typeof(ScrollRect))]
public class ReuseScrollViewTest : MonoBehaviour
{
    public ScrollItemTest slotsAreaPrefab;    // 생성할 프리팹
    public RectTransform slotsAreaRect;       // 프리팹의 RectTransform
    public float itemHeight;                  // 프리팹의 높이 값
    public List<int> slotAreaList;            // 슬롯 리스트
    public int slotAreaListCnt = 2;           // 슬롯 리스트 개수

    public ScrollRect _scrollRect;           // ScrollView의 Scroll Rect
    public RectTransform _rectTransform;     // ScrollView의 RectTransform
    public List<ScrollItemTest> itemList;    // 생성된 프리팹 리스트
    public float offset;


    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();  // Scroll View의 Scroll Rect 컴포넌트
        _rectTransform = GetComponent<RectTransform>();
        slotsAreaRect = slotsAreaPrefab.GetComponent<RectTransform>();  // SlotPrefab의 RectTransform
    }


    private void Start()
    {
        itemHeight = slotsAreaRect.sizeDelta.y;

        // dataList 세팅
        slotAreaList.Clear();

        for (int i = 0; i < slotAreaListCnt; i++)
            slotAreaList.Add(i);

        CreateSlotArea();
        //SetContentHeight();
    }


    private void CreateSlotArea()
    {
        // slotArea 프리팹을 인스턴스로 2개만 생성

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
        // Content 크기 늘이기
        //_scroll.content.sizeDelta = new Vector2(_scroll.content.sizeDelta.x, dataList.Count * itemHeight);
        _scroll.content.sizeDelta = new Vector2(_scroll.content.sizeDelta.x, 30000);
    }*/


    private void SetContentPos()
    {
        // Content 위치 변경
    }


    private bool RelocationItem(ScrollItemTest item, float contentY, float scrollHeight)
    {
        // 스크롤할 때 프리팹 인스턴스 재배치

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
