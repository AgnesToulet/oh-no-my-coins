using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {
    public LayerMask blockingLayer;
    protected float moveTime = 0.18f;

    private BoxCollider2D boxCollider;
    protected Rigidbody2D rb2D;
    protected float inverseMoveTime;
    protected bool isMoving;

    protected virtual void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    //Move returns true if it is able to move and false if not. 
    //Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
    protected bool Move (int xDir, int yDir, out RaycastHit2D hit) {

        //Store start position to move from, based on objects current transform position.
        Vector2 start = transform.position;

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector2 end = start + new Vector2 (xDir, yDir);
        Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

        //Cast a line from start point to end point checking collision on blockingLayer.
        Vector3 realEndPosition = newPosition + new Vector3(xDir, 0, 0) * GetComponent<Collider2D>().bounds.size.x / 2 
            + new Vector3(0, yDir, 0) * GetComponent<Collider2D>().bounds.size.y / 2;

        hit = Physics2D.Linecast (start, realEndPosition, blockingLayer);

        if (isMoving) return false;

        // if (hit.transform != null) {
        //     rb2D.MovePosition (new Vector3(hit.point.x, hit.point.y, 0) + new Vector3(-xDir, 0, 0) * GetComponent<Collider2D>().bounds.size.x / 2 
        //         + new Vector3(0, -yDir, 0) * GetComponent<Collider2D>().bounds.size.y / 2);
        // } else 
        if (hit.transform == null) {
            // rb2D.MovePosition (newPosition);
            StartCoroutine(SmoothMovement(end));
            //Return true to say that Move was successful
            return true;
        }

        //If something was hit, return false, Move was unsuccesful.
        return false;
    }


    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement (Vector3 end, bool jumpDown = false) {
        isMoving = true;

        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon) {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition (newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
        isMoving = false;

        if (jumpDown) {
            end = new Vector3(transform.position.x, transform.position.y, 0) 
                + new Vector3(0, -1, 0);
            StartCoroutine(SmoothMovement(end));
        }
    }


    //The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
    //AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
    protected virtual void AttemptMove (int xDir, int yDir) {
        //Hit will store whatever our linecast hits when Move is called.
        RaycastHit2D hit;

        //Set canMove to true if Move was successful, false if failed.
        bool canMove = Move (xDir, yDir, out hit);

        //Check if nothing was hit by linecast
        if (hit.transform == null) {
            //If nothing was hit, return and don't execute further code.
            return;
        }

        // //Get a component reference to the component of type T attached to the object that was hit
        // T hitComponent = hit.transform.GetComponent <T> ();

        // //If canMove is false and hitComponent is not equal to null, meaning MovingObject is blocked and has hit something it can interact with.
        // if (!canMove && hitComponent != null) {
        //     //Call the OnCantMove function and pass it hitComponent as a parameter.
        //     OnCantMove (hitComponent);
        // }
    }
}
