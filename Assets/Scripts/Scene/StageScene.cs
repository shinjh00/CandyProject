using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }

    public void GameSceneLoad()
    {
        Manager.Scene.LoadScene("GameScene");
    }
}
