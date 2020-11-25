using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    public  Button reStart;
    // Start is called before the first frame update
    void Start()
    {
        reStart.onClick.AddListener(()=> { SceneManager.LoadScene("MainScene"); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
