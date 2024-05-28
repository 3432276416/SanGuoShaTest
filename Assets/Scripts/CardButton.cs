using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardButton : MonoBehaviour {
	public Toggle toggle;
	public CharacterData characterData;
	// Use this for initialization
	void Start () {
		toggle = GetComponent<Toggle>();
		characterData = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterData>();
	}
	
	// Update is called once per frame
	void Update () {
		if (toggle == null) return;
		if(toggle.group == null)
        {
			toggle.group = transform.parent.gameObject.GetComponent<ToggleGroup>();
        }
	}

	public void Sel_toggle(bool Selete)
    {
		if (Selete)
		{
			Tween tween = gameObject.transform.DOMove(transform.position + new Vector3(0, 30, 0), 0.1f);
			tween.SetAutoKill(false);
			characterData.curClickCard = transform.gameObject;
		}
        else
        {
			gameObject.transform.DOPlayBackwards();
        }
    }
}
