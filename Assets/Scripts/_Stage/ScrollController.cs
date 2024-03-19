using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    [SerializeField] GameObject[] episodeMap;   // 생성할 프리팹
    [SerializeField] RectTransform content;     // 프리팹을 담을 부모 오브젝트 Content
    [SerializeField] ScrollRect scrollRect;

    // private
    [SerializeField] int myStageNum;

    private TextMeshProUGUI[] epiLabelText;
    private Vector2 anchoredPosition;
    private float scrollAmount = -9000;

    private void Awake()
    {
        CreateEpisode();
        FocusMyStage();
    }

    private void CreateEpisode()
    {
        int curEpiNum;
        int curStageNum;

        if (myStageNum <= 10)
        {
            curEpiNum = 1;
            curStageNum = myStageNum;
        }
        else
        {
            if (myStageNum % 10 == 0)
            {
                curEpiNum = myStageNum / 10;    // 20: episode 2 , 120: episode 12
                curStageNum = 10;               // 20: stage 10  , 120: stage 10
            }
            else
            {
                curEpiNum = myStageNum / 10 + 1;                    // 17: episode 2 , 117: episode 12
                curStageNum = myStageNum % ((curEpiNum - 1) * 10);  // 17: stage 7   , 117: stage 7
            }
        }

        Debug.Log($"my: {myStageNum}, curEpi: {curEpiNum}, curStage: {curStageNum}");

        for (int i = 1; i < curEpiNum + 1; i++)
        {
            if (i % 2 == 1)  // odd
            {
                Debug.Log(episodeMap[0].name);
                GameObject prefab = Instantiate(episodeMap[0]);
                prefab.transform.SetParent(content.transform, false);
                epiLabelText = prefab.transform.GetComponentsInChildren<TextMeshProUGUI>();
            }
            else  // even
            {
                Debug.Log(episodeMap[1].name);
                GameObject prefab = Instantiate(episodeMap[1]);
                prefab.transform.SetParent(content.transform, false);
                epiLabelText = prefab.transform.GetComponentsInChildren<TextMeshProUGUI>();
            }
            epiLabelText[0].text = $"EPISODE {i.ToString()}";
        }

        //episodeMap.SetAddStartIdx(index * 10);
        //index++;
    }

    private void FocusMyStage()
    {
        Vector2 prePos = scrollRect.content.anchoredPosition;
        Vector2 newPos = prePos + new Vector2(0, scrollAmount);
        scrollRect.content.anchoredPosition = newPos;
    }
}
