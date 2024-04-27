using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class DrawRune : MonoBehaviour
{
    public GameObject runePrefab;
    public RuneComparator runeComparator;
    public RuneSaver runeSaver;
    public Camera drawingCamera;

    private LineRenderer lr;
    private List<Vector2> drawnPoints = new List<Vector2>();
    private GameObject currentRune;
    
    public float elapsed = 0f;
    public float duration = 1f;
    public float minThickness = 0.02f;
    public float maxThickness = 0.2f; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentRune = Instantiate(runePrefab);
            lr = currentRune.GetComponent<LineRenderer>();
            drawnPoints.Add(Input.mousePosition);
            lr.positionCount = 1;
            lr.SetPosition(0, drawingCamera.ScreenToWorldPoint(drawnPoints[0]));
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 pos = Input.mousePosition;
            drawnPoints.Add(pos);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, drawingCamera.ScreenToWorldPoint(pos));
            
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);
            float thickness = Mathf.Lerp(maxThickness, minThickness, t);

            lr.startWidth = thickness;
            lr.endWidth = thickness * 0.5f;

            float hueShift = Time.time * 0.1f;
            Color glowColor = Color.HSVToRGB((hueShift % 1f), 1f, 1f);
            lr.startColor = glowColor;
            lr.endColor = glowColor;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            elapsed = 0f;
            if (runeSaver.isSaveMode)
            {
                runeSaver.SaveRune(drawnPoints);
                drawnPoints.Clear();
            }
            //else
            //{
            //    runeComparator.CompareRunes(drawnPoints);
            //}

            Destroy(currentRune);

        }
    }
    
    public List<Vector2> GetDrawnPoints()
    {
        return drawnPoints;
    }
    
    public void ClearDrawnPoints()
    {
        drawnPoints.Clear();
    }
}