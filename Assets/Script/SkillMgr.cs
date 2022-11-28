using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Buff { provoked,sleep}
public class SkillMgr : MonoBehaviour
{
    public GameObject[] Buffs;
    public GameObject skillRangePrefab;

    private IEnumerator skillNow;//스킬누르고 클릭해야 나가는 스킬 처리용

    float knightskill1Duration=15;
    float knightskill2Duration=10;
    float knightskill3Duration=5;
    float knightskill4Duration;

    //모든 스킬의 정보 관리
    // 쿨타임 관리
    //이펙트 소환
    // 인디케이터 표시

    public Knight knight;
    private void OnEnable()
    {
        GameMgr.Instance.inputActions.Command.SkillButton1.performed += OnSkill1;
        GameMgr.Instance.inputActions.Command.SkillButton2.performed += OnSkill2;
        GameMgr.Instance.inputActions.Command.SkillButton3.performed += OnSkill3;
        GameMgr.Instance.inputActions.Command.SkillButton4.performed += OnSkill4;
        GameMgr.Instance.inputActions.Command.SkillCancel.performed += OnSkillCancel;
    }

    private void OnSkillCancel(InputAction.CallbackContext obj)
    {
        
    }

    private void OnSkill1(InputAction.CallbackContext obj)
    {
        StartCoroutine(PlaySkillOnHero(KnightSkill.shieldAura, knightskill1Duration));
        StartCoroutine(knight.EnumeratorTimer(knight.shieldAuraCor, knightskill1Duration));
    }

    private void OnSkill2(InputAction.CallbackContext obj)
    {
        StartCoroutine(PlaySkillOnHero(KnightSkill.provoke, knightskill2Duration));
        StartCoroutine(knight.ProvokeCor(knightskill2Duration));
    }

    private void OnSkill3(InputAction.CallbackContext obj)
    {
        StartCoroutine(PlaySkillOnHero(KnightSkill.frenzy, knightskill3Duration));
        StartCoroutine(knight.EnumeratorTimer(knight.frenzyCor, knightskill3Duration));
    }

    private void OnSkill4(InputAction.CallbackContext obj)
    {
        ShowSkillRange(5);
        //StartCoroutine(PlaySkillOnHero(KnightSkill.finishMove, 5));
    }

    public void UseSkill(int i)
    {

    }
    public IEnumerator PlaySkillOnHero(KnightSkill skill, float sec)
    {
        GameObject obj = Instantiate(knight.skillEffects[(int)skill], GameMgr.Instance.commandMgr.SelectedHero.transform);

        yield return new WaitForSeconds(sec);
        Destroy(obj);
    }
    public void ShowSkillRange(float skillRange)
    {
        GameObject obj=Instantiate(skillRangePrefab, GameMgr.Instance.commandMgr.SelectedHero.transform);
        obj.transform.localScale=new Vector3(skillRange,0,skillRange);
    }
}