using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ScrollItem : MonoBehaviour
{
    public TMPro.TextMeshProUGUI episodeLabel;
    

    private void Start()
    {
        
    }

    public void SetEpisodeNum(int episodeNum)
    {
        episodeLabel.text = episodeNum.ToString();
    }

    private void SetStageNum()
    {

    }

    private void Update()
    {
        
    }
}
