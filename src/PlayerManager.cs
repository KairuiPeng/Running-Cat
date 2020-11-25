using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Getinstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }

    public bool gameStart;
    public Vector3 housePoint;
    public float distance;
    public int hp;
    public float score;
    public int fishNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject LoadObject(string path,Transform tr,Vector3 v3)
    {
        GameObject ob = Resources.Load<GameObject>(path);
        var go = Instantiate(ob, v3, Quaternion.identity,tr);
        return go;
    }
}
