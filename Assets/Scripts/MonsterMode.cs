using UnityEngine;  
using System.Collections;
using UnityEngine.UI;

public class MonsterMode : MonoBehaviour {

    public int HP = 100;
    public bool isWaitPlayer = true;
    public Slider MonsterHealthSlider;
    public PlayerMode player;

    //动画组件  
    //private Animation mAnimation;  
    private Animator mAnim;

    // Use this for initialization  
    void Start()
    {
        mAnim = GetComponent<Animator>();
        mAnim.SetBool("Idle", true);
    }


    public void OnDamage(int mValue)
    {
        HP -= mValue;
        Debug.Log("MonsterHP:" + HP);
        MonsterHealthSlider.value -= mValue * 0.01f; 
    }

    //敌人AI算法  
    public void StartAI()
    {
        if (!isWaitPlayer)
        {
            if (HP > 20)
            {
                //80%  
                if (Random.Range(1, 5) % 5 != 1)
                {
                    mAnim.SetBool("Idle", false);
                    mAnim.SetBool("Attack", true);
                    player.OnDamage(10);
                    //ondamage  
                    isWaitPlayer = true;
                }
                //20%  
                else
                {
                    mAnim.SetBool("Idle", false);
                    mAnim.SetBool("Attack", true);
                    isWaitPlayer = true;
                }
            }
            else
            {
                mAnim.SetBool("Idle", false);
                mAnim.SetBool("Attack", true);
                isWaitPlayer = true;
            }
        }
    }   

        // Update is called once per frame  
    void Update()
    {
        if (isWaitPlayer)
        {
            mAnim.SetBool("Idle", true);
            mAnim.SetBool("Attack", false);
        }
    }  
}  