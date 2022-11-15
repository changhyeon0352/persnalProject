using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitGroup:MonoBehaviour
{
    List<AllyUnit> unitList;
    private UnitType unitType = UnitType.none;
    public Transform unitsTr;
    public Transform spotsTr;
    public Transform AllyGroups;
    public Vector2Int rowColumn=Vector2Int.zero;

    Skills groupSkill =Skills.None;

    public Skills GroupSkill { get => groupSkill; }
    public UnitType UnitType { get => unitType;}

    private void Awake()
    {
        unitList = new List<AllyUnit>();
        AllyGroups = FindObjectOfType<AllyUnitGroups>().transform;
    }
    public void CheckSelected()
    {
        bool isSelected = (CommandMgr.Instance.SelectedGroupList.Contains(this));
        if(!isSelected&& CommandMgr.Instance.SelectedHero!=null)
        {
            isSelected= CommandMgr.Instance.SelectedHero==this.GetComponentInChildren<Hero>();
        }
        for(int i=0;i<unitList.Count;i++)
        {
            unitList[i].IsSelectedUnit = isSelected;
        }
        
    }
    public void InitializeUnitList()
    {
        unitList.Clear();
        
        for (int i = 0; i < unitsTr.childCount; i++)
        {
            AllyUnit allyUnit = transform.GetChild(0).GetChild(i).GetComponent<AllyUnit>();
            unitList.Add(allyUnit);
        }
        
        tag = transform.GetChild(0).GetChild(0).tag;
        if (CompareTag("Melee"))
        {
            groupSkill |= Skills.MoveToSpot;
            groupSkill |= Skills.Charge;
            unitType = UnitType.soldier;
        }
        else if (CompareTag("Range"))
        {
            groupSkill |= Skills.Shoot;
            unitType = UnitType.soldier;
        }
        else if(CompareTag("Knight"))
        {
            unitType = UnitType.hero;
        }
    }
    public void RemoveUnitFromList(AllyUnit unit)
    {
        unitList.Remove(unit);
        if(unitList.Count== 0)
        {
            CommandMgr.Instance.SelectedGroupList.Remove(this);
            Destroy(gameObject);
        }
    }
    
}