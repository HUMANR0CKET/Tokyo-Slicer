using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBarController : MonoBehaviour
{
    public Slider slider;

    private void Update()
    {
        
    }
    public void setMaxDash(int maxPoints)
    {
        slider.maxValue = maxPoints;
        slider.value = 0;
    }

    public void addDashPoints(int points)
    {
        slider.value += points;
    }

    public void removeDashPoints(int points)
    {
        slider.value -= points;
    }

}
