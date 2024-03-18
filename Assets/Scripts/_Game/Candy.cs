using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Candy : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private static Candy selected;
    private SpriteRenderer render;
    private Camera cam;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        // 함수 실행마다 Camera를 찾는 것을 방지하기 위해
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selected = this;
        Debug.Log(this.name);
        Select();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UnSelect();
    }

    private void Select()
    {
        render.color = new Color(1, 1, 1, 0.8f);
        render.sortingOrder = -1;
    }

    private void UnSelect()
    {
        render.color = new Color(1, 1, 1, 1);
        render.sortingOrder = 0;
    }
}
