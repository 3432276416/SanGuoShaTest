using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChouEnemyCard : MonoBehaviour {

	public GeneralThiaData enemyData;
	public GameObject tarGo;
	public string cardName;
	public bool isUpDateList = false;
	public GameObject ChouWeapon;
	public GameObject ChouArmor;
	public GameObject ChouJiaMa;
	public GameObject ChouJianMa;

	// Use this for initialization
	void Start () {
		enemyData = GameObject.FindGameObjectWithTag("Enemy").GetComponent<GeneralThiaData>();
		isUpDateList = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isUpDateList)
        {
			InCardGo();
			InEquipGo();
			isUpDateList = false;

		}
	}

	public void InEquipGo()
    {
        if (enemyData.weaponGo != null)
        {
			DisTarGoChild(ChouWeapon);
			GameObject weapon = Instantiate(enemyData.weaponGo);
			weapon.name = enemyData.weaponGo.name;
			weapon.transform.SetParent(ChouWeapon.transform);
			weapon.transform.localPosition = Vector3.zero;
			weapon.transform.localScale = Vector3.one;
			weapon.AddComponent<Toggle>();
			weapon.AddComponent<ChouEnemyCardButton>();
			weapon.GetComponent<ChouEnemyCardButton>().cardName = cardName;
			weapon.GetComponent<ChouEnemyCardButton>().isEquip = true;
		}
		if (enemyData.armorGo != null)
		{
			DisTarGoChild(ChouArmor);
			GameObject armor = Instantiate(enemyData.armorGo);
			armor.name = enemyData.armorGo.name;
			armor.transform.SetParent(ChouArmor.transform);
			armor.transform.localPosition = Vector3.zero;
			armor.transform.localScale = Vector3.one;
			armor.AddComponent<Toggle>();
			armor.AddComponent<ChouEnemyCardButton>();
			armor.GetComponent<ChouEnemyCardButton>().cardName = cardName;
			armor.GetComponent<ChouEnemyCardButton>().isEquip = true;
		}
		if (enemyData.jiaMaGo != null)
		{
			DisTarGoChild(ChouJiaMa);
			GameObject jiaMa = Instantiate(enemyData.jiaMaGo);
			jiaMa.name = enemyData.jiaMaGo.name;
			jiaMa.transform.SetParent(ChouJiaMa.transform);
			jiaMa.transform.localPosition = Vector3.zero;
			jiaMa.transform.localScale = Vector3.one;
			jiaMa.AddComponent<Toggle>();
			jiaMa.AddComponent<ChouEnemyCardButton>();
			jiaMa.GetComponent<ChouEnemyCardButton>().cardName = cardName;
			jiaMa.GetComponent<ChouEnemyCardButton>().isEquip = true;
		}
		if (enemyData.jianMaGo != null)
		{
			DisTarGoChild(ChouJianMa);
			GameObject jianMa = Instantiate(enemyData.jianMaGo);
			jianMa.name = enemyData.jianMaGo.name;
			jianMa.transform.SetParent(ChouJianMa.transform);
			jianMa.transform.localPosition = Vector3.zero;
			jianMa.transform.localScale = Vector3.one;
			jianMa.AddComponent<Toggle>();
			jianMa.AddComponent<ChouEnemyCardButton>();
			jianMa.GetComponent<ChouEnemyCardButton>().cardName = cardName;
			jianMa.GetComponent<ChouEnemyCardButton>().isEquip = true;
		}
	}

	public void InCardGo()
    {
        if (enemyData.thisCard.Count > 0)
        {
			DisTarGoChild(tarGo);
			for(int i = 0; i < enemyData.thisCard.Count; i++)
            {
				GameObject card = Instantiate(enemyData.thisCard[i]);
				card.name = enemyData.thisCard[i].name;
				card.transform.SetParent(tarGo.transform);
				card.AddComponent<Toggle>();
				card.AddComponent<ChouEnemyCardButton>();
				card.GetComponent<ChouEnemyCardButton>().cardName = cardName;
				card.GetComponent<ChouEnemyCardButton>().isEquip = false;
				card.transform.localPosition = Vector3.zero;
				card.transform.localScale = Vector3.one;

			}

		}
    }

	public void DisTarGoChild(GameObject gameObject)
    {
		for(int i = 0; i < gameObject.transform.childCount; i++)
        {
			Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
