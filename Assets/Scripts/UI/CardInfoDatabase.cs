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
        CardInfo c1 = new CardInfo("Jiu", "���ƽ׶λ�һ����ɫ����ʱ���Դ�����ظ�һ��������");
        CardInfo c2 = new CardInfo("Sha", "��ĳ��ƽ׶Σ��Գ����⣬�㹥����Χ�ڵ�һ����ɫʹ�ã�Ч���ǶԸý�ɫ���1���˺���");
        CardInfo c3 = new CardInfo("Shan", "�����ܵ���ɱ���Ĺ���ʱ�������ʹ��һ�š�������������ɱ����Ч����");
        CardInfo c4 = new CardInfo("Tao", "�ظ�1������");
        CardInfo c5 = new CardInfo("WuXieKeJi", "��һ�Ž�������Чǰ���������ƶ�һ����ɫ������Ч����Ҳ���Ե�����һ�š���и�ɻ�����");
        CardInfo c6 = new CardInfo("JieDaoShaRen", "���ƽ׶Σ���װ�������������Ƶ�������ɫAʹ�� �����乥����Χ���п���ʹ�á�ɱ����Ŀ��B��A���Bʹ��һ�š�ɱ�����������ȡAװ������������ơ�");
        CardInfo c7 = new CardInfo("WanJianQiFa", "���ƽ׶Σ��������ɫ��������ɫ����һ�š������������ܵ�1���˺���");
        CardInfo c8 = new CardInfo("NanManRuQin", "���ƽ׶Σ��������ɫ��������ɫ����һ�š�ɱ���������ܵ�1���˺���");
        CardInfo c9 = new CardInfo("WuZhongShengYou", "���ƽ׶Σ�ʹ�ú��Լ������������ơ�");
        CardInfo c10 = new CardInfo("ShunShouQianYang", "���ƽ׶Σ��Ծ���Ϊ1�����������Ƶ�һ��������ɫʹ�á����Ի�����������һ���ơ�");
        CardInfo c11 = new CardInfo("GuoHeChaiQiao", "���ƽ׶Σ������������Ƶ�һ��������ɫʹ�á������������������һ���ơ�");
        CardInfo c12 = new CardInfo("JueDou", "���ƽ׶Σ���һ��������ɫʹ�á����俪ʼ���������������һ�š�");
        CardInfo e1 = new CardInfo("CiXiongShuangJian", "������Χ��2��\n������Ч����ʹ�á�ɱ��ʱ��ָ����һ�����Խ�ɫ���ڡ�ɱ������ǰ���������Է�ѡ��һ��Լ���һ�����ƻ���������ƶ���һ���ơ�");
        CardInfo e2 = new CardInfo("BaiYinShiZi", "����Ч����ÿ�����ܵ��˺�ʱ��������1���˺�����ֹ������˺���������ʧȥװ������İ���ʨ��ʱ����ظ�1��������");
        CardInfo e3 = new CardInfo("BaGuaZhen", "����Ч����ÿ������Ҫʹ�ã�������һ�š�����ʱ������Խ���һ���ж��������Ϊ��ɫ������Ϊ��ʹ�ã���������һ�š���������Ϊ��ɫ�������Կɴ�������ʹ�ã�������");
        CardInfo e4 = new CardInfo("GuanShiFu", "������Χ��3��\n������Ч��������Ч��Ŀ���ɫʹ�á�����������ʹ�á�ɱ����Ч��ʱ��������������ƣ���ɱ����Ȼ����˺���");
        CardInfo e5 = new CardInfo("JiaMa", "װ��Ч����������ɫ��������ľ���ʱ��ʼ��+1��");
        CardInfo e6 = new CardInfo("JianMa", "װ��Ч����������ɫ��������ľ���ʱ��ʼ��-1��");
        CardInfo e7 = new CardInfo("QiLinGong", "������Χ��5��\n������Ч����ʹ�á�ɱ����Ŀ���ɫ����˺�ʱ������Խ���װ�������һƥ�����á�");
        CardInfo e8 = new CardInfo("QingGangJian", "������Χ��2��\n������Ч����������ÿ����ʹ�á�ɱ��ʱ������Ŀ���ɫ�ķ��ߡ�");
        CardInfo e9 = new CardInfo("QingLongYanYueDao", "������Χ��3��\n�������ܣ�����ʹ�õġ�ɱ��������ʱ���������������ͬ��Ŀ����ʹ��һ�š�ɱ����");
        CardInfo e10 = new CardInfo("ZhangBaSheMao", "������Χ��3��\n������Ч��������Ҫʹ�ã�������һ�š�ɱ��ʱ������Խ��������Ƶ�һ�š�ɱ��ʹ�ã���������");
        CardInfo e11 = new CardInfo("ZhuGeLianNu", "������Χ��1��\n������Ч�����ƽ׶Σ������ʹ�������š�ɱ����");
        CardInfo e12 = new CardInfo("ZhuQueYuShan", "������Χ��4��\n������Ч������Խ������һ��ͨɱ�����߻����˺���ɱ��ʹ�á�");
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
    /// ��ȡ�����е�����
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
    /// ��ȡ�����е���ϸ��Ϣ
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
