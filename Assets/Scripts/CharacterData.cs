using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterData : MonoBehaviour
{
    #region 参数
    // Start is called before the first frame update
    public int currentHP;//当然的血量
    public int maxHP;//最大血量
    public string currentOutCardName;//当前出的锦囊牌
    public GameObject curClickCard;//当前点击的手牌的游戏物体
    public GameObject EnemyCardUI; //抽敌人牌的UI
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
    public RoundType curRound;//出牌状态
    public PlayerType curPlayer;
    public List<GameObject> ownCardList = new List<GameObject>();//自身卡牌列表
    public List<GameObject> ownEquipCardList = new List<GameObject>();//装备游戏物体列表
    public List<GameObject> ownAllCardList = new List<GameObject>();//全部的卡牌列表
    public AudioController audioController;
    private Transform disCardPos;
    #endregion

    //public EnemyAI enemyAI;
    //public PlayerController playerController;

    public GameObject weaponCard; //武器牌
    public GameObject armorCard; //防具牌
    public GameObject jiaMaCard; //加一马
    public GameObject jianMaCard;

    #region 接口函数
    /// <summary>
    /// 获取自身卡牌的游戏物体
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetownCardListGo(string name)
    {
        for (int i = 0; i < ownCardList.Count; i++)
        {
            if (ownCardList[i].name == name)
            {
                return ownCardList[i].gameObject;
            }
        }
        return null;
    }
    /// <summary>
    /// 获取自身装备卡牌的游戏物体
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetEquipCard(string name)
    {
        for (int i = 0; i < ownEquipCardList.Count; i++)
        {
            if (ownEquipCardList[i].name == name)
            {
                return ownEquipCardList[i].gameObject;
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
        for (int i = 0; i < ownAllCardList.Count; i++)
        {
            if (ownAllCardList[i].name == name)
            {
                return ownAllCardList[i].gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// 更新自身牌列表
    /// </summary>
    /// <param name="curPlayer"></param>
    public void ownCarList(PlayerType curPlayer)
    {
        if (curPlayer == PlayerType.Player)
        {
            ownCardList.RemoveRange(0, ownCardList.Count);
            FindChild(GameObject.Find("PlayerCardList").gameObject);
        }
        if (curPlayer == PlayerType.Enemy)
        {
            ownCardList.RemoveRange(0, ownCardList.Count);
            FindChild(GameObject.Find("EnemyCardList").gameObject);
        }
    }


    /// <summary>
    /// 获取子物体添加到列表中
    /// </summary>
    /// <param name="Go"></param>
    public void FindChild(GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).childCount > 0)
            {
                FindChild(parent.transform.GetChild(i).gameObject);
            }
            ownCardList.Add(parent.transform.GetChild(i).gameObject);
        }
    }
    #endregion
   


    void Start()
    {
        //audioController = GetComponent<AudioController>();
       disCardPos = GameObject.Find("DisCardPos").transform;
        ////enemyAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
        ////playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
       EnemyCardUI = GameObject.Find("EnemyCardUI").gameObject;
    }
    void Update()
    {
       UpdateOwnEquip();

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    ownCardList(curPlayer);
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    OutCrad(currrentClickCardGo);
        //}
    }
    /// <summary>
    /// 获取自身有没有装备
    /// </summary>
    public void UpdateOwnEquip()
    {
        if (curPlayer == PlayerType.Player)
        {
            if (isWeapon == true && GameObject.Find("PlayerWeapon").transform.childCount > 0)
            {
                weaponCard = GameObject.Find("PlayerWeapon").transform.GetChild(0).gameObject;
            }
            else
            {
                isWeapon = false;
            }
            if (isArmor == true && GameObject.Find("PlayerArmor").transform.childCount > 0)
            {
                armorCard = GameObject.Find("PlayerArmor").transform.GetChild(0).gameObject;
            }
            else
            {
                isArmor = false;
            }
            if (isJiaMa == true && GameObject.Find("PlayerJiaMa").transform.childCount > 0)
            {
                jiaMaCard = GameObject.Find("PlayerJiaMa").transform.GetChild(0).gameObject;
            }
            else
            {
                isJiaMa = false;
            }
            if (isJianMa == true && GameObject.Find("PlayerJianMa").transform.childCount > 0)
            {
                jianMaCard = GameObject.Find("PlayerJianMa").transform.GetChild(0).gameObject;
            }
            else
            {
                isJianMa = false;
            }
        }


        if (curPlayer == PlayerType.Enemy)
        {
            if (isWeapon == true && GameObject.Find("EnemyWeapon").transform.childCount > 0)
            {
                weaponCard = GameObject.Find("EnemyWeapon").transform.GetChild(0).gameObject;
            }
            else
            {
                isWeapon = false;
            }
            if (isArmor == true && GameObject.Find("EnemyArmor").transform.childCount > 0)
            {
                armorCard = GameObject.Find("EnemyArmor").transform.GetChild(0).gameObject;
            }
            else
            {
                isArmor = false;
            }
            if (isJiaMa == true && GameObject.Find("EnemyJiaMa").transform.childCount > 0)
            {
                jiaMaCard = GameObject.Find("EnemyJiaMa").transform.GetChild(0).gameObject;
            }
            else
            {
                isJiaMa = false;
            }
            if (isJianMa == true && GameObject.Find("EnemyJianMa").transform.childCount > 0)
            {
                jianMaCard = GameObject.Find("EnemyJianMa").transform.GetChild(0).gameObject;
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
    public void OutCrad(GameObject _card)
    {
        if (ownCardList.Count != 0)
        {
            for (int i = 0; i < ownCardList.Count; i++)
            {
                if (ownCardList[i] == null) return;
                if (curPlayer == PlayerType.Player)
                {
                    if (ownCardList[i].name == _card.name && ownCardList[i].gameObject.GetComponent<Toggle>().isOn == true)
                    {
                        if (_card.name == "Sha" && curRound == RoundType.MeiChuPai)
                        {
                            outShaNum -= 1;
                        }
                        if (_card.name == "WuXieKeJi")
                        {
                            isWuXie = false;
                            isOutWuXie = true;
                            audioController.PlayAudio(_card.name);
                            StartCoroutine(FunctionCard(currentOutCardName));
                            StartCoroutine(FunctionCard(ownCardList[i].name));
                            ownCardList[i].gameObject.GetComponent<Toggle>().graphic = null;
                            OutCardMoveTarGetPos(ownCardList[i].gameObject);
                        }
                        else
                        {
                            audioController.PlayAudio(_card.name);
                            StartCoroutine(FunctionCard(ownCardList[i].name));
                            ownCardList[i].gameObject.GetComponent<Toggle>().graphic = null;
                            OutCardMoveTarGetPos(ownCardList[i].gameObject);
                        }
                        curClickCard = null;
                    }
                }
                if (curPlayer == PlayerType.Enemy)
                {
                    if (ownCardList[i].name == _card.name && _card != null)
                    {
                        if (currentNeedOutCardName == _card.name && curRound == RoundType.NeedOutPai)
                        {
                            if (_card.name == "WuXieKeJi")
                            {
                                ownCardList[i].GetComponent<Image>().sprite = GetCard_card(_card.name).GetComponent<Image>().sprite;
                                playerController.thisData.isWuXie = true;
                                isOutWuXie = true;
                                audioController.PlayAudio(_card.name);
                                StartCoroutine(FunctionCard(currentOutCardName));
                                StartCoroutine(FunctionCard(ownCardList[i].name));
                                OutCardMoveTarGetPos(ownCardList[i].gameObject);
                                curRound = curRound.Wait;
                            }
                            else
                            {
                                ownCardList[i].GetComponent<Image>().sprite = GetCard_card(_card.name).GetComponent<Image>().sprite;
                                audioController.PlayAudio(_card.name);
                                StartCoroutine(FunctionCard(ownCardList[i].name));
                                OutCardMoveTarGetPos(ownCardList[i].gameObject);
                                curRound = curRound.Wait;
                            }
                        }
                        if (curRound == RoundType.ChuPai)
                        {
                            if (_card.name == "Sha")
                            {
                                outShaNum -= 1;
                            }
                            ownCardList[i].GetComponent<Image>().sprite = GetCard_card(_card.name).GetComponent<Image>().sprite;
                            audioController.PlayAudio(_card.name);
                            StartCoroutine(FunctionCard(ownCardList[i].name));
                            OutCardMoveTarGetPos(ownCardList[i]);
                            curRound = curRound.Wait;
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
        for (int i = 0; i < ownCardList.Count; i++)
        {
            if (ownCardList[i].name == name && ownCardList[i].gameObject.GetComponent<Toggle>().isOn == true)
            {
                OutCardMoveToDisCard(ownCardList[i].gameObject);
            }
        }
    }
    /// <summary>
    /// 移动牌
    /// </summary>
    /// <param name="gameObject"></param>
    public void OutCardMoveToDisCard(GameObject gameObject)
    {
        if (curPlayer == PlayerType.Player)
        {
            Destroy(gameObject.transform.GetComponent<Toggle>());
        }
        gameObject.GetComponent<Image>().sprite = GetCardGo(gameObject.name).GetComponent<Image>().sprite;
        gameObject.transform.DOMove(disCardPos.position, 0.5f).OnComplete(() =>
        {
            DestroyCard(gameObject);
        });
    }

    public void EnemyCardMovePlayerCard(GameObject thisGO, GameObject tarGo, PlayerType curPlayer)
    {
        thisGO.transform.DOMove(tarGo.transform.position, 0.5f).OnComplete(() =>
        {
            if (curPlayer == PlayerType.Player)
            {
                ownCardList.Remove(thisGO);
                Destroy(thisGO);
                GameObject GO = Instantiate(GetCardGo(thisGO.name));
                playerController.thisData.ownCardList.Add(GO);
                GO.name = GetCardGo(thisGO.name).name;
                GO.transform.SetParent(tarGo.transform);
                GO.transform.localScale = Vector3.one;
            }
            if (curPlayer == curPlayer.Enemy)
            {
                ownCardList.Remove(thisGO);
                Destroy(thisGO);
                GameObject GO = Instantiate(GetCardGo(thisGO.name));
                enemyAI.thisData.ownCardList.Add(GO);
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
        ownCardList.Remove(gameObject);
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
        if (curPlayer == PlayerType.Player)
        {
            switch (name)
            {
                case "Jiu":
                    isJiu = true;
                    break;
                case "Sha":
                    if (curRound == curRound.ChuPai && isJueDou == false && enemyAI.thisData.isJueDou == false)
                    {
                        if (isJiu == false)
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
                    if (enemyAI.thisData.isChouPai == true)
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
                    Equip(name, EquipType.Armor, curPlayer);
                    break;
                case "BaiYinShiZi":
                    isArmor = true;
                    Equip(name, EquipType.Armor, curPlayer);
                    break;
                case "CiXiongShuangJian":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 2;
                    break;
                case "GuanShiFu":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 3;
                    break;
                case "JiaMa":
                    isJiaMa = true;
                    Equip(name, EquipType.JiaMa, curPlayer);
                    ziShenDis = 2;
                    break;
                case "JianMa":
                    isJianMa = true;
                    Equip(name, EquipType.JianMa, curPlayer);
                    outJinNangDis = 2;
                    break;
                case "QiLinGong":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 5;
                    break;
                case "QingGangJian":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 2;
                    break;
                case "QingLongYanYueDao":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 3;
                    break;
                case "ZhangBaSheMao":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 3;
                    break;
                case "ZhuGeLianNu":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 1;
                    break;
                case "ZhuQueYuShan":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 4;
                    break;
                default:
                    break;
            }
        }
        if (curPlayer == PlayerType.Enemy)
        {
            switch (name)
            {
                case "Jiu":
                    isJiu = true;
                    break;
                case "Sha":
                    if (isJueDou == true || playerController.thisData.isJueDou == true)
                    {
                        playerController.EnemyPlayCard(name);
                    }
                    if (curRound == curRound.ChuPai)
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
                    Equip(name, EquipType.Armor, curPlayer);
                    break;
                case "BaiYinShiZi":
                    isArmor = true;
                    Equip(name, EquipType.Armor, curPlayer);
                    break;
                case "CiXiongShuangJian":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 2;
                    break;
                case "GuanShiFu":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 3;
                    break;
                case "JiaMa":
                    isJiaMa = true;
                    Equip(name, EquipType.JiaMa, curPlayer);
                    ziShenDis = 2;
                    break;
                case "JianMa":
                    isJianMa = true;
                    Equip(name, EquipType.JianMa, curPlayer);
                    outJinNangDis = 2;
                    break;
                case "QiLinGong":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 5;
                    break;
                case "QingGangJian":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 2;
                    break;
                case "QingLongYanYueDao":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 3;
                    break;
                case "ZhangBaSheMao":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 3;
                    break;
                case "ZhuGeLianNu":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
                    attackDis = 1;
                    break;
                case "ZhuQueYuShan":
                    isWeapon = true;
                    Equip(name, EquipType.Weapon, curPlayer);
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
    /// <param name="curPlayer"></param>
    public void Equip(string name, EquipType equipType, PlayerType curPlayer)
    {
        audioController.PlayAudio("ZhuangBei");
        GameObject go = Instantiate(GetEquipCard(name));
        go.name = name;
        Transform ts = null;
        switch (equipType)
        {
            case EquipType.Armor:
                if (curPlayer == curPlayer.Player)
                {
                    ts = GameObject.Find("PlayerArmor").transform;
                }
                if (curPlayer == curPlayer.Enemy)
                {
                    ts = GameObject.Find("EnemyArmor").transform;
                }
                break;
            case EquipType.Weapon:
                if (curPlayer == curPlayer.Player)
                {
                    ts = GameObject.Find("PlayerWeapon").transform;
                }
                if (curPlayer == curPlayer.Enemy)
                {
                    ts = GameObject.Find("EnemyWeapon").transform;
                }
                break;
            case EquipType.JiaMa:
                if (curPlayer == curPlayer.Player)
                {
                    ts = GameObject.Find("PlayerJiaMa").transform;
                }
                if (curPlayer == curPlayer.Enemy)
                {
                    ts = GameObject.Find("EnemyJiaMa").transform;
                }
                break;
            case EquipType.JianMa:
                if (curPlayer == curPlayer.Player)
                {
                    ts = GameObject.Find("PlayerJianMa").transform;
                }
                if (curPlayer == curPlayer.Enemy)
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
    
}
