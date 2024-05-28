using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public PlayerController playerController;
	public EnemyAI enemyAI;
	public CardController cardController;
	public float playerNeedTimer = 0;
	public float playerTimer = 0;
	public float enemyTimer = 0;
	public Slider playerSlider;
	public Slider enemySlider;
	public GameObject playerBtn;
	public bool isGetCard = false;
	public bool isPlayerSendCard = false;
	public bool isEnemySendCard = false;

	private static GameController instance;
	public static GameController Instance
    {
        get
        {
			return instance;
        }
    }

	void Awake()
    {
        if (instance == null)
        {
			instance = this;
        }
    }

	// Use this for initialization
	void Start () {
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		enemyAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
		playerController.thisData.chuPaiState = ChuPaiState.MayChuPai;
		isGetCard = true;
		PlayerSendCard(4);
		EnemySendCard(4);
		isPlayerSendCard = true;
		playerTimer = 20;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerAndEnemyDataSwitch();

        if (isGetCard)
        {
			enemyAI.thisData.ThisCard(enemyAI.thisData.playerAndEnemy);
			playerController.thisData.ThisCard(playerController.thisData.playerAndEnemy);
			isGetCard = false;
        }
	}

	public void PlayerAndEnemyDataSwitch()
    {
        switch (playerController.thisData.chuPaiState)
        {
			case ChuPaiState.Wait:
				playerSlider.value = playerTimer;
				break;
			case ChuPaiState.MayChuPai:
				playerSlider.value = playerTimer;
				if(isPlayerSendCard == true)
                {
					PlayerSendCard(2);
					isPlayerSendCard = false;
                }
				playerTimer -= Time.deltaTime;
				playerBtn.SetActive(true);
                if (playerTimer <= 0)
                {
                    if (playerController.thisData.thisCard.Count > playerController.thisData.currentHP)
                    {
						InitPlayeAndEnemy();
						isGetCard = true;
						playerController.thisData.chuPaiState = ChuPaiState.QiPai;
                    }
                    else
                    {
						InitPlayeAndEnemy();
						playerBtn.SetActive(false);
						playerController.thisData.chuPaiState = ChuPaiState.Wait;
						enemyAI.thisData.chuPaiState = ChuPaiState.MayChuPai;
						isEnemySendCard = true;
						isGetCard = true;
						enemyTimer = 20;
					}
                }
				break;
			case ChuPaiState.NeedOutPai:
				playerSlider.value = playerNeedTimer;
				playerNeedTimer -= Time.deltaTime;
				playerBtn.SetActive(true);
                if (playerNeedTimer <= 0)
                {
					isGetCard = true;
					playerController.thisData.isNeedOutCard = false;
                    if (enemyTimer <= 0)
                    {
						playerTimer = 20;
						playerNeedTimer = 0;
						playerController.thisData.currentNeedOutCardName = "";
						playerController.CardYesClick();
						playerController.thisData.chuPaiState = ChuPaiState.MayChuPai;
                    }
                    else
                    {
						playerNeedTimer = 0;
						playerController.thisData.currentNeedOutCardName = "";
						playerBtn.SetActive(false);
						playerController.thisData.chuPaiState = ChuPaiState.Wait;
					}
                }
				break;
			case ChuPaiState.QiPai:
				playerSlider.value = playerTimer;
                if (playerController.thisData.thisCard.Count > playerController.thisData.currentHP)
                {

                }
                else
                {
					playerBtn.SetActive(false);
					playerController.thisData.chuPaiState = ChuPaiState.Wait;
					enemyAI.thisData.chuPaiState = ChuPaiState.MayChuPai;
					isGetCard = true;
					isEnemySendCard = true;
					enemyTimer = 20;
                }
				break;
			default:
				break;
		}
		switch (enemyAI.thisData.chuPaiState)
		{
			case ChuPaiState.Wait:
				enemySlider.value = enemyTimer;
				break;
			case ChuPaiState.MayChuPai:
				enemySlider.value = enemyTimer;
				if(isEnemySendCard == true)
                {
					EnemySendCard(2);
					isEnemySendCard = false;
                }
				enemyTimer -= Time.deltaTime;
				break;
			case ChuPaiState.NeedOutPai:
				isGetCard = true;
				break;
			case ChuPaiState.QiPai:
                if (enemyAI.thisData.thisCard.Count > enemyAI.thisData.currentHP)
                {
                    //敌人的弃牌
                    if (enemyAI.thisData.thisCard[enemyAI.thisData.thisCard.Count - 1] != null)
                    {
						enemyAI.thisData.OutCardMoveTarGetPos
							(enemyAI.thisData.thisCard[enemyAI.thisData.thisCard.Count - 1]);
                    }
                }
                else
                {
					InitPlayeAndEnemy();
					isGetCard = true;
					enemyAI.thisData.chuPaiState = ChuPaiState.Wait;
					playerController.thisData.chuPaiState = ChuPaiState.MayChuPai;
					playerController.CardYesClick();
					isPlayerSendCard = true;
					playerTimer = 20;
				}
				break;
		}
	}

	public void InitPlayeAndEnemy()
    {
		playerController.thisData.currentOutCardName = "";
		enemyAI.thisData.currentOutCardName = "";
		playerController.thisData.outShaNum = 1;
		enemyAI.thisData.outShaNum = 1;
	}

	/// <summary>
	/// 给玩家发牌方法
	/// </summary>
	/// <param name="number"></param>
	public void PlayerSendCard(int number)
    {
		for(int i = 0; i < number; i++)
        {
			cardController.SendCard(PlayerAndEnemy.Player);
        }
	}
	/// <summary>
	/// 给敌人发牌方法
	/// </summary>
	/// <param name="number"></param>
	public void EnemySendCard(int number)
	{
		for (int i = 0; i < number; i++)
		{
			cardController.SendCard(PlayerAndEnemy.Enemy);
		}
	}

	/// <summary>
	/// 玩家点击出牌
	/// </summary>
	public void PlayerOutCard()
    {
		if(playerController.thisData.chuPaiState == ChuPaiState.MayChuPai && 
			playerController.thisData.currrentClickCardGo != null)
        {
			isGetCard = true;
			playerTimer = 20;
			playerNeedTimer = 0;
			playerController.thisData.OutCrad(playerController.thisData.currrentClickCardGo);
		}
		if (playerController.thisData.chuPaiState == ChuPaiState.QiPai &&
			playerController.thisData.currrentClickCardGo != null)
		{
			playerController.thisData.DisCard(playerController.thisData.currrentClickCardGo.name);
		}
		if (playerController.thisData.chuPaiState == ChuPaiState.NeedOutPai &&
			playerController.thisData.currrentClickCardGo != null)
		{
			isGetCard = true;
			playerController.thisData.OutCrad(playerController.thisData.currrentClickCardGo);
			playerController.thisData.isNeedOutCard = true;
            if (enemyTimer <= 0)
            {
				playerTimer = 20;
				playerNeedTimer = 0;
				playerController.thisData.currentNeedOutCardName = "";
				playerController.CardYesClick();
				playerBtn.SetActive(false);
				playerController.thisData.chuPaiState = ChuPaiState.MayChuPai;
            }
            else
            {
				playerNeedTimer = 0;
				playerController.thisData.currentNeedOutCardName = "";
				playerBtn.SetActive(false);
				playerController.thisData.chuPaiState = ChuPaiState.Wait;
			}
		}

	}
	/// <summary>
	/// 玩家点击取消
	/// </summary>
	public void PlayerOutOff()
    {
		if(playerController.thisData.chuPaiState == ChuPaiState.NeedOutPai)
        {
			playerController.thisData.isNeedOutCard = false;
			playerController.thisData.isOutWuXie = false;
			playerController.thisData.isWuXie = false;
			if(enemyTimer <= 0)
            {
				playerTimer = 20;
				playerController.thisData.currentNeedOutCardName = "";
				playerController.CardYesClick();
				playerNeedTimer = 0;
				playerController.thisData.chuPaiState = ChuPaiState.MayChuPai;
            }
            else
			{
				playerController.thisData.currentNeedOutCardName = "";
				playerController.CardYesClick();
				playerNeedTimer = 0;
				playerController.thisData.chuPaiState = ChuPaiState.Wait;
			}
        }
    }
	/// <summary>
	/// 结束出牌
	/// </summary>
	public void PlayerEndOutCard()
    {
		playerTimer = 0;
		playerController.thisData.chuPaiState = ChuPaiState.QiPai;

		if (playerController.thisData.chuPaiState == ChuPaiState.NeedOutPai)
		{
			playerController.thisData.isNeedOutCard = false;
			playerController.thisData.isOutWuXie = false;
			playerController.thisData.isWuXie = false;
			if (enemyTimer <= 0)
			{
				playerTimer = 20;
				playerController.thisData.currentNeedOutCardName = "";
				playerController.CardYesClick();
				playerNeedTimer = 0;
				playerController.thisData.chuPaiState = ChuPaiState.MayChuPai;
			}
			else
			{
				playerController.thisData.currentNeedOutCardName = "";
				playerController.CardYesClick();
				playerNeedTimer = 0;
				playerController.thisData.chuPaiState = ChuPaiState.Wait;
			}
		}
	}
}
