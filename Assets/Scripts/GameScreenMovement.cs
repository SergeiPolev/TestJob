using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenMovement : MonoBehaviour
{
    public float playerSpeed = 1f;
    public float timeToRotate = 1f;
    public float speedMutiplier = 1f;
    public float finishSpeedMultiplier = 1f;
    public Button buttonFinish;
    public Slider sliderFinish;

    bool finishing = false;

    public PlayerMovement playerMovement;
    
    void Start()
    {
        buttonFinish.onClick.AddListener(IncreaseFinishMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.Rotate(0, -90f, 0, Space.World);
        }
            
        if (Input.GetKeyDown(KeyCode.X))
        {
            transform.Rotate(0, 90f, 0, Space.World);
        }

        sliderFinish.value = finishSpeedMultiplier;
    }

    IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Yes");
        var tag = other.tag;
        switch (tag)
        {
            case "Left":
                StartCoroutine(Rotate(Vector3.up, -90f, timeToRotate / speedMutiplier));
                break;
            case "Right":
                StartCoroutine(Rotate(Vector3.up, 90f, timeToRotate / speedMutiplier));
                break;
            case "Finish":
                if (!finishing)
                {
                    finishing = true;
                    sliderFinish.gameObject.SetActive(true);
                    buttonFinish.gameObject.SetActive(true);
                }
                else
                {
                    playerMovement.AddForce(65f);
                }
                break;
        }
    }
    public void IncreaseMultiplier()
    {
        speedMutiplier += 1f;
    }
    public void DecreaseMultiplier()
    {
        speedMutiplier -= 1f;
    }
    
    public void IncreaseFinishMultiplier()
    {
        finishSpeedMultiplier += 0.2f;
    }
    private void FixedUpdate()
    {
        transform.position += playerSpeed * speedMutiplier * transform.forward * finishSpeedMultiplier;

        if (finishing && finishSpeedMultiplier > 1f)
        {
            finishSpeedMultiplier -= 0.01f;
        }
    }
}
