using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Object/Unit Data", order = int.MinValue)]
public class UnitData : ScriptableObject
{
    private HeroData heroData;
    public HeroData HeroData { set { heroData = value; } }
    [SerializeField]
    private UnitType type;
    public UnitType Type { get { return type; } }
    [SerializeField]
    private Sprite unitPortrait;
    public Sprite UnitPortrait { get { return unitPortrait; } }
    [SerializeField] 
    private string unitName;
    public string UnitName { get { return unitName; } }
    [SerializeField]
    private int hp;
    public int HP { get { return hp; } }
    [SerializeField]
    private int mp;
    public int MP { get { return mp; } }
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField]
    private int atk;
    public int Atk { get { return atk; } }
    [SerializeField]
    private int magicPower;
    public int MagicPower { get { return magicPower; } }
    [SerializeField]
    private int armor;
    public int Armor { get {return armor; } }
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }
    [SerializeField]
    private int cost;
    public int Cost { get { return cost; } }
    [SerializeField]private float attackRange;
    public float AttackRange { get { return attackRange; } }
    [SerializeField] private float searchRange;
    public float SearchRange { get { return searchRange; } }
    public GameObject unitPrefab;
    [SerializeField]
    private float[] attackAniLength;
    public float[] AttackAniLength { get { return attackAniLength; } }
}
