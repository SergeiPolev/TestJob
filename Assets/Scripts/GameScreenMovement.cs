using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenMovement : MonoBehaviour
{
    public float playerSpeed = 1f;
    public float timeToRotate = 1f;
    public float speedMutiplier = 1f;

    public Text diamondsCounterText;
    
    [SerializeField] private int diamonds = 0;


    void Start()
    {
        diamondsCounterText.text = diamonds.ToString();
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
    public void IncreaseDiamonds(int count) 
    {
        diamonds += count;
        diamondsCounterText.text = diamonds.ToString();
    }

    private void FixedUpdate()
    {
        transform.position += playerSpeed * speedMutiplier * transform.forward;
    }
}
