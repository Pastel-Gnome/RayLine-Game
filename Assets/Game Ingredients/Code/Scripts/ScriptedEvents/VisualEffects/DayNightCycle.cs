using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] GameObject nightFilter;

    public void SetTimeVisuals(GameInfo.timeOfDay gameTime)
    {
        if ((int)gameTime < (int)GameInfo.timeOfDay.Evening)
        {
            nightFilter.SetActive(false);
        } else
        {
            nightFilter.SetActive(true);
        }
    }
}
