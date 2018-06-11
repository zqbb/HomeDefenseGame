using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private Vector3 ballInitPosition;
    public Transform[] trans;
    private Vector3[] posArr;

    public Slider slider;
    public Text scoreText;
    private bool isGameOver = false;

    private int scoreNum = 0;
    public Text scoreStr;

    public static GameManager Instance;
    private float attackPower;

    public Transform gameOverBg;
    private bool hasCreateEnemy = false;
    private void Awake()
    {
        Instance = this;
    }
    void Start () {
        posArr = new Vector3[trans.Length];
        for (int i=0;i<posArr.Length;i++) {
            posArr[i]=trans[i].position;
        }
        scoreStr.text = "score:";
	}

    public void SwordAttack() {
        attackPower = Random.Range(0.01f, 0.03f);
        if (slider.value > 0.01f)
        {
            slider.value -= attackPower;
            scoreText.text = (100 * (1 - attackPower)).ToString("f2") + "/" + 100;
        }
        else {
            isGameOver = true;
            gameOverBg.gameObject.SetActive(true);
        }
    }

	void Update () {
        if(isGameOver==false)
        if (Input.GetMouseButtonDown(0)) {
            CreateBall();
        }

        if (isGameOver) {
            CancelInvoke("CreateEnemy");
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i=0;i<gos.Length;i++) {
                Destroy(gos[i]);
            }
        }

        if (hasCreateEnemy==false) {
            InvokeRepeating("CreateEnemy", 0, Random.Range(1, 4));
            hasCreateEnemy = true;
        }
	}

    void CreateEnemy() {
        int posIndex = Random.Range(0,posArr.Length);
        GameObject enmeyGo = GameObject.Instantiate(Resources.Load("Warlord") as GameObject);
        enmeyGo.transform.position = posArr[posIndex];
    }
    void CreateBall() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 cameraScreenPos = Camera.main.WorldToScreenPoint(Camera.main.transform.position);
        Vector3 mouseWorlPos=Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,cameraScreenPos.z+2f));
        GameObject ball = GameObject.Instantiate(Resources.Load("Ball") as GameObject);
        ball.transform.position = mouseWorlPos;

        ball.GetComponent<Rigidbody>().AddForce((mouseWorlPos-Camera.main.transform.position)*Random.Range(150,350),ForceMode.Acceleration);
    }

    public void GetScore() {
        scoreNum++;
        scoreStr.text = "score： " + scoreNum;
    }

    public void RestartGame() {
        isGameOver = false;
        slider.value = 1;
        scoreText.text = "100/100";
        gameOverBg.gameObject.SetActive(false);
        hasCreateEnemy = false;
        scoreStr.text = "score:";
    }

    public void ExitGame() {
        Application.Quit();
    }
}
