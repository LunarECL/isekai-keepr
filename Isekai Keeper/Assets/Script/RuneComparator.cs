using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class RuneComparator : MonoBehaviour
{
    public List<RuneData> preparedRunes;
    public float similarityThreshold = 0.8f;
    public int numPointsToCompare = 100;

    public void CompareRunes(List<Vector2> drawnPoints)
    {
        foreach (RuneData rune in preparedRunes)
        {
            bool isMatched = CompareRunePoints(drawnPoints, rune.points);
            if (isMatched)
            {
                Debug.Log("Rune match: " + rune.name);
                break;
            }
        }
    }

    private bool CompareRunePoints(List<Vector2> drawnPoints, List<Vector2> preparedPoints)
    {
        List<Vector2> normalizedDrawnPoints = NormalizePoints(drawnPoints, numPointsToCompare);
        List<Vector2> normalizedPreparedPoints = NormalizePoints(preparedPoints, numPointsToCompare);

        float similarity = CalculateSimilarity(normalizedDrawnPoints, normalizedPreparedPoints);
        Debug.Log("Similarity: " + similarity);
        return similarity >= similarityThreshold;
    }

    private float CalculateSimilarity(List<Vector2> drawnPoints, List<Vector2> preparedPoints)
    {
        int n = drawnPoints.Count;
        int m = preparedPoints.Count;
        float[,] dtw = new float[n + 1, m + 1];

        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= m; j++)
            {
                dtw[i, j] = float.PositiveInfinity;
            }
        }
        dtw[0, 0] = 0;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                float cost = Vector2.Distance(drawnPoints[i - 1], preparedPoints[j - 1]);
                dtw[i, j] = cost + Mathf.Min(dtw[i - 1, j], dtw[i, j - 1], dtw[i - 1, j - 1]);
            }
        }

        float similarity = 1f - (dtw[n, m] / Mathf.Max(n, m));
        return Mathf.Clamp01(similarity);
    }

    private List<Vector2> NormalizePoints(List<Vector2> points, int numPoints)
    {
        List<Vector2> normalizedPoints = new List<Vector2>();

        Vector2 centroid = CalculateCentroid(points);
        float maxDistance = CalculateMaxDistance(points, centroid);

        float stepSize = (float)(points.Count - 1) / (numPoints - 1);
        for (int i = 0; i < numPoints; i++)
        {
            int index = Mathf.Clamp(Mathf.RoundToInt(i * stepSize), 0, points.Count - 1);
            Vector2 point = points[index];
            Vector2 normalizedPoint = (point - centroid) / maxDistance;
            normalizedPoints.Add(normalizedPoint);
        }

        return normalizedPoints;
    }

    private Vector2 CalculateCentroid(List<Vector2> points)
    {
        Vector2 sum = Vector2.zero;
        foreach (Vector2 point in points)
        {
            sum += point;
        }
        return sum / points.Count;
    }

    private float CalculateMaxDistance(List<Vector2> points, Vector2 centroid)
    {
        float maxDistance = 0f;
        foreach (Vector2 point in points)
        {
            float distance = Vector2.Distance(point, centroid);
            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
        }
        return maxDistance;
    }
}