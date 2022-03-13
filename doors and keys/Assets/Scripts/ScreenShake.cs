using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instace;

    [SerializeField] float shakePower;
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeCooldown;
    [SerializeField] float rotationPower;

    private float timeRemaining;
    private float fadeTime;
    private float modifiedShakePower;

    private void Awake()
    {
        instace = this;
        timeRemaining = 0;
    }

    private void Start()
    {
        InvokeRepeating("InvokeShake", shakeCooldown, shakeCooldown);
    }

    public void InvokeShake()
    {
        modifiedShakePower = shakePower;
        timeRemaining = shakeDuration;
        fadeTime = modifiedShakePower / shakeDuration;
    }

    private void LateUpdate()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * modifiedShakePower;
            float zAmount = Random.Range(-1f, 1f) * modifiedShakePower;

            transform.position += new Vector3(xAmount, 0f, zAmount);

            modifiedShakePower = Mathf.MoveTowards(modifiedShakePower, 0f, fadeTime * Time.deltaTime);
        }
    }
}
