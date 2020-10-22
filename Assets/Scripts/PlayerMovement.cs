using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeed = 0.01f;

    public Transform rightCheck;
    public Transform leftCheck;
    public LayerMask layerMaskGround;

    public GameScreenMovement game;

    private void Start()
    {
    }

    private void Update()
    {
        var leftCheckSphere = Physics.OverlapSphere(leftCheck.position, 0.2f, layerMaskGround);
        var rightCheckSphere = Physics.OverlapSphere(rightCheck.position, 0.2f, layerMaskGround);
        if (Input.GetKey(KeyCode.LeftArrow) && leftCheckSphere.Length > 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - playerSpeed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
        }

        if (Input.GetKey(KeyCode.RightArrow) && rightCheckSphere.Length > 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + playerSpeed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        switch (tag)
        {
            case "Speed Up":
                game.IncreaseMultiplier();
                break;
            case "Diamond":
                game.IncreaseDiamonds(1);
                Destroy(other.gameObject);
                break;
        }
    }
}
