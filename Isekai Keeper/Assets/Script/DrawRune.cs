using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class DrawRune : MonoBehaviour
{
    public GameObject runePrefab;
    public RuneComparator runeComparator;
    public RuneSaver runeSaver;

    private LineRenderer lr;
    private List<Vector2> drawnPoints = new List<Vector2>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject rune = Instantiate(runePrefab);
            lr = rune.GetComponent<LineRenderer>();
            drawnPoints.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lr.positionCount = 1;
            lr.SetPosition(0, drawnPoints[0]);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            drawnPoints.Add(pos);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, pos);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (runeSaver.isSaveMode)
            {
                runeSaver.SaveRune(drawnPoints);
            }
            else
            {
                runeComparator.CompareRunes(drawnPoints);
            }
            drawnPoints.Clear();
        }
    }
}