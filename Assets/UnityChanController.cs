using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{

    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;
    //Unity�������ړ�������R���|�[�l���g������(�ǉ�)
    private Rigidbody myRigidbody;
    //�O�����̑��x(�ǉ�)
    private float velocityZ = 16f;
    //�������̑��x(�ǉ�)
    private float velocityX = 10f;
    //������̑��x(�ǉ�)
    private float velocityY = 10f;
    //���E�̈ړ��ł���͈�(�ǉ�)
    private float movableRange = 3.4f;
    //����������������W��(�ǉ�)
    private float coefficient = 0.99f;
    //�Q�[���I���̔���(�ǉ�)
    private bool isEnd = false;
    //�Q�[���I�����ɕ\������e�L�X�g(�ǉ�)
    private GameObject stateText;
    //�X�R�A��\������e�L�X�g(�ǉ�)
    private GameObject scoreText;
    //���_(�ǉ�)
    private int score = 0;
    //���{�^�������̔���(�ǉ�)
    private bool isLButtonDown = false;
    //�E�{�^�������̔���(�ǉ�)
    private bool isRButtonDown = false;
    //�W�����v�{�^�������̔���(�ǉ�)
    private bool isJButtonDown = false;

    // Start is called before the first frame update
    void Start()
    {
        //Animator�R���|�[�l���g���擾
        this.myAnimator = GetComponent<Animator>();

        //����A�j���[�V�������J�n
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbody�R���|�[�l���g���擾(�ǉ�)
        this.myRigidbody = GetComponent<Rigidbody>();

        //�V�[������stateText�I�u�W�F�N�g���擾(�ǉ�)
        this.stateText = GameObject.Find("GameResultText");

        //�V�[������scoreText�I�u�W�F�N�g���擾(�ǉ�)
        this.scoreText = GameObject.Find("ScoreText");

    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���I���Ȃ�Unity�����̓�������������(�ǉ�)
        if (this.isEnd)
        {
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

        //�������̓��͂ɂ�鑬�x
        float inputVelocityX = 0;
        //������̓��͂ɂ�鑬�x(�ǉ�)
        float inputVelocityY = 0;

        //Unity��������L�[�܂��̓{�^���ɉ����č��E�Ɉړ�������(�ǉ�)
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            //�������ւ̑��x����(�ǉ�)
            inputVelocityX = -this.velocityX;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //�E�����ւ̑��x����(�ǉ�)
            inputVelocityX = this.velocityX;
        }

        //�W�����v���Ă��Ȃ����ɃX�y�[�X�܂��̓{�^���������ꂽ��W�����v����(�ǉ�)
        if ((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            //�W�����v�A�j�����Đ�(�ǉ�)
            this.myAnimator.SetBool("Jump", true);
            //������ւ̑��x����(�ǉ�)
            inputVelocityY = this.velocityY;
        }
        else
        {
            //���݂�Y���̑��x����(�ǉ�)
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //Jump�X�e�[�g�̏ꍇ��Jump��false���Z�b�g����(�ǉ�)
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //Unity�����ɑ��x��^����(�ǉ�)
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);
    }

    //�g���K�[���[�h�ő��̃I�u�W�F�N�g�ƐڐG�����ꍇ�̏���
    void OnTriggerEnter(Collider other)
    {
        //��Q���ɏՓ˂����ꍇ(�ǉ�)
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            //stateText��GAME OVER��\��(�ǉ�)
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //�S�[���n�_�ɓ��B�����ꍇ
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            //stateText��GAME CLEAR��\��(�ǉ�)
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        //�R�C���ɏՓ˂����ꍇ(�ǉ�)
        if (other.gameObject.tag == "CoinTag")
        {
            //�X�R�A�����Z(�ǉ�)
            this.score += 10;

            //ScoreText�Ɋl�������_����\��(�ǉ�)
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";

            //�p�[�e�B�N�����Đ�(�ǉ�)
            GetComponent<ParticleSystem>().Play();

            //�ڐG�����R�C���̃I�u�W�F�N�g��j��(�ǉ�)
            Destroy(other.gameObject);
        }
    }

    //�W�����v�{�^�����������ꍇ�̏���(�ǉ�)
    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }

    //�W�����v�{�^���𗣂����ꍇ�̏���(�ǉ�)
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }

    //���{�^���������������ꍇ�̏���(�ǉ�)
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }

    //���{�^���𗣂����ꍇ�̏���(�ǉ�)
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //�E�{�^���������������ꍇ�̏���(�ǉ�)
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }

    //�E�{�^���𗣂����ꍇ�̏���(�ǉ�)
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }


}
