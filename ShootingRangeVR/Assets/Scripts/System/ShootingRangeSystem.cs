using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootingRangeSystem : Singleton<ShootingRangeSystem> {

    [Header("Settings")]
    [SerializeField]
    private float visibleTime = 2f;
    [SerializeField]
    private int targetCount = 10;

    [Header("References")]
    [SerializeField]
    private TextMesh pointText;
    [SerializeField]
    private TextMesh runText;
    [SerializeField]
    private TextMesh stateText;

    private Target[] targets;
    private int totalPoints = 0;
    private float visibleTimer = Mathf.Infinity;
    private int targetCounter = 0;
    private bool working = false;
    private Target lastTarget = null;



    void Awake()
    {
        targets = GetComponentsInChildren<Target>();
        SetTexts(false);
    }

    void Update()
    {
        if(working)
        {
            visibleTimer += Time.deltaTime;

            if (visibleTimer > visibleTime)
            {
                ActivateNext();
                visibleTimer = 0;
                targetCounter++;
            }

            if(targetCounter>=targetCount)
            {
                TurnOff();
            }
        }
    }

    private void ActivateNext()
    {
        while(true)
        {
            int index = Random.Range(0, targets.Length);

            Target candidate = targets[index];

            if(candidate != null && candidate != lastTarget)
            {
                if(lastTarget != null)
                {
                    lastTarget.Hide();
                }                
                candidate.Show();
                lastTarget = candidate;
                break;
            }
        }
    }

    public void TargetHitted()
    {
        //ActivateNext();
        //visibleTimer = 0;
        //targetCounter++;
    }

    private void SetTexts(bool working)
    {
        if(working)
        {
            stateText.text = "Running";
            stateText.color = Color.green;
            runText.text = "Stop";
            runText.color = Color.red;
        }
        else
        {
            stateText.text = "Stopped";
            stateText.color = Color.red;
            runText.text = "Run";
            runText.color = Color.green;
        }       
    }

    public void RunButton()
    {
        if(working)
        {
            TurnOff();
        }
        else
        {
            Run();
        }
    }

    void Run()
    {
        SetTexts(true);
        StartCoroutine(RunCoroutine());
    }

    private void TurnOff()
    {
        SetTexts(false);
        working = false;

        foreach (var target in targets)
        {
            target.Hide();
        }
    }

    private IEnumerator RunCoroutine()
    {
        totalPoints = 0;
        RefreshPoints();
        visibleTimer = Mathf.Infinity;
        targetCounter = 0;


        foreach (var target in targets)
        {
            target.Hide();
        }

        yield return new WaitForSeconds(2);

        working = true;      
    }


    public void AddPoints(int value)
    {
        if(working)
        {
            totalPoints += value;
            RefreshPoints();
        }
       
    }

    private void RefreshPoints()
    {
        pointText.text = totalPoints.ToString();
    }
	
}
