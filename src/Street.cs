using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour
{
    public int type;
   
    private void Awake()
    {
        Destroy(gameObject,50);
    }
    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case 1:
                CreatOb("ObstacleBin");
                break;
            case 2:
                CreatOb("ObstacleHighBarrier",true);
                break;
            case 3:
                CreatOb("ObstacleLowBarrier", true);
                break;
            case 4:
                CreatOb("ObstacleRoadworksBarrier", true);
                break;
            case 5:
                CreatOb("ObstacleRoadworksCone");
                break;
            case 6:
                CreatOb("ObstacleWheelyBin");
                break;
            default:
                break;
        }
    }

    private void CreatOb(string name,bool isMiddle =false) 
    {
        var ob = PlayerManager.Getinstance().LoadObject("ModelPrefabs/Obstacles/"+ name, transform,Vector3.zero);
        var a = Random.Range(-1, 2);
        if (isMiddle) a = 0;
        ob.transform.localPosition = new Vector3(-1.5f * a, 0, 0);
        CreatFish(ob.transform);
    }


    private void CreatFish(Transform ob)
    {
        Vector3 vector3 = ob.localPosition;
        var b = Random.Range(-1, 2);
        vector3.x = b*-1.5f;
        vector3.y = 1;
        for (int i = 0; i < 10; i++)
        {
            vector3 += new Vector3(0,0,-1);
            var a= Instantiate(Resources.Load<GameObject>("ModelPrefabs/Fish"));
            a.transform.SetParent(ob);
            a.transform.localPosition = vector3;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
