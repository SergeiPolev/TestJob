using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 0.01f;

    public Transform rightCheck;
    public Transform leftCheck;
    public Transform groundCheck;
    public LayerMask layerMaskGround;
    public GameScreenMovement game;
    public Slider checkpointSlider;

    bool jumped = false;
    int checkpoint = 0;
    public int checkpointCounter = 1;

    Rigidbody rb;

    public Text diamondsCounterText;
    [SerializeField] private int diamonds = 0;

    private void Start()
    {
        diamondsCounterText.text = diamonds.ToString();
        checkpointSlider.maxValue = checkpointCounter;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        checkpointSlider.value = checkpoint;

        var leftCheckSphere = Physics.OverlapSphere(leftCheck.position, 0.2f, layerMaskGround);
        var rightCheckSphere = Physics.OverlapSphere(rightCheck.position, 0.2f, layerMaskGround);
        var groundCheckSphere = Physics.OverlapSphere(groundCheck.position, 0.2f, layerMaskGround);

        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.GetTouch(0);
            if (myTouch.phase == TouchPhase.Moved)
            {
                if (myTouch.deltaPosition.x != 0 )
                {
                    Debug.Log(myTouch.deltaPosition.x);
                    if (myTouch.deltaPosition.x > 0 && rightCheckSphere.Length == 0)
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
                    } else if (myTouch.deltaPosition.x < 0 && leftCheckSphere.Length == 0)
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
                    }
                    else
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x + myTouch.deltaPosition.x / 10 * playerSpeed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
                    }

                }
            }
        }

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
                IncreaseDiamonds(1);
                Destroy(other.gameObject);
                break;
            case "Thorn":
                PlayerPrefs.SetInt("Last Level", SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene("Lose");
                Destroy(gameObject);
                break;
            case "Jump Pad":
                StartCoroutine(Jumping());
                AddForce(65f);
                break;
            case "Multiplier":
                diamonds *= other.GetComponent<Multiplier>().GetMultiplier();
                PlayerPrefs.SetInt("Last Diamonds", diamonds);
                PlayerPrefs.SetInt("Last Level", SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene("Results");
                break;
            case "Checkpoint":
                checkpoint++;
                break;
        }
    }

    public void AddForce(float upForce)
    {
        rb.AddForce(new Vector3(0f, upForce, 0f), ForceMode.Impulse);
    }
    public void IncreaseDiamonds(int count)
    {
        diamonds += count;
        diamondsCounterText.text = diamonds.ToString();
    }
    IEnumerator Jumping()
    {
        jumped = true;
        yield return new WaitForSeconds(0.5f);
        jumped = false;
    }
}
