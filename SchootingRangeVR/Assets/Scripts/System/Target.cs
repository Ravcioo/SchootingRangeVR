using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    [SerializeField]
    private Transform rotateAroundPivot;
    [SerializeField]
    private AnimationCurve rotationCurve;
    [SerializeField]
    private float animSpeed = 1f;

    public bool isActiveTarget = false;

    private bool blockTarget = false;
    private float phase = 0;
    private int direction = 1;
    private Vector3 startPosition;
    

    void Start()
    {
        startPosition = transform.position;
        phase = 1;
        Calculate(phase);

        StartCoroutine(Test());
    }

	void Update ()
    {
        phase += direction * Time.deltaTime * animSpeed;
        phase = Mathf.Clamp01(phase);

        isActiveTarget = !Mathf.Approximately(phase, 1) && !blockTarget;

        Calculate(phase);
	}

    private void Calculate(float phase)
    {
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        transform.RotateAround(rotateAroundPivot.position, new Vector3(1, 0, 0), rotationCurve.Evaluate(phase) * -90f);
    }

    public void Show()
    {
        direction = -1;
        blockTarget = false;
    }

    public void Hide()
    {
        direction = 1;
    }

    public void OnHit()
    {
        Hide();
        blockTarget = true;
    }


    IEnumerator Test()
    {
        Show();
        yield return new WaitForSeconds(1);
        Hide();
        yield return new WaitForSeconds(2);
        Show();
        yield return new WaitForSeconds(3);
        Hide();
        yield return new WaitForSeconds(5);
        Show();
        yield return new WaitForSeconds(3);
        Hide();
        yield return new WaitForSeconds(1);
        Show();
        yield return new WaitForSeconds(3);
        Hide();
        yield return new WaitForSeconds(3);
        Show();
        yield return new WaitForSeconds(3);
        Hide();
        yield return new WaitForSeconds(1);
        Show();
        yield return new WaitForSeconds(3);
        Hide();
        yield return new WaitForSeconds(1);
        Show();
        yield return new WaitForSeconds(3);
        Hide();
        yield return new WaitForSeconds(1);

    }
}
