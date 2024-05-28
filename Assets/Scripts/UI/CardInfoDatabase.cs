using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardInfo
{
    private string name;
    private string introduce;

    public string Name
    {
        get { return name; }
    }
    public string Introduce
    {
        get { return introduce; }
    }

    public CardInfo(string Name, string Introduce)
    {
        this.name = Name;
        this.introduce = Introduce;
    }
}

public class CardInfoDatabase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   
    List<CardInfo> cardInfoList = new List<CardInfo>();
    private GameObject CardTextBG;
    private Text IntroduceText;

    void Start()
    {
        CardTextBG = GameObject.Find("CardTextBG").gameObject;
        IntroduceText = GameObject.Find("CardTextBG").transform.GetChild(0).GetComponent<Text>();
        InitCardInfo();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        CardTextBG.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        IntroduceText.color = new Color(0, 0, 0, 255);
        CardTextBG.transform.position = Input.mousePosition;
        if (GetCardName(transform.GetComponent<Image>().gameObject.name) == transform.GetComponent<Image>().gameObject.name)
        {
            IntroduceText.text = GetCardIntroduce(transform.GetComponent<Image>().gameObject.name);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardTextBG.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        IntroduceText.color = new Color(0, 0, 0, 0);
    }

    // Use this for initialization
   

    private void InitCardInfo()
    {
        CardInfo c1 = new CardInfo("Jiu", "出牌阶段或一名角色濒死时可以打出，回复一点体力。");
        CardInfo c2 = new CardInfo("Sha", "你的出牌阶段，对除你外，你攻击范围内的一名角色使用，效果是对该角色造成1点伤害。");
        CardInfo c3 = new CardInfo("Shan", "当你受到【杀】的攻击时，你可以使用一张【闪】来抵消【杀】的效果。");
        CardInfo c4 = new CardInfo("Tao", "回复1点体力");
        CardInfo c5 = new CardInfo("WuXieKeJi", "在一张锦囊牌生效前，抵消此牌对一名角色产生的效果，也可以抵消另一张【无懈可击】。");
        CardInfo c6 = new CardInfo("JieDaoShaRen", "出牌阶段，对装备区里有武器牌的其他角色A使用 ，且其攻击范围内有可以使用【杀】的目标B。A需对B使用一张【杀】，否则你获取A装备区里的武器牌。");
        CardInfo c7 = new CardInfo("WanJianQiFa", "出牌阶段，除自身角色外其他角色需打出一张【闪】，否则受到1点伤害。");
        CardInfo c8 = new CardInfo("NanManRuQin", "出牌阶段，除自身角色外其他角色需打出一张【杀】，否则受到1点伤害。");
        CardInfo c9 = new CardInfo("WuZhongShengYou", "出牌阶段，使用后，自己可以摸两张牌。");
        CardInfo c10 = new CardInfo("ShunShouQianYang", "出牌阶段，对距离为1且区域里有牌的一名其他角色使用。可以获得其区域里的一张牌。");
        CardInfo c11 = new CardInfo("GuoHeChaiQiao", "出牌阶段，对区域里有牌的一名其他角色使用。可以弃置其区域里的一张牌。");
        CardInfo c12 = new CardInfo("JueDou", "出牌阶段，对一名其他角色使用。由其开始，其与你轮流打出一张。");
        CardInfo e1 = new CardInfo("CiXiongShuangJian", "攻击范围：2。\n武器特效：你使用【杀】时，指定了一名异性角色后，在【杀】结算前，你可以令对方选择一项：自己弃一张手牌或者让你从牌堆摸一张牌。");
        CardInfo e2 = new CardInfo("BaiYinShiZi", "防具效果：每次你受到伤害时，最多承受1点伤害（防止多余的伤害）；当你失去装备区里的白银狮子时，你回复1点体力。");
        CardInfo e3 = new CardInfo("BaGuaZhen", "防具效果：每当你需要使用（或打出）一张【闪】时，你可以进行一次判定：若结果为红色，则视为你使用（或打出）了一张【闪】；若为黑色，则你仍可从手牌里使用（或打出）");
        CardInfo e4 = new CardInfo("GuanShiFu", "攻击范围：3。\n武器特效：武器特效：目标角色使用【闪】抵消你使用【杀】的效果时，你可以弃两张牌，则【杀】依然造成伤害。");
        CardInfo e5 = new CardInfo("JiaMa", "装备效果：其他角色计算与你的距离时，始终+1。");
        CardInfo e6 = new CardInfo("JianMa", "装备效果：其他角色计算与你的距离时，始终-1。");
        CardInfo e7 = new CardInfo("QiLinGong", "攻击范围：5。\n武器特效：你使用【杀】对目标角色造成伤害时，你可以将其装备区里的一匹马弃置。");
        CardInfo e8 = new CardInfo("QingGangJian", "攻击范围：2。\n武器特效：锁定技，每当你使用【杀】时，无视目标角色的防具。");
        CardInfo e9 = new CardInfo("QingLongYanYueDao", "攻击范围：3。\n武器技能：当你使用的【杀】被抵消时，你可以立即对相同的目标再使用一张【杀】。");
        CardInfo e10 = new CardInfo("ZhangBaSheMao", "攻击范围：3。\n武器特效：当你需要使用（或打出）一张【杀】时，你可以将两张手牌当一张【杀】使用（或打出）。");
        CardInfo e11 = new CardInfo("ZhuGeLianNu", "攻击范围：1。\n武器特效：出牌阶段，你可以使用任意张【杀】。");
        CardInfo e12 = new CardInfo("ZhuQueYuShan", "攻击范围：4。\n武器特效：你可以将你的任一普通杀当作具火焰伤害的杀来使用。");
        cardInfoList.Add(c1);
        cardInfoList.Add(c2);
        cardInfoList.Add(c3);
        cardInfoList.Add(c4);
        cardInfoList.Add(c5);
        cardInfoList.Add(c6);
        cardInfoList.Add(c7);
        cardInfoList.Add(c8);
        cardInfoList.Add(c9);
        cardInfoList.Add(c10);
        cardInfoList.Add(c11);
        cardInfoList.Add(c12);
        cardInfoList.Add(e1);
        cardInfoList.Add(e2);
        cardInfoList.Add(e3);
        cardInfoList.Add(e4);
        cardInfoList.Add(e5);
        cardInfoList.Add(e6);
        cardInfoList.Add(e7);
        cardInfoList.Add(e8);
        cardInfoList.Add(e9);
        cardInfoList.Add(e10);
        cardInfoList.Add(e11);
        cardInfoList.Add(e12);
    }

    /// <summary>
    /// 获取数组中的名字
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private string GetCardName(string name)
    {
        for (int i = 0; i < cardInfoList.Count; i++)
        {
            if (name == cardInfoList[i].Name)
            {
                return cardInfoList[i].Name;
            }
        }
        return null;
    }
    /// <summary>
    /// 获取数组中的详细信息
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private string GetCardIntroduce(string name)
    {
        for (int i = 0; i < cardInfoList.Count; i++)
        {
            if (name == cardInfoList[i].Name)
            {
                return cardInfoList[i].Introduce;
            }
        }
        return null;
    }
}
