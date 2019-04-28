using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathFind;
using System.Text.RegularExpressions;

public class Enemy : MovingObject {

    public Sprite[] sprites;

    public AudioClip seeThiefSound;

    private GameObject player;
    private GameObject exit;
    private Player playerController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int playerDirection = 0;

    private bool[,] tilesmap;
    private int tilesmapHeight;
    private int tilesmapWidth;

    private PathFind.Grid grid;

    private bool searchingForThief = false;
    private bool goingToExit = false;

    void readLevelFile () {
        string text = Tilesmaps.GetLevel(GameManager.instance.GetLevel());
        string[] lines = Regex.Split(text, "\r\n");
        tilesmapHeight = lines.Length;
        tilesmapWidth = Regex.Split(lines[0], " ").Length;
        
        tilesmap = new bool[tilesmapWidth, tilesmapHeight];
        for (int i = 0; i < lines.Length; i++)  {
            string[] cols = Regex.Split(lines[i], " ");
            for (int j = 0; j < cols.Length; j++) {
                if (cols[j] == "X") {
                    tilesmap[j, i] = false;
                } else {
                    tilesmap[j, i] = true;
                }
            }
        }
    }

    protected override void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        exit = GameObject.FindWithTag("Exit");
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<Player>();

        moveTime = 0.2f;

        base.Start();

        readLevelFile();

        grid = new PathFind.Grid(tilesmapWidth, tilesmapHeight, tilesmap);
    }

    void Update() {
        if (GameManager.instance.doingSetup) {
            return;
        }

        if (searchingForThief && playerController.playerIsHiding()) {
            goingToExit = true;
            inverseMoveTime = 1 / 0.2f;
        }

        PathFind.Point _to;
        if (goingToExit) {
            _to = new PathFind.Point(
                Mathf.RoundToInt(exit.transform.position.x),
                Mathf.RoundToInt(exit.transform.position.y));
        } else {
            _to = new PathFind.Point(
                Mathf.RoundToInt(player.transform.position.x),
                Mathf.RoundToInt(player.transform.position.y));
        }
        PathFind.Point _from = new PathFind.Point(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y));

        List<PathFind.Point> path = PathFind.Pathfinding.FindPath(grid, _from, _to);
        if (!searchingForThief && path.Count > 0 && path.Count <= 4) {
            searchingForThief = true;
            SoundManager.instance.PlaySingle(seeThiefSound);
        }
        
        if (path.Count > 0 && searchingForThief) {
            PathFind.Point target = path[0];

            int horizontal = target.x - _from.x;
            int vertical = horizontal != 0 ? 0 : target.y - _from.y;

            if (horizontal != 0 || vertical != 0) {
                if (horizontal == 1) { playerDirection = 1; }
                else if (horizontal == -1) { playerDirection = 2; } 
                else if (vertical == 1) { playerDirection = 3; }
                else if (vertical == -1) { playerDirection = 0; }
                animator.enabled = true;
                animator.SetBool("isWalking", true);
                animator.SetInteger("direction", playerDirection);
                base.AttemptMove(horizontal, vertical);
            } else if (!isMoving) {
                animator.SetBool("isWalking", false);
                animator.enabled = false;
                spriteRenderer.sprite = sprites[playerDirection];
            }
        } else if (path.Count == 0) {
            animator.SetBool("isWalking", false);
        }
    }

    // void FixedUpdate() {
    //     if (GameManager.instance.doingSetup) {
    //         return;
    //     }

    //     int horizontal = 0;
    //     int vertical = 0;
    //     horizontal = (int) Input.GetAxisRaw("Horizontal");
    //     vertical = (int) Input.GetAxisRaw("Vertical");

    //     if (horizontal != 0) {
    //         vertical = 0;
    //     }

    //     if (horizontal != 0 || vertical != 0) {
    //         if (horizontal == 1) { playerDirection = 1; }
    //         else if (horizontal == -1) { playerDirection = 2; } 
    //         else if (vertical == 1) { playerDirection = 3; }
    //         else if (vertical == -1) { playerDirection = 0; }
    //         animator.enabled = true;
    //         animator.SetBool("isWalking", true);
    //         animator.SetInteger("direction", playerDirection);
    //         base.AttemptMove(horizontal, vertical);

    //         if (Input.GetKeyDown(KeyCode.Return) &&  > 20) {
    //             Vector2 end = new Vector3(transform.position.x, transform.position.y, 0) 
    //                 + new Vector3(horizontal * 2, vertical * 2, 0);
    //             StartCoroutine(SmoothMovement(end));
    //             Lose(20);
    //         }
    //     } else if (!isMoving) {
    //         animator.SetBool("isWalking", false);
    //         animator.enabled = false;
    //         spriteRenderer.sprite = sprites[playerDirection];
    //     }

    //     if (Input.GetKeyDown(KeyCode.Space) &&  > 10 && !isMoving) {
    //         CameraController.instance.Zoom();
    //         Vector2 end = new Vector3(transform.position.x, transform.position.y, 0) 
    //             + new Vector3(0, 1, 0);
    //         StartCoroutine(SmoothMovement(end, true));
    //         Lose(10);
    //     }
    // }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Player") {
            GameManager.instance.GameOver();
        } else if (other.tag == "Exit") {
            Destroy (gameObject);
        }
    }
}
