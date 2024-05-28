using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	public CharacterData thisData;
	public PlayerController playerController;
	public float outCardTimer = 2;
	public bool isNanMan = false;
	public bool isWanJian = false;
	// Use this for initialization
	void Awake() {
		thisData = GetComponent<CharacterData>();
		thisData.curPlayer = PlayerType.Enemy;
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		thisData.currentHP = thisData.maxHP;
	}
	
	// Update is called once per frame
	void Update ()
	{
		EnemyOutCard();
		EnemyOutCardIsTaKeEffect();
	}
    /// <summary>
    /// 敌人出牌
    /// </summary>
    /// 



    #region AI判断
    public void EnemyOutCard()
    {
		if(thisData.curRound == RoundType.ChuPai)
        {
			outCardTimer -= Time.deltaTime;
            if (outCardTimer <= 0)
            {
				if(EnemyCard() == null)
                {
					thisData.currentNeedOutCardName = "";
					thisData.isNeedOutCard = false;
					thisData.curRound = RoundType.QiPai;
					GameController.Instance.enemyTimer = 0;
					outCardTimer = 2;
                }
                else
                {
					thisData.OutCrad(EnemyCard());
					GameController.Instance.enemyTimer = 20;
					outCardTimer = 2;
                }
            }
        }
		if(thisData.curRound == RoundType.Wait)
        {
			if(playerController.thisData.curRound == RoundType.Wait)
            {
				thisData.curRound = RoundType.ChuPai;
            }
        }
    }

	/// <summary>
	/// 返回敌人列表里可以出的牌的游戏物体
	/// </summary>
	/// <returns></returns>
	public GameObject EnemyCard()
    {
		for(int i = 0; i < thisData.ownCardList.Count; i++)
        {
            if (IsHaveMayOutCard(thisData.ownCardList[i].name))
            {
				return thisData.ownCardList[i].gameObject;
            }
        }
		return null;
    }


    
    /// <summary>
    /// 判断牌是否可以出
    /// </summary>
    /// <param name="cardName"></param>
    /// <returns></returns>
    public bool IsHaveMayOutCard(string cardName)
    {
		bool TrueOrFalse = false;

		switch (cardName)
        {
			case "Jiu":
				if(thisData.isJiu == false)
                {
					TrueOrFalse = true;
                }
				break;
			case "Sha":
				if (thisData.attackDis - playerController.thisData.ziShenDis >= 0 && thisData.outShaNum > 0)
				{
					TrueOrFalse = true;
				}
				break;
			case "Tao":
                if (thisData.currentHP < thisData.maxHP)
                {
					TrueOrFalse = true;
                }
				break;
			case "GuoHeChaiQiao":
				if(thisData.outJinNangDis - playerController.thisData.ziShenDis >=0 &&
                    playerController.thisData.ownCardList.Count > 0)
                {
					TrueOrFalse = true;
				}
				break;
			case "JieDaoShaRen":
				if(playerController.thisData.isWeapon == true)
				{
					TrueOrFalse = true;
				}
				break;
			case "JueDou":
				TrueOrFalse = true;
				break;
			case "NanManRuQin":
				TrueOrFalse = true;
				break;
			case "ShunShouQianYang":
				if (thisData.outJinNangDis - playerController.thisData.ziShenDis >= 0 &&
					playerController.thisData.ownCardList.Count > 0)
				{
					TrueOrFalse = true;
				}
				break;
			case "WanJianQiFa":
				TrueOrFalse = true;
				break;
			case "WuZhongShengYou":
				TrueOrFalse = true;
				break;
			case "BaGuaZhen":
				TrueOrFalse = true;
				break;
			case "BaiYinShiZi":
				TrueOrFalse = true;
				break;
			case "CiXiongShuangJian":
				TrueOrFalse = true;
				break;
			case "GuanShiFu":
				TrueOrFalse = true;
				break;
			case "JiaMa":
				TrueOrFalse = true;
				break;
			case "JianMa":
				TrueOrFalse = true;
				break;
			case "QiLinCardng":
				TrueOrFalse = true;
				break;
			case "QingGangJian":
				TrueOrFalse = true;
				break;
			case "QingLongYanYueDao":
				TrueOrFalse = true;
				break;
			case "ZhangBaSheMao":
				TrueOrFalse = true;
				break;
			case "ZhuGeLianNu":
				TrueOrFalse = true;
				break;
			case "ZhuQueYuShan":
				TrueOrFalse = true;
				break;
		}

		return TrueOrFalse;
    }

	/// <summary>
	/// 敌人打出的牌是否生效
	/// </summary>
	public void EnemyOutCardIsTaKeEffect()
    {
		//杀
        if (playerController.isEnemyOutSha)
        {
            if (playerController.thisData.curRound != RoundType.NeedOutPai)
            {
				if (playerController.thisData.isNeedOutCard)
				{
					playerController.isEnemyOutSha = false;
					playerController.thisData.isNeedOutCard = false;
				}
                else
                {
					if(playerController.thisData.isJueDou == true||thisData.isJueDou == true)
                    {
						playerController.thisData.Hit(1);
						playerController.isEnemyOutSha = false;
						playerController.thisData.isJueDou = false;
						thisData.isJueDou = false;
                    }
                    else
                    {
						if(thisData.isJiu == true)
                        {
							playerController.thisData.Hit(2);
							thisData.isJiu = false;
							playerController.isEnemyOutSha = false;
                        }
                        else
						{
							playerController.thisData.Hit(1);
							playerController.isEnemyOutSha = false;
						}
                    }
                }
            }
        }
        //过河拆桥
        if (playerController.isEnemyOutGuoHe)
        {
			if(playerController.thisData.curRound != RoundType.NeedOutPai)
            {
                if (playerController.thisData.isOutWuXie)
                {
					playerController.thisData.isOutWuXie = false;
					playerController.isEnemyOutGuoHe = false;
                }
                else
                {
					playerController.thisData.OutCardMoveToDisCard(playerController.thisData.ownCardList
						[Random.Range(0, playerController.thisData.ownCardList.Count)]);
					playerController.isEnemyOutGuoHe = false;
                }
            }
        }
        //顺手牵羊
        if (playerController.isEnemyOutShunShou)
        {
			if(playerController.thisData.curRound != RoundType.NeedOutPai)
            {
				if (playerController.thisData.isOutWuXie)
				{
					playerController.thisData.isOutWuXie = false;
					playerController.isEnemyOutShunShou = false;
                }
                else
                {
                    if (playerController.thisData.ownCardList.Count > 0)
                    {
						playerController.thisData.EnemyCardMovePlayerCard(playerController.thisData.ownCardList
							[Random.Range(0, playerController.thisData.ownCardList.Count)], GameObject.Find
							("EnemyCardList"), PlayerType.Enemy);
                    }
					playerController.isEnemyOutShunShou = false;
                }
			}
        }
		//借刀杀人
		if (playerController.isEnemyOutJieDao)
		{
			if (playerController.thisData.curRound != RoundType.NeedOutPai)
			{
				if (playerController.thisData.isOutWuXie)
				{
					playerController.thisData.isOutWuXie = false;
					playerController.isEnemyOutJieDao = false;
				}
				else
				{
					playerController.thisData.EnemyCardMovePlayerCard(playerController.thisData.weaponCard,
						GameObject.Find("EnemyCardList"), PlayerType.Enemy);
					playerController.isEnemyOutJieDao = false;
				}
			} 
		}
        //决斗
        if (playerController.isEnemyOutJueDou)
        {
			if(playerController.thisData.curRound!= RoundType.NeedOutPai && 
				thisData.isJueDou== false && playerController.thisData.isJueDou == false)
            {
                if (playerController.thisData.isOutWuXie)
                {
					playerController.thisData.isOutWuXie = false;
					playerController.isEnemyOutJueDou = false;
                }
                else
                {
					thisData.isJueDou = true;
					playerController.isEnemyOutJueDou = false;
					playerController.EnemyPlayCard("JueDou");
                }
            }
			if(playerController.thisData.curRound != RoundType.NeedOutPai && 
				(thisData.isJueDou == true|| playerController.thisData.isJueDou == true))
            {
                if (playerController.thisData.isNeedOutCard)
                {
					PlayerPlayCard("Sha", 1);
					playerController.isEnemyOutJueDou = false;
					playerController.thisData.isNeedOutCard = false;
                }
                else
                {
					playerController.thisData.Hit(1);
					thisData.isJueDou = false;
					playerController.thisData.isJueDou = false;
					playerController.isEnemyOutJueDou = false;
                }
            }
        }
        //南蛮入侵
        if (playerController.isEnemyOutNanMan)
        {
			if(playerController.thisData.curRound != RoundType.NeedOutPai && isNanMan == false)
            {
                if (playerController.thisData.isOutWuXie)
                {
					playerController.thisData.isOutWuXie = false;
					playerController.isEnemyOutNanMan = false;
                }
                else
                {
					isNanMan = true;
					playerController.isEnemyOutNanMan = false;
					playerController.EnemyPlayCard("NanManRuQin");
                }
            }
			if(playerController.thisData.curRound != RoundType.NeedOutPai && isNanMan == true)
            {
                if (playerController.thisData.isNeedOutCard)
                {
					isNanMan = false;
					playerController.isEnemyOutNanMan = false;
					playerController.thisData.isNeedOutCard = false;
                }
                else
                {
					playerController.thisData.Hit(1);
					isNanMan = false;
					playerController.isEnemyOutNanMan = false;
                }
            }
		}
		//万箭齐发
		if (playerController.isEnemyOutWanJian)
		{
			if (playerController.thisData.curRound != RoundType.NeedOutPai && isWanJian == false)
			{
				if (playerController.thisData.isOutWuXie)
				{
					playerController.thisData.isOutWuXie = false;
					playerController.isEnemyOutWanJian = false;
				}
				else
				{
					isWanJian = true;
					playerController.isEnemyOutWanJian = false;
					playerController.EnemyPlayCard("WanJianQiFa");
				}
			}
			if (playerController.thisData.curRound != RoundType.NeedOutPai && isWanJian == true)
			{
				if (playerController.thisData.isNeedOutCard)
				{
					isWanJian = false;
					playerController.isEnemyOutWanJian = false;
					playerController.thisData.isNeedOutCard = false;
				}
				else
				{
					playerController.thisData.Hit(1);
					isWanJian = false;
					playerController.isEnemyOutWanJian = false;
				}
			}
		}
        //无中生有
        if (playerController.isEnemyOutWuZhong)
        {
			if(playerController.thisData.curRound != RoundType.NeedOutPai)
            {
                if (playerController.thisData.isOutWuXie)
				{
					playerController.thisData.isOutWuXie = false;
					playerController.isEnemyOutWuZhong = false;
                }
                else
                {
					GameController.Instance.EnemySendCard(2);
					playerController.isEnemyOutWuZhong = false;
                }
            }
        }
	}


	/// <summary>
	/// 需要出的牌
	/// </summary>
	/// <param name="name"></param>
	public void ReceiveCardPlay(string name)
    {
		thisData.currentNeedOutCardName = name;
        if (thisData.ownCardList.Count > 0)
        {
			for(int i = 0; i < thisData.ownCardList.Count; i++)
            {
				if(thisData.ownCardList[i].name == name)
                {
					thisData.curRound = RoundType.NeedOutPai;
					thisData.OutCrad(thisData.ownCardList[i]);
					thisData.isNeedOutCard = true;
                }
            }
        }
    }
    #endregion

    /// <summary>
    /// 接收玩家出的牌
    /// </summary>
    /// <param name="name"></param>
    public void PlayerPlayCard(string name ,int damage)
    {
        switch (name)
        {
			case "Sha":
				if(playerController.thisData.isJueDou == true||thisData.isJueDou == true)
                {
					ReceiveCardPlay("Sha"); 
					if (thisData.isNeedOutCard == true)
					{
						thisData.currentNeedOutCardName = "";
						thisData.isNeedOutCard = false;
					}
					else
					{
						thisData.Hit(damage);
						playerController.thisData.isJueDou = false;
						thisData.isJueDou = false;
					}
				}
                else
				{
					ReceiveCardPlay("Shan");
					if(thisData.isNeedOutCard == true)
                    {
						thisData.currentNeedOutCardName = "";
						thisData.isNeedOutCard = false;
                    }
                    else
                    {
						thisData.Hit(damage);
						thisData.currentNeedOutCardName = "";
					}
				}
				break;
			case "GuoHeChaiQiao":
				ReceiveCardPlay("WuXieKeJi");
                if (thisData.isOutWuXie)
                {
					OutWuXieKeJi();
				}
                else
                {
					thisData.isChouPai = true;
                }
				break;
			case "JieDaoShaRen":
				ReceiveCardPlay("WuXieKeJi");
                if (thisData.isOutWuXie)
                {
					OutWuXieKeJi();
                }
                else
                {
					thisData.EnemyCardMovePlayerCard(thisData.weaponCard, GameObject.Find("PlayerCardList"), PlayerType.Player);
                }
				break;
			case "JueDou":
				ReceiveCardPlay("WuXieKeJi");
                if (thisData.isOutWuXie)
                {
					OutWuXieKeJi();
                }
                else
                {
					ReceiveCardPlay("Sha");
					playerController.thisData.isJueDou = true;
					thisData.isJueDou = true;
					if (thisData.isNeedOutCard == true)
					{
						thisData.currentNeedOutCardName = "";
						thisData.isNeedOutCard = false;
					}
                    else
                    {
						thisData.Hit(damage);
						playerController.thisData.isJueDou = false;
						thisData.isJueDou = false;
					}
				}
				break;
			case "NanManRuQin":
				ReceiveCardPlay("WuXieKeJi");
				if (thisData.isOutWuXie)
				{
					OutWuXieKeJi();
				}
                else
                {
					ReceiveCardPlay("Sha"); 
					if (thisData.isNeedOutCard == true)
					{
						thisData.currentNeedOutCardName = "";
						thisData.isNeedOutCard = false;
					}
					else
					{
						thisData.Hit(damage);
					}
				}
				break;
			case "ShunShouQianYang":
				ReceiveCardPlay("WuXieKeJi");
				if (thisData.isOutWuXie)
				{
					OutWuXieKeJi();
				}
				else
				{
					thisData.isChouPai = true;
				}
				break;
			case "WanJianQiFa":
				ReceiveCardPlay("WuXieKeJi");
				if (thisData.isOutWuXie)
				{
					OutWuXieKeJi();
				}
				else
				{
					ReceiveCardPlay("Shan");
					if (thisData.isNeedOutCard == true)
					{
						thisData.currentNeedOutCardName = "";
						thisData.isNeedOutCard = false;
					}
					else
					{
						thisData.Hit(damage);
					}
				}
				break;
			case "WuXieKeJi":
				ReceiveCardPlay("WuXieKeJi");
				break;
			case "WuZhongShengYou":
				ReceiveCardPlay("WuXieKeJi");
				if (thisData.isOutWuXie)
				{
					OutWuXieKeJi();
				}
				else
				{
					GameController.Instance.PlayerSendCard(2);
				}
				break;
			default:
				break;

		}
    }

	/// <summary>
	/// 打出无懈可以
	/// </summary>
	public void OutWuXieKeJi()
    {
		thisData.currentNeedOutCardName = "";
		thisData.isNeedOutCard = false;
		playerController.thisData.curRound = RoundType.NeedOutPai;
		GameController.Instance.playerNeedTimer = 10;
		playerController.thisData.isNeedOutCard = true;
		thisData.isOutWuXie = false;
    }
}
