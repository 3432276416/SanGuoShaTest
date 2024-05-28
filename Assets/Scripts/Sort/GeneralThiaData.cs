using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GeneralThiaData : MonoBehaviour 
{
	public int currentHP;//当然的血量
	public int maxHP;//最大血量
	public string currentOutCardName;//当前出的锦囊牌
	public GameObject currrentClickCardGo;//当前点击的手牌的游戏物体
	public GameObject chouEnemyCard; //抽敌人牌的UI
	public string currentNeedOutCardName;//当前需要出的牌
	public bool isNeedOutCard;//是否打出需要的牌
	public bool isJiu; //是否喝酒
	public bool isChouPai; //是否要抽牌
	public bool isArmor; //是否有防具
	public bool isWeapon; //
	public bool isJiaMa;
	public bool isJianMa;
	public bool isWuXie; //是否可以出无懈可击
	public bool isOutWuXie; //是否出过无懈可击
	public bool isJueDou; //是否打出决斗
	public int attackDis = 1; //攻击距离
	public int outShaNum = 1; //出杀的次数
	public int outJinNangDis = 1; //出锦囊的距离
	public int ziShenDis = 1; //自身的距离
	public ChuPaiState chuPaiState;//出牌状态
	public PlayerAndEnemy playerAndEnemy;
	public List<GameObject> thisCard = new List<GameObject>();//自身卡牌列表
	public List<GameObject> thisEquipCardGo = new List<GameObject>();//装备游戏物体列表
	public List<GameObject> cardGo = new List<GameObject>();//全部的卡牌列表
	public AudioController audioController;
	private Transform tarGetPos;

	public EnemyAI enemyAI;
	public PlayerController playerController;

	public GameObject weaponGo;
	public GameObject armorGo;
	public GameObject jiaMaGo;
	public GameObject jianMaGo;


	// Use this for initialization
	void Start () {
		audioController = GetComponent<AudioController>();
		tarGetPos = GameObject.Find("TarGetPos").transform;
		enemyAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		chouEnemyCard = GameObject.Find("ChouEnemyCard").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		GetThisEquipGo();

		if (Input.GetKeyDown(KeyCode.S))
        {
			ThisCard(playerAndEnemy);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			OutCrad(currrentClickCardGo);
		}
	}
	/// <summary>
	/// 获取自身有没有装备
	/// </summary>
	public void GetThisEquipGo()
    {
		if(playerAndEnemy == PlayerAndEnemy.Player)
        {
			if(isWeapon == true && GameObject.Find("PlayerWeapon").transform.childCount > 0)
            {
				weaponGo = GameObject.Find("PlayerWeapon").transform.GetChild(0).gameObject;
            }
            else
            {
				isWeapon = false;
			}
			if (isArmor == true && GameObject.Find("PlayerArmor").transform.childCount > 0)
			{
				armorGo = GameObject.Find("PlayerArmor").transform.GetChild(0).gameObject;
			}
			else
			{
				isArmor = false;
			}
			if (isJiaMa == true && GameObject.Find("PlayerJiaMa").transform.childCount > 0)
			{
				jiaMaGo = GameObject.Find("PlayerJiaMa").transform.GetChild(0).gameObject;
			}
			else
			{
				isJiaMa = false;
			}
			if (isJianMa == true && GameObject.Find("PlayerJianMa").transform.childCount > 0)
			{
				jianMaGo = GameObject.Find("PlayerJianMa").transform.GetChild(0).gameObject;
			}
			else
			{
				isJianMa = false;
			}
		}


		if (playerAndEnemy == PlayerAndEnemy.Enemy)
		{
			if (isWeapon == true && GameObject.Find("EnemyWeapon").transform.childCount > 0)
			{
				weaponGo = GameObject.Find("EnemyWeapon").transform.GetChild(0).gameObject;
			}
			else
			{
				isWeapon = false;
			}
			if (isArmor == true && GameObject.Find("EnemyArmor").transform.childCount > 0)
			{
				armorGo = GameObject.Find("EnemyArmor").transform.GetChild(0).gameObject;
			}
			else
			{
				isArmor = false;
			}
			if (isJiaMa == true && GameObject.Find("EnemyJiaMa").transform.childCount > 0)
			{
				jiaMaGo = GameObject.Find("EnemyJiaMa").transform.GetChild(0).gameObject;
			}
			else
			{
				isJiaMa = false;
			}
			if (isJianMa == true && GameObject.Find("EnemyJianMa").transform.childCount > 0)
			{
				jianMaGo = GameObject.Find("EnemyJianMa").transform.GetChild(0).gameObject;
			}
			else
			{
				isJianMa = false;
			}
		}
	}

	/// <summary>
	/// 出牌
	/// </summary>
	/// <param name="GO"></param>
	public void OutCrad(GameObject GO)
    {
        if (thisCard.Count != 0)
        {
			for(int i = 0; i < thisCard.Count; i++)
            {
				if (thisCard[i] == null) return;
				if(playerAndEnemy == PlayerAndEnemy.Player)
                {
					if(thisCard[i].name == GO.name && thisCard[i].gameObject.GetComponent<Toggle>().isOn == true)
                    {
						if(GO.name == "Sha"&& chuPaiState == ChuPaiState.MayChuPai)
                        {
							outShaNum -= 1;
                        }
						if(GO.name == "WuXieKeJi")
                        {
							isWuXie = false;
							isOutWuXie = true;
							audioController.PlayAudio(GO.name);
							StartCoroutine(FunctionCard(currentOutCardName));
							StartCoroutine(FunctionCard(thisCard[i].name));
							thisCard[i].gameObject.GetComponent<Toggle>().graphic = null;
							OutCardMoveTarGetPos(thisCard[i].gameObject);
                        }
                        else
						{
							audioController.PlayAudio(GO.name);
							StartCoroutine(FunctionCard(thisCard[i].name));
							thisCard[i].gameObject.GetComponent<Toggle>().graphic = null;
							OutCardMoveTarGetPos(thisCard[i].gameObject);
						}
						currrentClickCardGo = null;
					}
                }
				if (playerAndEnemy == PlayerAndEnemy.Enemy)
				{
					if (thisCard[i].name == GO.name && GO != null)
					{
						if (currentNeedOutCardName == GO.name && chuPaiState == ChuPaiState.NeedOutPai)
						{
							if (GO.name == "WuXieKeJi")
							{
								thisCard[i].GetComponent<Image>().sprite = GetCardGo(GO.name).GetComponent<Image>().sprite;
								playerController.thisData.isWuXie = true;
								isOutWuXie = true;
								audioController.PlayAudio(GO.name);
								StartCoroutine(FunctionCard(currentOutCardName));
								StartCoroutine(FunctionCard(thisCard[i].name));
								OutCardMoveTarGetPos(thisCard[i].gameObject);
								chuPaiState = ChuPaiState.Wait;
							}
							else
							{
								thisCard[i].GetComponent<Image>().sprite = GetCardGo(GO.name).GetComponent<Image>().sprite;
								audioController.PlayAudio(GO.name);
								StartCoroutine(FunctionCard(thisCard[i].name));
								OutCardMoveTarGetPos(thisCard[i].gameObject);
								chuPaiState = ChuPaiState.Wait;
							}
						}
						if (chuPaiState == ChuPaiState.MayChuPai)
						{
							if (GO.name == "Sha")
							{
								outShaNum -= 1;
							}
							thisCard[i].GetComponent<Image>().sprite = GetCardGo(GO.name).GetComponent<Image>().sprite;
							audioController.PlayAudio(GO.name);
							StartCoroutine(FunctionCard(thisCard[i].name));
							OutCardMoveTarGetPos(thisCard[i]);
							chuPaiState = ChuPaiState.Wait;
						}
					}
				}
			}
        }
    }
	/// <summary>
	/// 弃牌
	/// </summary>
	/// <param name="name"></param>
	public void DisCard(string name)
    {
		for(int i = 0; i < thisCard.Count; i++)
        {
			if(thisCard[i].name == name && thisCard[i].gameObject.GetComponent<Toggle>().isOn == true)
            {
				OutCardMoveTarGetPos(thisCard[i].gameObject);
            }
        }
    }
	/// <summary>
	/// 移动牌
	/// </summary>
	/// <param name="gameObject"></param>
	public void OutCardMoveTarGetPos(GameObject gameObject)
    {
        if (playerAndEnemy == PlayerAndEnemy.Player)
        {
			Destroy(gameObject.transform.GetComponent<Toggle>());
		}
		gameObject.GetComponent<Image>().sprite = GetCardGo(gameObject.name).GetComponent<Image>().sprite;
		gameObject.transform.DOMove(tarGetPos.position, 0.5f).OnComplete(() =>
		{
			DestroyCard(gameObject);
		});
	}

	public void EnemyCardMovePlayerCard(GameObject thisGO,GameObject tarGo,PlayerAndEnemy playerAndEnemy)
    {
		thisGO.transform.DOMove(tarGo.transform.position, 0.5f).OnComplete(() =>
		{
			if(playerAndEnemy == PlayerAndEnemy.Player)
            {
				thisCard.Remove(thisGO);
				Destroy(thisGO);
				GameObject GO = Instantiate(GetCardGo(thisGO.name));
				playerController.thisData.thisCard.Add(GO);
				GO.name = GetCardGo(thisGO.name).name;
				GO.transform.SetParent(tarGo.transform);
				GO.transform.localScale = Vector3.one;
            }
			if(playerAndEnemy == PlayerAndEnemy.Enemy)
			{
				thisCard.Remove(thisGO);
				Destroy(thisGO);
				GameObject GO = Instantiate(GetCardGo(thisGO.name));
				enemyAI.thisData.thisCard.Add(GO);
				GO.name = GetCardGo(thisGO.name).name;
				GO.transform.SetParent(tarGo.transform);
				GO.transform.localScale = Vector3.one;
				GO.GetComponent<Image>().sprite = GetCardGo("Back").GetComponent<Image>().sprite;
				Destroy(GO.transform.GetComponent<CardUIDisPlay>());
				Destroy(GO.transform.GetComponent<CardButton>());
				Destroy(GO.transform.GetComponent<Toggle>());

			}
		});
    }

	/// <summary>
	/// 移除牌
	/// </summary>
	/// <param name="gameObject"></param>
	public void DestroyCard(GameObject gameObject)
    {
		thisCard.Remove(gameObject);
		Destroy(gameObject);
	}
	/// <summary>
	/// 掉血
	/// </summary>
	/// <param name="damage"></param>
	public void Hit(int damage)
    {
		currentHP -= damage;
		audioController.PlayAudio("Hit");
    }
	public void AddHP(int number)
    {
		currentHP += number;
		audioController.PlayAudio("Tao");
        if (currentHP > maxHP)
        {
			currentHP = maxHP;
        }
    }
	/// <summary>
	/// 出的什么牌
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	IEnumerator FunctionCard(string name)
    {
		yield return 0;
		if(playerAndEnemy == PlayerAndEnemy.Player)
        {
            switch (name)
            {
				case "Jiu":
					isJiu = true;
					break;
				case "Sha":
					if (chuPaiState == ChuPaiState.MayChuPai && isJueDou == false && enemyAI.thisData.isJueDou == false)
                    {
						if(isJiu == false)
                        {
							enemyAI.PlayerPlayCard(name, 1);
                        }
                        else
						{
							enemyAI.PlayerPlayCard(name, 2);
						}
					}
					if (isJueDou == true || enemyAI.thisData.isJueDou == true)
					{
						enemyAI.PlayerPlayCard(name, 1);
					}
					break;
				case "Tao":
					AddHP(1);
					break;
				case "GuoHeChaiQiao":
					enemyAI.PlayerPlayCard(name, 0);
					currentOutCardName = name;
					if(enemyAI.thisData.isChouPai == true)
                    {
						chouEnemyCard.gameObject.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
						chouEnemyCard.GetComponent<ChouEnemyCard>().isUpDateList = true;
						chouEnemyCard.GetComponent<ChouEnemyCard>().cardName = name;
						enemyAI.thisData.isChouPai = false;
					}
					break;
				case "JieDaoShaRen":
					enemyAI.PlayerPlayCard(name, 0);
					currentOutCardName = name;
					break;
				case "JueDou":
					enemyAI.PlayerPlayCard(name, 1);
					currentOutCardName = name;
					break;
				case "NanManRuQin":
					enemyAI.PlayerPlayCard(name, 1);
					currentOutCardName = name;
					break;
				case "ShunShouQianYang":
					enemyAI.PlayerPlayCard(name, 0);
					currentOutCardName = name;
					if (enemyAI.thisData.isChouPai == true)
					{
						chouEnemyCard.gameObject.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
						chouEnemyCard.GetComponent<ChouEnemyCard>().isUpDateList = true;
						chouEnemyCard.GetComponent<ChouEnemyCard>().cardName = name;
						enemyAI.thisData.isChouPai = false;
					}
					break;
				case "WanJianQiFa":
					enemyAI.PlayerPlayCard(name, 1);
					currentOutCardName = name;
					break;
				case "WuXieKeJi":
					enemyAI.PlayerPlayCard(name, 0);
					enemyAI.thisData.isOutWuXie = false;
					break;
				case "WuZhongShengYou":
					enemyAI.PlayerPlayCard(name, 0);
					currentOutCardName = name;
					break;
				case "BaGuaZhen":
					isArmor = true;
					Equip(name, EquipType.Armor, playerAndEnemy);
					break;
				case "BaiYinShiZi":
					isArmor = true;
					Equip(name, EquipType.Armor, playerAndEnemy);
					break;
				case "CiXiongShuangJian":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 2;
					break;
				case "GuanShiFu":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy); 
					attackDis = 3;
					break;
				case "JiaMa":
					isJiaMa = true;
					Equip(name, EquipType.JiaMa, playerAndEnemy);
					ziShenDis = 2;
					break;
				case "JianMa":
					isJianMa = true;
					Equip(name, EquipType.JianMa, playerAndEnemy);
					outJinNangDis = 2;
					break;
				case "QiLinGong":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 5;
					break;
				case "QingGangJian":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 2;
					break;
				case "QingLongYanYueDao":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 3;
					break;
				case "ZhangBaSheMao":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 3;
					break;
				case "ZhuGeLianNu":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 1;
					break;
				case "ZhuQueYuShan":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 4;
					break;
				default:
					break;
			}
        }
		if (playerAndEnemy == PlayerAndEnemy.Enemy)
		{
			switch (name)
			{
				case "Jiu":
					isJiu = true;
					break;
				case "Sha":
					if(isJueDou == true || playerController.thisData.isJueDou == true)
                    {
						playerController.EnemyPlayCard(name);
                    }
					if(chuPaiState == ChuPaiState.MayChuPai)
                    {
						playerController.EnemyPlayCard(name);
					}
					break;
				case "Tao":
					AddHP(1);
					break;
				case "GuoHeChaiQiao":
					playerController.EnemyPlayCard(name);
					currentOutCardName = name;
					break;
				case "JieDaoShaRen":
					playerController.EnemyPlayCard(name);
					currentOutCardName = name;
					break;
				case "JueDou":
					playerController.EnemyPlayCard(name);
					currentOutCardName = name;
					break;
				case "NanManRuQin":
					playerController.EnemyPlayCard(name);
					currentOutCardName = name;
					break;
				case "ShunShouQianYang":
					playerController.EnemyPlayCard(name);
					currentOutCardName = name;
					break;
				case "WanJianQiFa":
					playerController.EnemyPlayCard(name);
					currentOutCardName = name;
					break;
				case "WuXieKeJi":
					playerController.EnemyPlayCard(name);
					break;
				case "WuZhongShengYou":
					playerController.EnemyPlayCard(name);
					currentOutCardName = name;
					break;
				case "BaGuaZhen":
					isArmor = true;
					Equip(name, EquipType.Armor, playerAndEnemy);
					break;
				case "BaiYinShiZi":
					isArmor = true;
					Equip(name, EquipType.Armor, playerAndEnemy);
					break;
				case "CiXiongShuangJian":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 2;
					break;
				case "GuanShiFu":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 3;
					break;
				case "JiaMa":
					isJiaMa = true;
					Equip(name, EquipType.JiaMa, playerAndEnemy);
					ziShenDis = 2;
					break;
				case "JianMa":
					isJianMa = true;
					Equip(name, EquipType.JianMa, playerAndEnemy);
					outJinNangDis = 2;
					break;
				case "QiLinGong":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 5;
					break;
				case "QingGangJian":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 2;
					break;
				case "QingLongYanYueDao":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 3;
					break;
				case "ZhangBaSheMao":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 3;
					break;
				case "ZhuGeLianNu":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 1;
					break;
				case "ZhuQueYuShan":
					isWeapon = true;
					Equip(name, EquipType.Weapon, playerAndEnemy);
					attackDis = 4;
					break;
				default:
					break;
			}
		}
	}
	/// <summary>
	/// 穿装备
	/// </summary>
	/// <param name="name"></param>
	/// <param name="equipType"></param>
	/// <param name="playerAndEnemy"></param>
	public void Equip(string name,EquipType equipType,PlayerAndEnemy playerAndEnemy)
    {
		audioController.PlayAudio("ZhuangBei");
		GameObject go = Instantiate(GetThisEquipCardGo(name));
		go.name = name;
		Transform ts = null;
        switch (equipType)
        {
			case EquipType.Armor:
				if(playerAndEnemy == PlayerAndEnemy.Player)
                {
					ts = GameObject.Find("PlayerArmor").transform;
				}
				if (playerAndEnemy == PlayerAndEnemy.Enemy)
				{
					ts = GameObject.Find("EnemyArmor").transform;
				}
				break;
			case EquipType.Weapon:
				if (playerAndEnemy == PlayerAndEnemy.Player)
				{
					ts = GameObject.Find("PlayerWeapon").transform;
				}
				if (playerAndEnemy == PlayerAndEnemy.Enemy)
				{
					ts = GameObject.Find("EnemyWeapon").transform;
				}
				break;
			case EquipType.JiaMa:
				if (playerAndEnemy == PlayerAndEnemy.Player)
				{
					ts = GameObject.Find("PlayerJiaMa").transform;
				}
				if (playerAndEnemy == PlayerAndEnemy.Enemy)
				{
					ts = GameObject.Find("EnemyJiaMa").transform;
				}
				break;
			case EquipType.JianMa:
				if (playerAndEnemy == PlayerAndEnemy.Player)
				{
					ts = GameObject.Find("PlayerJianMa").transform;
				}
				if (playerAndEnemy == PlayerAndEnemy.Enemy)
				{
					ts = GameObject.Find("EnemyJianMa").transform;
				}
				break;
			default:
				break;
		}
        if (ts != null)
        {
            if (ts.childCount != 0)
            {
				Destroy(ts.GetChild(0).gameObject);
            }
        }
		go.transform.SetParent(ts);
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = Vector3.one;
    }

	/// <summary>
	/// 获取自身卡牌的游戏物体
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public GameObject GetThisCardGo(string name)
    {
		for(int i = 0; i < thisCard.Count; i++)
        {
			if(thisCard[i].name == name)
            {
				return thisCard[i].gameObject;
            }
        }
		return null;
	}
	/// <summary>
	/// 获取自身装备卡牌的游戏物体
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public GameObject GetThisEquipCardGo(string name)
	{
		for (int i = 0; i < thisEquipCardGo.Count; i++)
		{
			if (thisEquipCardGo[i].name == name)
			{
				return thisEquipCardGo[i].gameObject;
			}
		}
		return null;
	}
	/// <summary>
	/// 获取自身卡牌的游戏物体
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public GameObject GetCardGo(string name)
	{
		for (int i = 0; i < cardGo.Count; i++)
		{
			if (cardGo[i].name == name)
			{
				return cardGo[i].gameObject;
			}
		}
		return null;
	}

	/// <summary>
	/// 更新自身牌列表
	/// </summary>
	/// <param name="playerAndEnemy"></param>
	public void ThisCard(PlayerAndEnemy playerAndEnemy)
    {
		if(playerAndEnemy == PlayerAndEnemy.Player)
        {
			thisCard.RemoveRange(0, thisCard.Count);
			FindChild(GameObject.Find("PlayerCardList").gameObject);
		}
		if (playerAndEnemy == PlayerAndEnemy.Enemy)
		{
			thisCard.RemoveRange(0, thisCard.Count);
			FindChild(GameObject.Find("EnemyCardList").gameObject);
		}
	}


	/// <summary>
	/// 获取子物体添加到列表中
	/// </summary>
	/// <param name="Go"></param>
	public void FindChild(GameObject Go)
    {
		for(int i = 0; i < Go.transform.childCount; i++)
        {
            if (Go.transform.GetChild(i).childCount > 0)
            {
				FindChild(Go.transform.GetChild(i).gameObject);
            }
			thisCard.Add(Go.transform.GetChild(i).gameObject);
        }
    }
}
