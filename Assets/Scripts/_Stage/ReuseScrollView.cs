using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[RequireComponent(typeof(ScrollRect))]
public class ReuseScrollView : MonoBehaviour
{
    public ScrollItem episodeMap;         // ������ ������
    private float itemHeight = 3000.0f;   // �������� ���� ��
    public List<int> dataList;            // ������ ����Ʈ
    public int dataListCnt;

    private ScrollRect _scroll;           // Scroll View - Scroll Rect
    private List<ScrollItem> itemList;    // ������ ������ ����Ʈ
    private float offset;

    // test
    private int myStageNum = 6;

    private void Awake()
    {
        _scroll = GetComponent<ScrollRect>();  // Scroll View�� Scroll Rect ������Ʈ
    }


    private void Start()
    {
        // dataList ����
        dataList.Clear();

        for (int i = 0; i < dataListCnt; i++)
            dataList.Add(i);

        CreateEpisode();
        SetContentHeight();
    }


    private void CreateEpisode()
    {
        // episodeMap �������� �ν��Ͻ��� 3���� ����
        RectTransform scrollRect = _scroll.GetComponent<RectTransform>();  // Scroll View�� RectTransform ������Ʈ
        itemList = new List<ScrollItem>();
        float itemLocalY = (dataListCnt - 1) * 0.5f * itemHeight;  // ((dataListCnt - 1) / 2) * itemHeight

        for (int i = 0; i < 3; i++)
        {
            ScrollItem item = Instantiate<ScrollItem>(episodeMap, _scroll.content);
            itemList.Add(item);
            item.transform.localPosition = new Vector3(0, i * itemHeight);
            //item.transform.localPosition = new Vector3(0, (i * itemHeight) - itemLocalY);
            SetData(item, i);
        }
        offset = itemList.Count * itemHeight;
    }


    private void SetContentHeight()
    {
        // Content ũ�� ���̱�
        //_scroll.content.sizeDelta = new Vector2(_scroll.content.sizeDelta.x, dataList.Count * itemHeight);
        _scroll.content.sizeDelta = new Vector2(_scroll.content.sizeDelta.x, 30000);
    }


    private void SetContentPos()
    {
        // Content ��ġ ����
    }


    private bool RelocationItem(ScrollItem item, float contentY, float scrollHeight)
    {
        // ��ũ���� �� ������ �ν��Ͻ� ���ġ
        Debug.Log($"contentY: {contentY}");
        
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


    private void SetData(ScrollItem item, int idx)
    {
        if (idx < 0 || idx >= dataList.Count)
        {
            item.gameObject.SetActive(false);
            return;
        }
        item.gameObject.SetActive(true);
        item.SetEpisodeNum(dataList[idx]);
    }


    private void Update()
    {
        RectTransform scrollRect = _scroll.GetComponent<RectTransform>();
        float scrollHeight = scrollRect.rect.height;
        float contentY = _scroll.content.anchoredPosition.y;

        foreach (ScrollItem item in itemList)
        {
            bool isChanged = RelocationItem(item, contentY, scrollHeight);
            if (isChanged)
            {
                int idx = (int)(-item.transform.localPosition.y / itemHeight);
                SetData(item, idx);
            }
        }
    }
}
