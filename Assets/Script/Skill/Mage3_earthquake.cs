using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage3_earthquake : Skill
{
    public override void InitSetting()
    {
        data.skillType = SkillType.AreaTarget;
        data.coolTime = 25f;
        data.range = 8f;
        data.nonTargetRange = 5f;
        data.damage = 5;
        data.duration = 10f;// playskillontr을 반복하기 위해서
    }
    public override void EffectOnUnit(Unit unit, HeroUnit hero)
    {
        unit.TakeDamage(data.damage + unit.UnitData.MagicPower/2);
        SlowEffect slowEffect = new SlowEffect(unit, 1f);
        StartCoroutine(unit.StatusEffectCoroutine(slowEffect));
    }
    public override IEnumerator SkillCor(Transform skillTr, HeroUnit hero)
    {
        StartCoroutine(PlaySkillOnTr(skillTr));
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < data.duration; i++)
        {
            AoeSkill(skillTr, hero);
            yield return new WaitForSeconds(1f);
        }
    }
}
