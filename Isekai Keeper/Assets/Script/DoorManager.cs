using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    // DoorManager.cs
    public GameObject[] Door_arr;
    private Dictionary<string, int> colorDic = new Dictionary<string, int>();
    private void Start ()
    {
        AddData();
    }
    void AddData()
    {
        colorDic.Add("black", 0);
        colorDic.Add("red", 1);
        colorDic.Add("blue", 2);
        colorDic.Add("green", 3);
        colorDic.Add("yellow", 4);
    }

    public void OpenDoor(int doorIndex, string monsterColor)
    {
        Door_arr[doorIndex].GetComponent<DoorController>().is_rotate_Sig = true;
        Door_arr[doorIndex].GetComponent<DoorController>().color_index = colorDic[monsterColor];
        Debug.Log($"Door {doorIndex} is opening. Monster color: {monsterColor}");
    }

    public void CloseDoor(int doorIndex)
    {
        Door_arr[doorIndex].GetComponent<DoorController>().is_rotate_Sig = false;
        Debug.Log($"Door {doorIndex} is closing.");
    }
}
