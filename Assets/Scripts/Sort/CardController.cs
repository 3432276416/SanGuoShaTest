using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SocialPlatforms;

public class CardOj
{
    private string _Name;
    private GameObject _CardObject;

    public string Name
    {
        get { return _Name; }
    }

    public GameObject CardObject
    {
        get { return _CardObject; }
    }
    public CardOj(string Name, GameObject CardObject)
    {
        this._Name = Name;
        this._CardObject = CardObject;
    }

}

public class CardController : MonoBehaviour {


	public GameObject playerTrs;
	public GameObject enemyTrs;
	public GameObject targetTrs;
	public List<GameObject> CardGameObjectList;
	public List<CardOj> cardLists = new List<CardOj>();
	public Image back;

	// Use this for initialization
	void Awake() {
		CreateCardOjList();
	}
	
	// Update is called once per frame
	void Update () {
		if(cardLists.Count == 0)
        {
			CreateCardOjList();
		}
	}


	public void SendCard(PlayerAndEnemy playerAndEnemy)
    {
		GameObject newCard = InstantiateCard();
		if(playerAndEnemy == PlayerAndEnemy.Player)
        {
			OutCardMovePos(newCard, playerTrs.transform);
			newCard.transform.localScale = Vector3.one;
        }
        else
        {
			newCard.GetComponent<Image>().sprite = back.sprite;
			//Destroy(newCard.transform.GetComponent<CardUIDisPlay>());
			Destroy(newCard.transform.GetComponent<CardButton>());
			Destroy(newCard.transform.GetComponent<Toggle>());
			OutCardMovePos(newCard, enemyTrs.transform);
			newCard.transform.localScale = Vector3.one;
		}
		cardLists.RemoveAt(cardLists.Count - 1);
    }

	/// <summary>
	/// 牌的移动
	/// </summary>
	/// <param name="gameObject"></param>
	/// <param name="tarGetPos"></param>
	public void OutCardMovePos(GameObject gameObject, Transform tarGetPos)
	{
		gameObject.transform.DOMove(tarGetPos.position, 0.5f).OnComplete(() =>
		{
			gameObject.transform.SetParent(tarGetPos);
			GameController.Instance.isGetCard = true;
		});
    }

	/// <summary>
	/// 实例化牌
	/// </summary>
	/// <returns></returns>
	public GameObject InstantiateCard()
    {
		GameObject newCard = Instantiate(cardLists[cardLists.Count - 1].CardObject);
		newCard.name = cardLists[cardLists.Count - 1].CardObject.name;
		newCard.transform.SetParent(targetTrs.transform);
		newCard.transform.position = targetTrs.transform.position;
		return newCard;
	}


	public void CreateCardOjList()
    {
		AddCardList(5, CardOj[0]);
		AddCardList(30, CardOj[1]);
		AddCardList(15, CardOj[2]);
		AddCardList(8, CardOj[3]);
		AddCardList(6, CardOj[4]);
		AddCardList(2, CardOj[5]);
		AddCardList(3, CardOj[6]);
		AddCardList(3, CardOj[7]);
		AddCardList(5, CardOj[8]);
		AddCardList(1, CardOj[9]);
		AddCardList(1, CardOj[10]);
		AddCardList(3, CardOj[11]);
		AddCardList(4, CardOj[12]);
		AddCardList(1, CardOj[13]);
		AddCardList(1, CardOj[14]);
		AddCardList(1, CardOj[15]);
		AddCardList(1, CardOj[16]);
		AddCardList(1, CardOj[17]);
		AddCardList(1, CardOj[18]);
		AddCardList(1, CardOj[19]);
		AddCardList(2, CardOj[20]);
		AddCardList(2, CardOj[21]);
		AddCardList(1, CardOj[22]);
		AddCardList(1, CardOj[23]);

		for (int i = 0; i < cardLists.Count; i++)
        {
			var temp = cardLists[i];
			var randomIndex = Random.Range(0, cardLists.Count);
			cardLists[i] = cardLists[randomIndex];
			cardLists[randomIndex] = temp;
        }
	}

	/// <summary>
	/// 往牌库里添加牌
	/// </summary>
	/// <param name="Number"></param>
	/// <param name="CardOj"></param>
	public void AddCardList(int Number, GameObject CardOj)
    {
		CaedList caedList = new CaedList(CardOj.ToString(), CardOj);
		for(int i = 0; i < Number; i++)
        {
			cardLists.Add(caedList);

		}
	}
}


