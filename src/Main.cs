using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject streets;
    public Button runBtn;
    public Text distanceText;
    public Text scoreText;
    public Text fishNumText;
    public Image[] hpImages;
    public AudioSource bg;
    private Cat mainCat;

    // Start is called before the first frame update
    void Start()
    {
        runBtn.onClick.AddListener(GameStart);
        var cat = PlayerManager.Getinstance().LoadObject("ModelPrefabs/Cat", null, Vector3.zero);
        cat.transform.eulerAngles = new Vector3(0, 180, 0);
        mainCat = cat.GetComponent<Cat>();

        //PlayerManager.Getinstance().LoadObject("ModelPrefabs/IndustrialWarehouse01", streets.transform, PlayerManager.Getinstance().housePoint);
        //PlayerManager.Getinstance().housePoint += new Vector3(0, 0, 18);
        //PlayerManager.Getinstance().LoadObject("ModelPrefabs/IndustrialWarehouse02", streets.transform, PlayerManager.Getinstance().housePoint);
        //PlayerManager.Getinstance().housePoint += new Vector3(0, 0, 18);
    }

    private void GameStart()
    {
        runBtn.gameObject.SetActive(false);
        mainCat.transform.eulerAngles = new Vector3(0, 0, 0);
        mainCat.animator.SetTrigger("isStart");

        Invoke("Run", 3);
        var cl = Resources.Load<AudioClip>("Sound/Runing");
        bg.clip = cl;
        bg.Play();
    }

    void Run()
    {
        PlayerManager.Getinstance().gameStart = true;
        mainCat.isRun = true;
        //StartCoroutine(CreatOb());
    }
    // Update is called once per frame
    void Update()
    {
        distanceText.text = (PlayerManager.Getinstance().distance /10).ToString();
        scoreText.text = PlayerManager.Getinstance().score.ToString();
        fishNumText.text = PlayerManager.Getinstance().fishNum.ToString();
        for (int i = 0; i < hpImages.Length; i++)
        {
            if (PlayerManager.Getinstance().hp == i)
            {
                hpImages[i].gameObject.SetActive(false);
            }
        }
        if (PlayerManager.Getinstance().gameStart)
        {
            //胜利距离设置
            if (PlayerManager.Getinstance().distance >= 1000)
            {
                PlayerManager.Getinstance().gameStart = false;
                mainCat.isRun = false;
                mainCat.GetComponent<Animator>().SetTrigger("isOver");
                mainCat.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
                //加载胜利bgm
                var bg = GameObject.Find("Bg").GetComponent<AudioSource>();
                var cl = Resources.Load<AudioClip>("Sound/Death");
                bg.clip = cl;
                bg.Play();
                //跳转胜利场景
                SceneManager.LoadScene("Win", LoadSceneMode.Additive);
            }
        }
    } 
    
}
