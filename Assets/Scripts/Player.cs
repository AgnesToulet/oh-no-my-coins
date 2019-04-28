using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MovingObject {

    public AudioClip coinSound;
    public AudioClip goThroughSound;
    public AudioClip jumpSound;
    public AudioClip failSound;

    public int wallDamage = 1;
    public int pointsPerChest = 100;
    public int pointsPerPileOfGold = 50;
    public float restartLevelDelay = 1f;

    public Sprite[] sprites;

    private Animator animator;
    private Text moneyText;

    private SpriteRenderer spriteRenderer;
    private int playerDirection = 0;
    private int money = 100;

    private bool isHiding = false;

    private IEnumerator coroutine;

    public bool playerIsHiding () {
        return isHiding;
    }

    protected override void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        money = Tilesmaps.GetLevelMoney(GameManager.instance.GetLevel());
        moneyText.text = money.ToString();

        base.Start();

        StartCoroutine(LoseMoneyCoroutine());
    }

    public IEnumerator LoseMoneyCoroutine() {
        while (true) {
            if (!GameManager.instance.doingSetup) {
                LoseMoney();
            }
            yield return new WaitForSeconds(1);
        }
    }

    void LoseMoney(int moneyToLose = 2) {
        money -= moneyToLose;
        moneyText.text = money.ToString();

        CheckIfGameOver();
        if (moneyToLose > 2) {
            UIController.instance.showPoints(moneyToLose * -1);
        }
        if (money <= 15) {
            UIController.instance.startMoneyFlash();
        }
    }

    void GainMoney(int moneyToGain) {
        money += moneyToGain;
        moneyText.text = money.ToString();

        UIController.instance.showPoints(moneyToGain);
        if (money > 15) {
            UIController.instance.stopMoneyFlash();
        }
    }

    void FixedUpdate() {
        if (GameManager.instance.doingSetup || CameraController.instance.isZoomed) {
            return;
        }

        int horizontal = 0;
        int vertical = 0;
        horizontal = (int) Input.GetAxisRaw("Horizontal");
        vertical = (int) Input.GetAxisRaw("Vertical");

        if (horizontal != 0) {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0) {
            if (horizontal == 1) { playerDirection = 1; }
            else if (horizontal == -1) { playerDirection = 2; } 
            else if (vertical == 1) { playerDirection = 3; }
            else if (vertical == -1) { playerDirection = 0; }
            animator.enabled = true;
            animator.SetBool("isWalking", true);
            animator.SetInteger("direction", playerDirection);
            base.AttemptMove(horizontal, vertical);

            if (Input.GetKeyDown(KeyCode.Return) && money > 40 && !isMoving) {
                Vector2 end = new Vector3(transform.position.x, transform.position.y, 0) 
                    + new Vector3(horizontal * 2, vertical * 2, 0);
                
                if (CanGoThrough(end)) {
                    StartCoroutine(SmoothMovement(end));
                    LoseMoney(40);
                    SoundManager.instance.PlaySingle(goThroughSound);
                }
            }
        } else if (!isMoving) {
            animator.SetBool("isWalking", false);
            animator.enabled = false;
            spriteRenderer.sprite = sprites[playerDirection];
        }

        if (Input.GetKeyDown(KeyCode.Space) && money > 10 && !isMoving) {
            CameraController.instance.Zoom();
            Vector2 end = new Vector3(transform.position.x, transform.position.y, 0) 
                + new Vector3(0, 1, 0);
            StartCoroutine(SmoothMovement(end, true));
            LoseMoney(10);
            SoundManager.instance.PlaySingle(jumpSound);
        }
    }

    private bool CanGoThrough (Vector2 end) {
        Vector2 start = transform.position;
        RaycastHit2D[] hits = Physics2D.LinecastAll(start, end, blockingLayer);

        if (hits.Length != 1) {
            return false;
        }
        return !(Mathf.Abs(hits[0].transform.position.x - start.x) > 1) 
            && !(Mathf.Abs(hits[0].transform.position.y - start.y) > 1);
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Exit") {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        } else if (other.tag == "Chest") {
            other.gameObject.SetActive(false);
            SoundManager.instance.PlaySingle(coinSound);
            GainMoney(pointsPerChest);
        } else if (other.tag == "PileOfGold") {
            SoundManager.instance.PlaySingle(coinSound);
            other.gameObject.SetActive(false);
            GainMoney(pointsPerPileOfGold);
        } else if (other.tag == "Trapdoor") {
            isHiding = true;
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = SmoothOpacityUpdate(0);
            StartCoroutine(coroutine);
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other.tag == "Trapdoor") {
            isHiding = false;
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = SmoothOpacityUpdate(1);
            StartCoroutine(coroutine);
        }
    }

    
    protected IEnumerator SmoothOpacityUpdate (float end) {

        float difference = 1.0f;
        float speed = end == 0 ? (1/.4f) : (1/.2f);

        while (difference > .001f) {
            //Find a new position proportionally closer to the end, based on the moveTime
            float newOpacity = Mathf.Lerp(spriteRenderer.color.a, end, speed * Time.deltaTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newOpacity);

            //Recalculate the remaining distance after moving.
            difference = Mathf.Abs(spriteRenderer.color.a - end);
            yield return null;
        }
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, end);
    }


    private void Restart() {

        if (GameManager.instance.GetLevel() < 7) {
            SceneManager.LoadScene("MainScene");
        } else {
            GameManager.instance.GameOver();
        }
    }

    private void OnDisable() {
        GameManager.instance.AddPlayerMoneyPoints(money);
    }

    private void CheckIfGameOver() {
        if (money <= 0) {
            GameManager.instance.GameOver();
        }
    }
}
