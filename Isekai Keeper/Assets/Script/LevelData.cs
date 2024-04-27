using System;

[Serializable]
public class LevelData
{
    public float timeLimit;
    public MonsterSpawnData[] monsters;
    public DoorDirection allowedDoorDirections;
    public bool isInfinite;
}

[Serializable]
public class MonsterSpawnData
{
    public float spawnDelay;
}

[Serializable]
public enum DoorDirection
{
    Front = 1,
    FrontAndSide = 2,
    All = 3
}