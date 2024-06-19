using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private static Tile selected;
    private SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    public void Select()
    {
        //render.color = Color.white;
        render.color = new Color(1, 1, 1, 0.5f);
    }

    public void Unselect()
    {
        render.color = new Color(1, 1, 1, 1);
    }

    private void OnMouseDown()
    {
        if (selected == this)
        {
            selected = null;
            Unselect();
            return;
        }
        /*if (selected != null)
        {
            selected.Unselect();
            if (Vector3.Distance(selected.transform.position, transform.position) == 1)
            {
                SwapAndCheckMatch(selected, this, false);
                selected = null;
                return;
            }
        }*/
        selected = this;
        Select();
    }

    private void Update()
    {
        
    }
}
