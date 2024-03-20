using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Candy : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private static Candy selected;
    private Candy targeted;
    private SpriteRenderer render;
    private Camera cam;

    private float candyMoveTime = 0.1f;

    public Vector2Int position;
    public Vector2Int targetCandyPos;
    public Vector2Int selectCandyPos;

    //test

    private float timeCount;
    private Vector2 deltaValue;

    Vector2 startPos;   // Ŭ���� ��ġ
    Vector2 endPos;     // �巡���� ��ġ
    float degree;       // �巡���� ���� ����
    Vector3 curDragPos;     // ĵ���� ���� ��ġ
    Vector3 targetDragPos;  // ĵ�� �̵��� ��ġ

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        // �Լ� ���ึ�� Camera�� ã�� ���� �����ϱ� ����
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selected = this;
        Select();

        if (selected != null)
        {
            startPos = Input.mousePosition;
            //Debug.Log($"startPos: {startPos}");
        }

        curDragPos = selected.transform.position;
        //Debug.Log(curDragPos);

        selectCandyPos = selected.position;
        Debug.Log($"selectCandyPos: {selectCandyPos}");
    }

    

    public void OnBeginDrag(PointerEventData eventData)
    {
        deltaValue = Vector2.zero;

        

        /*endPos = Input.mousePosition;
        //Debug.Log($"endPos: {endPos}");

        Vector2 dragOffset = endPos - startPos;  // �巡���� ��ġ ����
        degree = Mathf.Atan2(dragOffset.y, dragOffset.x) * Mathf.Rad2Deg;
        if (degree < 0)
        {
            degree += 360;
        }
        // ��: 0, ��: 90, ��: 180, ��: 270
        //Debug.Log($"degree: {degree}");

        if ((0 <= degree && degree < 45) || (315 < degree && degree < 360))
        {
            Debug.Log("right");
            targetDragPos = new Vector3(curDragPos.x + 1, curDragPos.y, curDragPos.z);
        }
        if (45 < degree && degree < 135)
        {
            Debug.Log("up");
            targetDragPos = new Vector3(curDragPos.x, curDragPos.y + 1, curDragPos.z);
        }
        if (135 < degree && degree < 225)
        {
            Debug.Log("left");
            targetDragPos = new Vector3(curDragPos.x - 1, curDragPos.y, curDragPos.z);
        }
        if (225 < degree && degree < 315)
        {
            Debug.Log("down");
            targetDragPos = new Vector3(curDragPos.x, curDragPos.y - 1, curDragPos.z);
        }*/
    }

    public void OnDrag(PointerEventData eventData)
    {
        deltaValue += eventData.delta;
        if (eventData.dragging)
        {
            timeCount += Time.deltaTime;
            if (timeCount > 0.1f)
            {
                timeCount = 0.0f;
                Debug.Log($"delta: {deltaValue}");
            }
        }

        if (deltaValue.x > 30.0f)
        {
            Debug.Log("right");
        }
        if (deltaValue.x < -30.0f)
        {
            Debug.Log("left");
        }
        if (deltaValue.y > 30.0f)
        {
            Debug.Log("up");
        }
        if (deltaValue.y < -30.0f)
        {
            Debug.Log("down");
        }




        /*moveCandy(targetDragPos);

        Debug.Log($"targetCandyPos: {targetCandyPos}");
        Debug.Log($"selectedCandyPos: {selectCandyPos}");
        GridManager.instance.SwapCandy(targetCandyPos, selectCandyPos);*/

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        deltaValue = Vector2.zero;


        //selected = null;
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

    /*private Vector3 GetMousePos()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        return mousePos;
    }*/

    private void moveCandy(Vector3 targetDragPos)
    {
        StartCoroutine(moveCandyCoroutine(targetDragPos));
    }

    private IEnumerator moveCandyCoroutine(Vector3 targetDragPos)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < candyMoveTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(curDragPos, targetDragPos, elapsedTime / candyMoveTime);
            transform.position = Vector3.Lerp(curDragPos, targetDragPos, elapsedTime / candyMoveTime);
            yield return null;
        }
        //transform.position = targetDragPos;
    }


    private void Update()
    {
        // Ray test
        if (selected != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.blue);
        }
    }
}
