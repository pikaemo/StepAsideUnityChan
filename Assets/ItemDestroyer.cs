using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    //���j�e�B�����̃I�u�W�F�N�g
    private GameObject mainCamera;

    //�I�u�W�F�N�g��j������n�_��z���W
    private float objectDestroyPositionZ;


    // Start is called before the first frame update
    void Start()
    {
        //�J�����̃I�u�W�F�N�g���擾
        this.mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        //�I�u�W�F�N�g��j������n�_��z���W�̎w��
        this.objectDestroyPositionZ = mainCamera.transform.position.z;

        //��ʊO�ɏo����I�u�W�F�N�g��j������
        if(this.objectDestroyPositionZ > this.gameObject.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
}
