using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
    [SerializeField] GameObject[] episodeMap;  // episodeMap[0] : EpisodeMap_odd, episodeMap[1] : EpisodeMap_even
    [SerializeField] int epiNum;

    private void Start()
    {
        epiNum = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateEpisode(epiNum);
            Debug.Log(epiNum);
        }
    }

    private void CreateEpisode(int epiNum)
    {

        if (epiNum % 2 == 1)  // odd
        {
            Debug.Log(episodeMap[0].name);
            GameObject instance = Instantiate(episodeMap[0], episodeMap[0].transform.position, episodeMap[0].transform.rotation);
            instance.transform.parent = transform;
        }
        else  // even
        {
            Instantiate(episodeMap[1], episodeMap[1].transform.position, episodeMap[1].transform.rotation);
            episodeMap[1].transform.parent = transform;
        }
        epiNum++;

        //episodeMap.SetAddStartIdx(index * 10);
        //index++;
    }

}
