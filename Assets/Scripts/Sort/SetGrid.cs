using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetGrid : MonoBehaviour {
	private GridLayoutGroup layoutGroup;
	private RectTransform m_parent;

	// Use this for initialization
	void Start () {
		layoutGroup = GetComponent<GridLayoutGroup>();
		m_parent = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		SetGridSpacing();
	}
	public void SetGridSpacing()
    {
		int childCount = m_parent.childCount;
        if (childCount >= 6)
        {
			layoutGroup.spacing = new Vector2(-80, 0);
        }
        else
		{
			layoutGroup.spacing = new Vector2(0, 0);
		}
    }
}
