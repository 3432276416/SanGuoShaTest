using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ChouEnemyCardButton : MonoBehaviour {
	public Toggle toggle;
	public GeneralThiaData enemyData;
	public GameObject chouEnemyCardGo;
	public GameObject playerCardList;
	public bool isEquip = false;
	public string cardName;

	// Use this for initialization
	void Start () {
		toggle = gameObject.GetComponent<Toggle>();
		enemyData = GameObject.FindGameObjectWithTag("Enemy").GetComponent<GeneralThiaData>();
		playerCardList = GameObject.Find("PlayerCardList").gameObject;
		chouEnemyCardGo = GameObject.Find("ChouEnemyCard").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(toggle.isOn == true)
        {
			if (isEquip == false)
			{
				if (cardName == "GuoHeChaiQiao")
                {
					GetGo(gameObject.name).GetComponent<Image>().sprite = enemyData.GetCardGo(gameObject.name).
						GetComponent<Image>().sprite;
					enemyData.OutCardMoveTarGetPos(GetGo(gameObject.name));
					Destroy(gameObject);
					chouEnemyCardGo.transform.DOLocalMove(new Vector3(1600, 0, 0), 0.3f);
				}
				if (cardName == "ShunShouQianYang")
				{
					GetGo(gameObject.name).GetComponent<Image>().sprite = enemyData.GetCardGo(gameObject.name).
						GetComponent<Image>().sprite;
					enemyData.EnemyCardMovePlayerCard(GetGo(gameObject.name), playerCardList, PlayerAndEnemy.Player);
					Destroy(gameObject);
					chouEnemyCardGo.transform.DOLocalMove(new Vector3(1600, 0, 0), 0.3f);
				}
            }
            else
			{
				if (cardName == "GuoHeChaiQiao")
				{
					enemyData.OutCardMoveTarGetPos(GetEquip(gameObject.name));
					Destroy(gameObject);
					chouEnemyCardGo.transform.DOLocalMove(new Vector3(1600, 0, 0), 0.3f);
				}
				if (cardName == "ShunShouQianYang")
				{
					enemyData.EnemyCardMovePlayerCard(GetEquip(gameObject.name), playerCardList, PlayerAndEnemy.Player);
					Destroy(gameObject);
					chouEnemyCardGo.transform.DOLocalMove(new Vector3(1600, 0, 0), 0.3f);
				}
			}
        }
	}

	public GameObject GetGo(string name)
    {
		for(int i = 0;i< enemyData.thisCard.Count; i++)
        {
            if (enemyData.thisCard[i].name == name)
            {
				return enemyData.thisCard[i].gameObject;
            }
        }
		return null;
    }

	public GameObject GetEquip(string name)
    {
        if (enemyData.weaponGo != null)
        {
			if(name == enemyData.weaponGo.name)
            {
				return enemyData.weaponGo;
            }
		}
		if (enemyData.armorGo != null)
		{
			if (name == enemyData.armorGo.name)
			{
				return enemyData.armorGo;
			}
		}
		if (enemyData.jiaMaGo != null)
		{
			if (name == enemyData.jiaMaGo.name)
			{
				return enemyData.jiaMaGo;
			}
		}
		if (enemyData.jianMaGo != null)
		{
			if (name == enemyData.jianMaGo.name)
			{
				return enemyData.jianMaGo;
			}
		}
		return null;
	}
}
