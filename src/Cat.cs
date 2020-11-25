using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour
{
    public Animator animator;
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public bool isRun;
    public bool isLeft;
    public bool isRight;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            speed += Time.deltaTime / 20;
            PlayerManager.Getinstance().distance += 1f;
            transform.Translate(transform.forward * Time.deltaTime * speed);
            Camera.main.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                isLeft = true;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                isRight = true;
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                animator.SetTrigger("isJump");

                if (GetComponent<BoxCollider>().center.y < 1.5f)
                {
                    GetComponent<BoxCollider>().center += new Vector3(0, 0.5f, 0);
                }
                Invoke("Recover", 1);
                var bg = GameObject.Find("Sound").GetComponent<AudioSource>();
                var cl = Resources.Load<AudioClip>("Sound/CatJump");
                bg.clip = cl;
                bg.Play();
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                animator.SetTrigger("isSlider");
                if (GetComponent<BoxCollider>().center.y > 0)
                {
                    GetComponent<BoxCollider>().center -= new Vector3(0, 1f, 0);
                }
                Invoke("Recover", 1);
                var bg = GameObject.Find("Sound").GetComponent<AudioSource>();
                var cl = Resources.Load<AudioClip>("Sound/CatInjured");
                bg.clip = cl;
                bg.Play();
            }
            if (isLeft)
            {
                if (transform.position.x > -1.5f && transform.position.x <= 0)
                {
                    transform.position -= new Vector3(Time.deltaTime * 6, 0, 0);
                    if (transform.position.x <= -1.5f)
                    {
                        transform.position = new Vector3(-1.5f, transform.position.y, transform.position.z);
                        isLeft = false;
                    }
                }
                else if (transform.position.x > 0 && transform.position.x <= 1.5f)
                {
                    transform.position -= new Vector3(Time.deltaTime * 6, 0, 0);
                    if (transform.position.x <= 0)
                    {
                        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
                        isLeft = false;
                    }
                }
            }
            if (isRight)
            {
                if (transform.position.x < 1.5f && transform.position.x >= 0)
                {
                    transform.position += new Vector3(Time.deltaTime * 6, 0, 0);
                    if (transform.position.x >= 1.5f)
                    {
                        transform.position = new Vector3(1.5f, transform.position.y, transform.position.z);
                        isRight = false;
                    }
                }
                else if (transform.position.x < 0 && transform.position.x >= -1.5f)
                {
                    transform.position += new Vector3(Time.deltaTime * 6, 0, 0);
                    if (transform.position.x >= 0)
                    {
                        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
                        isRight = false;
                    }
                }
            }
        }
    }
    void Recover()
    {
        GetComponent<BoxCollider>().center = new Vector3(0, 1f, 0);
    }
    //猫的触发器
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Entry")
        {
            GameObject.Find("Bg").GetComponent<AudioSource>().volume = 0.5f;
            return;
        }
        if (other.gameObject.tag == "Exit")
        {
            GameObject.Find("Bg").GetComponent<AudioSource>().volume = 1f;
            return;

        }
        if (other.gameObject.tag == "Fish")
        {
            Destroy(other.gameObject);
            PlayerManager.Getinstance().fishNum++;
            PlayerManager.Getinstance().score += 10;
            var bg = GameObject.Find("Sound").GetComponent<AudioSource>();
            var cl = Resources.Load<AudioClip>("Sound/FishboneCollection");
            bg.clip = cl;
            bg.Play();
        }
        else if(other.gameObject.tag == "Obstacle")
        {
            PlayerManager.Getinstance().hp--;
            if (PlayerManager.Getinstance().hp <= 0)
            {
                PlayerManager.Getinstance().gameStart = false;
                animator.SetTrigger("isOver");
                transform.eulerAngles = new Vector3(0, 180, 0);
                var bg = GameObject.Find("Bg").GetComponent<AudioSource>();
                var cl = Resources.Load<AudioClip>("Sound/Death");
                bg.clip = cl;
                bg.Play();
                //加载骷髅头
                var sk = Resources.Load<GameObject>("ModelPrefabs/Skeleton");
                var k= Instantiate(sk);
                k.transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
                k.transform.localEulerAngles += new Vector3(0,180,0);
                gameObject.SetActive(false);

                SceneManager.LoadScene("Lose", LoadSceneMode.Additive);
            }
            else
            {
                animator.SetTrigger("isHit");
            }
            isRun = false;

            if (PlayerManager.Getinstance().hp > 0)
            {
                Invoke("RecoverRun", 1);
            }
            var sound = GameObject.Find("Sound").GetComponent<AudioSource>();
            var so = Resources.Load<AudioClip>("Sound/CatInjured");
            sound.clip = so;
            sound.Play();
            Destroy(other.gameObject);
        }

       
    }
    void RecoverRun()
    {
        isRun = true;
    }
}
