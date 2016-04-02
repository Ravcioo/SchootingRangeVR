using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootingRangeSystem : Singleton<ShootingRangeSystem> {

    [SerializeField]
    private Text pointText;

    private int totalPoints = 0;

    public void AddPoints(int value)
    {
        totalPoints += value;
        pointText.text = totalPoints.ToString();
    }
	
}
