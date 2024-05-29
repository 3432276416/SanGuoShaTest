using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    #region 参数
    public CharacterData thisData;
	public EnemyAI enemyAI;
	public Text qiPaiText;
	public Text chuPaiText;
	public bool isEnemyOutSha = false;   
	public bool isEnemyOutJieDao = false;
	public bool isEnemyOutGuoHe = false;
	public bool isEnemyOutJueDou = false;
	public bool isEnemyOutNanMan = false;
	public bool isEnemyOutShunShou = false;
	public bool isEnemyOutWanJian = false;
	public bool isEnemyOutWuZhong = false;
    #endregion
    // Use this for initialization

    #region 初始化
	void initSetUp()
	{
        thisData = GetComponent<CharacterData>();
        thisData.curPlayer = PlayerType.Player;
        enemyAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
        qiPaiText = GameObject.Find("QiPaiText").GetComponent<Text>();
        chuPaiText = GameObject.Find("ChuPaiText").GetComponent<Text>();
        qiPaiText.gameObject.SetActive(false);
        chuPaiText.gameObject.SetActive(false);
        thisData.currentHP = thisData.maxHP;
    }
    #endregion

    void Awake () {
		initSetUp();
	}
	
	// Update is called once per frame
	void Update () 
	{
		NeedOutCardText(thisData.currentNeedOutCardName);
		PlayerCardYesAndNoClick();
	}
	/// <summary>
	/// 需要出牌时更新出Text文本显示
	/// </summary>
	/// <param name="name"></param>
	public void NeedOutCardText(string name)
    {
        switch (name)
        {
			case "Sha":
				if(thisData.isJueDou == true || enemyAI.thisData.isJueDou == true || enemyAI.isNanMan == true)
                {
					chuPaiText.text = "请出杀";
                }
                else
				{
					chuPaiText.text = "请出闪";
				}
				break;
			case "Shan":
				chuPaiText.text = "请出闪";
				break;
			case "GuoHeChaiQiao":
				if(thisData.isWuXie == true)
				{
					chuPaiText.text = "请出无懈可击";
				}
				break;
			case "JieDaoShaRen":
				if (thisData.isWuXie == true)
				{
					chuPaiText.text = "请出无懈可击";
				}
				break;
			case "JueDou":
				if (thisData.isWuXie == true)
				{
					chuPaiText.text = "请出无懈可击";
				}
				break;
			case "NanManRuQin":
				if (thisData.isWuXie == true)
				{
					chuPaiText.text = "请出无懈可击";
				}
				break;
			case "ShunShouQianYang":
				if (thisData.isWuXie == true)
				{
					chuPaiText.text = "请出无懈可击";
				}
				break;
			case "WanJianQiFa":
				if (thisData.isWuXie == true)
				{
					chuPaiText.text = "请出无懈可击";
                }
                else
                {
					chuPaiText.text = "请出闪";
				}
				break;
			case "WuXieKeJi":
				if (thisData.isWuXie == true)
				{
					chuPaiText.text = "请出无懈可击";
				}
				break;
			case "WuZhongShengYou":
				if (thisData.isWuXie == true)
				{
					chuPaiText.text = "请出无懈可击";
				}
				break;
			default:
				chuPaiText.text = "请出牌";
				break;
		}
    }

	/// <summary>
	/// 玩家的牌是否可以点击
	/// </summary>
	public void PlayerCardYesAndNoClick()
    {
        switch (thisData.curRound)
        {
			case RoundType.Wait:
				qiPaiText.gameObject.SetActive(false);
				chuPaiText.gameObject.SetActive(false);
				CardNoClick();
				break;
			case RoundType.ChuPai:
				chuPaiText.gameObject.SetActive(true);
				qiPaiText.gameObject.SetActive(false);
				if (thisData.attackDis-enemyAI.thisData.ziShenDis < 0 || thisData.outShaNum < 1)
                {
					DesCardNoClick("Sha");
                }
				if(thisData.outJinNangDis - enemyAI.thisData.ziShenDis<0||(enemyAI.thisData.ownCardList.Count ==0
					&& enemyAI.thisData.isWeapon == false && enemyAI.thisData.isArmor == false&&
					enemyAI.thisData.isJiaMa == false&& enemyAI.thisData.isJianMa == false))
                {
					DesCardNoClick("ShunShouQianYang");
				}
				if (enemyAI.thisData.ownCardList.Count == 0 && enemyAI.thisData.isWeapon == false && 
					enemyAI.thisData.isArmor == false && enemyAI.thisData.isJiaMa == false && 
					enemyAI.thisData.isJianMa == false)
				{
					DesCardNoClick("GuoHeChaiQiao");
				}
				if(enemyAI.thisData.isWeapon == false)
                {
					DesCardNoClick("JieDaoShaRen");
				}
				if(thisData.isWuXie == false)
                {
					DesCardNoClick("WuXieKeJi");
                }
                else
                {
					DesCardYesClick("WuXieKeJi");
                }
                if (thisData.currentHP >= thisData.maxHP)
                {
					DesCardNoClick("Tao");
                }
                else
				{
					DesCardYesClick("Tao");
				}
				DesCardNoClick("Shan");
				break;
			case RoundType.NeedOutPai:
				chuPaiText.gameObject.SetActive(true);
				qiPaiText.gameObject.SetActive(false);
				DesCardYesClick(thisData.currentNeedOutCardName);
				if (thisData.isWuXie == false)
				{
					DesCardNoClick("WuXieKeJi");
				}
				else
				{
					DesCardYesClick("WuXieKeJi");
				}
				break;
			case RoundType.QiPai:
				qiPaiText.gameObject.SetActive(true);
				chuPaiText.gameObject.SetActive(false);
				qiPaiText.text = "请弃掉[" + (thisData.ownCardList.Count - thisData.currentHP) + "]张牌.";
				CardYesClick();
				break;
		}
    }


    #region 设置牌的点击情况
    /// <summary>
    /// 让牌不可以点击
    /// </summary>
    public void CardNoClick()
    {
		GameController.Instance.isGetCard = true;
		for(int i = 0; i < thisData.ownCardList.Count; i++)
        {
            if (thisData.ownCardList[i] != null && thisData.ownCardList[i].GetComponent<Toggle>() != null)
            {
				thisData.ownCardList[i].GetComponent<Toggle>().interactable = false;
			}
        }
	}
	/// <summary>
	/// 让牌可以点击
	/// </summary>
	public void CardCanClick()
	{
		GameController.Instance.isGetCard = true;
		for (int i = 0; i < thisData.ownCardList.Count; i++)
		{
			if (thisData.ownCardList[i] != null && thisData.ownCardList[i].GetComponent<Toggle>() != null)
			{
				thisData.ownCardList[i].GetComponent<Toggle>().interactable = true;
			}
		}
	}
	/// <summary>
	/// 让指定牌不可以点击
	/// </summary>
	public void setOneCardNoClick(string Name)
	{
		GameController.Instance.isGetCard = true;
		for (int i = 0; i < thisData.ownCardList.Count; i++)
		{
			if (thisData.ownCardList[i] != null && thisData.ownCardList[i].GetComponent<Toggle>() != null && 
				thisData.ownCardList[i].name == Name)
			{
				thisData.ownCardList[i].GetComponent<Toggle>().interactable = false;
			}
		}
	}
	/// <summary>
	/// 让指定牌可以点击
	/// </summary>
	public void setOneCardCanClick(string Name)
	{
		GameController.Instance.isGetCard = true;
		for (int i = 0; i < thisData.ownCardList.Count; i++)
		{
			if (thisData.ownCardList[i] != null && thisData.ownCardList[i].GetComponent<Toggle>() != null &&
				thisData.ownCardList[i].name == Name)
			{
				thisData.ownCardList[i].GetComponent<Toggle>().interactable = true;
			}
		}
	}
    #endregion


    /// <summary>
    /// 敌人出的什么牌
    /// </summary>
    /// <param name="name"></param>
    public void EnemyPlayCard(string name)
    {
		switch (name)
		{
			case "Sha":
				if(thisData.isJueDou == true|| enemyAI.thisData.isJueDou == true)
                {
					isEnemyOutSha = true;
					SetChuPaiZhuangTai("Sha");
                }
                else
				{
					isEnemyOutSha = true;
					SetChuPaiZhuangTai("Shan");
				}
				break;
			case "GuoHeChaiQiao":
				isEnemyOutGuoHe = true;
				SetChuPaiZhuangTai("WuXieKeJi");
				break;
			case "JieDaoShaRen":
				isEnemyOutJieDao = true;
				SetChuPaiZhuangTai("WuXieKeJi");
				break;
			case "JueDou":
				if(thisData.isJueDou == true || enemyAI.thisData.isJueDou == true)
                {
					isEnemyOutJueDou = true;
					SetChuPaiZhuangTai("Sha");
                }
                else
				{
					isEnemyOutJueDou = true;
					SetChuPaiZhuangTai("WuXieKeJi");
				}
				break;
			case "NanManRuQin":
				if (enemyAI.isNanMan == true)
				{
					isEnemyOutNanMan = true;
					SetChuPaiZhuangTai("Sha");
				}
				else
				{
					isEnemyOutNanMan = true;
					SetChuPaiZhuangTai("WuXieKeJi");
				}
				break;
			case "ShunShouQianYang":
				isEnemyOutShunShou = true;
				SetChuPaiZhuangTai("WuXieKeJi");
				break;
			case "WanJianQiFa":
				if (enemyAI.isWanJian == true)
				{
					isEnemyOutWanJian = true;
					SetChuPaiZhuangTai("Shan");
				}
				else
				{
					isEnemyOutWanJian = true;
					SetChuPaiZhuangTai("WuXieKeJi");
				}
				break;
			case "WuXieKeJi":
				SetChuPaiZhuangTai("WuXieKeJi");
				break;
			case "WuZhongShengYou":
				isEnemyOutWuZhong = true;
				SetChuPaiZhuangTai("WuXieKeJi");
				break;
		}
    }

	/// <summary>
	/// 设置出牌状态并传达需要出什么牌
	/// </summary>
	/// <param name="needOutName"></param>
	public void SetChuPaiZhuangTai(string needOutName)
    {
		thisData.currentNeedOutCardName = needOutName;
		thisData.curRound = RoundType.NeedOutPai;
		enemyAI.thisData.RoundType = RoundType.Wait;
		GameController.Instance.playerNeedTimer = 10;
		thisData.isNeedOutCard = true;
		if(needOutName == "WuXieKeJi")
        {
			thisData.isWuXie = true;
        }
        else
        {
			thisData.isWuXie = false;
        }
		CardNoClick();
    }
}
