using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeed = 0.01f;

    public Transform rightCheck;
    public Transform leftCheck;
    public Transform groundCheck;
    public LayerMask layerMaskGround;
    public GameScreenMovement game;

    bool jumped = false;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var leftCheckSphere = Physics.OverlapSphere(leftCheck.position, 0.2f, layerMaskGround);
        var rightCheckSphere = Physics.OverlapSphere(rightCheck.position, 0.2f, layerMaskGround);
        var groundCheckSphere = Physics.OverlapSphere(groundCheck.position, 0.2f, layerMaskGround);

        if (Input.GetKey(KeyCode.LeftArrow) && leftCheckSphere.Length > 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - playerSpeed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
        }

        if (Input.GetKey(KeyCode.RightArrow) && rightCheckSphere.Length > 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + playerSpeed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
        }

        if (groundCheckSphere.Length > 0 && !jumped)
        {
            rb.velocity = new Vector2(0f, 0f);
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
            case "Thorn":
                Destroy(gameObject);
                break;
            case "Jump Pad":
                StartCoroutine(Jumping());
                rb.AddForce(new Vector3(0f, 65f, 0f), ForceMode.Impulse);
                break;
        }
    }

    IEnumerator Jumping()
    {
        jumped = true;
        yield return new WaitForSeconds(0.5f);
        jumped = false;
    }
}
