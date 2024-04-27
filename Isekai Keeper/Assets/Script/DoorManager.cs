using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    // DoorManager.cs
    public GameObject[] Door_arr;
    private Dictionary<string, int> colorDic = new Dictionary<string, int>();
  
    void AddData()
    {
        colorDic.Add("Black", 0);
        colorDic.Add("Red", 1);
        colorDic.Add("Blue", 2);
        colorDic.Add("Green", 3);
        colorDic.Add("Yellow", 4);
    }

    public void OpenDoor(int doorIndex, string monsterColor)
    {
        if (colorDic.Count == 0)
        {
            AddData();
        }
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
