using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMode : MonoBehaviour {

    public int HP = 100;
    public bool isWaitPlayer = true;
    public bool ifUIshow = true;
    public Slider PlayerHealthSlider;
    public MonsterMode monster;

    private Button btnAttack;

    private Animator mAnim;

	// Use this for initialization
	void Start () {
        mAnim = GetComponent<Animator>();
        mAnim.SetBool("Idle", true);
        mAnim.SetBool("Attack", false);
        mAnim.SetBool("Run", false);
        mAnim.SetBool("Skill", false);
    }

    public void OnDamage(int mValue)
    {
        HP -= mValue;
        Debug.Log("PlayerHP:" + HP);
        PlayerHealthSlider.value -= mValue * 0.01f;
    }

    private void OnGUI()
    {
     
        if (isWaitPlayer || ifUIshow)
        {
            GUI.Window(0, new Rect(Screen.width / 2 + 150, Screen.height / 2 + 150, 200, 200), InitWindow, "请选择技能");
        }
    }

    void InitWindow(int ID)
    {
        if(GUI.Button(new Rect(0, 20, 200, 30), "突刺"))
        {
            Attack();
        }

        if (GUI.Button(new Rect(0, 50, 200, 30), "休息"))
        {
            Waitfor();
        }
    }

    void Attack()
    {
        mAnim.SetBool("Attack", true);
        mAnim.SetBool("Skill", false);
        mAnim.SetBool("Idle", false);  
        mAnim.SetBool("Run", false);
        isWaitPlayer = false;
        ifUIshow = false;
        monster.OnDamage(15);
    }


    void Waitfor()
    {
        mAnim.SetBool("Skill", true);
        mAnim.SetBool("Attack", false);
        mAnim.SetBool("Run", false);
        mAnim.SetBool("Idle", false);
        //交换操作权  
        isWaitPlayer = false;
        ifUIshow = false;
    }

    // Update is called once per frame
    void Update () {
      
    }


}
