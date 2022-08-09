using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    //ユニティちゃんのオブジェクト
    private GameObject mainCamera;

    //オブジェクトを破棄する地点のz座標
    private float objectDestroyPositionZ;


    // Start is called before the first frame update
    void Start()
    {
        //カメラのオブジェクトを取得
        this.mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクトを破棄する地点のz座標の指定
        this.objectDestroyPositionZ = mainCamera.transform.position.z;

        //画面外に出たらオブジェクトを破棄する
        if(this.objectDestroyPositionZ > this.gameObject.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
}
