using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour {

    public Text pointsUpdateText;
    public Text moneyText;

    public static UIController instance = null;
    private float inverseMoveTime = 1/0.1f;
    private IEnumerator coroutine;

    private bool isFlashing = false;


    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void startMoneyFlash() {
        if (!isFlashing) {
            isFlashing = true;
            moneyText.color = new Color32(236, 43, 43, 255);
            coroutine = SmoothFlash(0);
            StartCoroutine(coroutine);
        }
    }

    public void stopMoneyFlash() {
        if (isFlashing) {
            isFlashing = false;
            moneyText.color = new Color32(255, 255, 255, 255);
            StopCoroutine(coroutine);
        }
    }

    protected IEnumerator SmoothFlash (float end) {

        float difference = 1.0f;

        while (difference > .001f) {
            //Find a new position proportionally closer to the end, based on the moveTime
            float newOpacity = Mathf.Lerp(moneyText.color.a, end, inverseMoveTime * Time.deltaTime);
            moneyText.color = new Color(moneyText.color.r, moneyText.color.g, moneyText.color.b, newOpacity);

            //Recalculate the remaining distance after moving.
            difference = Mathf.Abs(newOpacity - end);
            yield return null;
        }

        end = 1 - end;
        coroutine = SmoothFlash(end);
        StartCoroutine(coroutine);
    }

    public void showPoints(int points) {
        pointsUpdateText.text = (points > 0 ? "+" : "") + points;
        pointsUpdateText.enabled = true;

        Invoke ("hidePoints", 1);
    }

    private void hidePoints() {
        pointsUpdateText.enabled = false;
    }
 }
