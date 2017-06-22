using UnityEngine;  
using System.Collections;  
   
public class TurnBaseControl : MonoBehaviour {  
   
        //动画组件  
    private Animator mAnim;

    //定义玩家及敌人  
    public Transform mPlayer;
    public Transform mEnemy;

    //定义玩家及敌人脚本类  
    private PlayerMode playerScript;
    private MonsterMode enemyScript;

    //默认操作状态为玩家操作  
    private OperatorState mState = OperatorState.Player;

    //定义操作状态枚举  
    public enum OperatorState
    {
        Quit,
        EnemyAI,
        Player
    }

    // Use this for initialization  
    void Start()
    {
        mAnim = mPlayer.GetComponent<Animator>();

        //获取玩家及敌人脚本类  
        playerScript = mPlayer.GetComponent<PlayerMode>();
        enemyScript = mEnemy.GetComponent<MonsterMode>();
    }

    //UI延迟4.5秒调出  
    IEnumerator WaitUI()
    {
        yield return new WaitForSeconds(4.5F);
        enemyScript.isWaitPlayer = true;
        playerScript.ifUIshow = true;
    }


    IEnumerator WaitAI()
    {
        yield return new WaitForSeconds(2.0F);
        enemyScript.isWaitPlayer = false;
    }

    //为怪物AI延迟3秒  
    IEnumerator UpdateLater()
    {
        yield return new WaitForSeconds(3.0F);
        //敌人停止等待  
        enemyScript.isWaitPlayer = false;
        //敌人执行AI  
        enemyScript.StartAI();
    }

        // Update is called once per frame  
    void Update()
    {
        //如果敌我双方有一方生命值为0，则游戏结束  
        if (playerScript.HP == 0)
        {
            mState = OperatorState.Quit;
            Debug.Log("游戏失败");
        }
        else if (enemyScript.HP == 0)
        {
            mState = OperatorState.Quit;
            Debug.Log("游戏胜利");
        }
        else
        {
            switch (mState)
            {
                case OperatorState.Player:
                    if (!playerScript.isWaitPlayer)
                    {
                        StartCoroutine("UpdateLater");
                        StartCoroutine("WaitUI");
                        mState = OperatorState.EnemyAI;
                    }
                    break;
                case OperatorState.EnemyAI:
                    if (enemyScript.isWaitPlayer)
                    {
                        StartCoroutine("WaitAI");
                        playerScript.isWaitPlayer = true;
                        mState = OperatorState.Player;

                        mAnim.SetBool("Attack", false);
                        mAnim.SetBool("Idle", true);
                    }
                    break;
            }
        }  
    }  
}  