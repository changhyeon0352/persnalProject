//활을 쏘는 애니메이션이 실행될때 모션에 따라 취하는 행동
//활이 휘어지게 화살이 나오게 화살을 목표에 쏘게

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

public class BowLoadShot : MonoBehaviour
{
	   
	public Transform bow;
	public Transform arrowLoad;   //평소엔 바닥에 쏠때 몸 중간 쯤으로 올라와서 화살이 로딩되는 만큼 뒤로감
    private ObjectPooling arrowPool;
    //Bow Blendshape
    SkinnedMeshRenderer bowSkinnedMeshRenderer;

    //Arrow draw & rotation
    private bool isShotSpot;
	public bool arrowOnHand;
	public Transform arrowToDraw;  //화살 쏘는척 할꺼 
    bool isShootingEnd = true;
    private float shotAngle=10;
    public float ShotAngle { get { return shotAngle; } set { shotAngle = value; } }
    [SerializeField] float accuracy = 0f;
    private Transform target;

	void Awake()
	{

        arrowPool= GameMgr.Instance.ObjectPools.GetObjectPool(ObjectPoolType.Arrow);
        if (bow != null)
		{
			bowSkinnedMeshRenderer = bow.GetComponent<SkinnedMeshRenderer>();
		}
			
		if(arrowToDraw != null)
		{
			arrowToDraw.gameObject.SetActive(false);
		}
	}
    public void SetEnemyMode()
    {
        ShotAngle = 5;
        isShotSpot = false;
    }
    public void SetTarget(Transform target)
    {
        this.target = target;   
    }
    public void SetSpotMode(Transform tr)
    {
        ShotAngle = 40;
        target = tr;
        isShotSpot = true;
    }
	void Update()
	{
        if(target != null)
        {
            //if(oldTarget!=target||correctionTarget == null)
            //{
            //    GameObject obj = new GameObject();
            //Debug.Log("계속 생성");
            //    obj.transform.position=target.position;
            //    obj.transform.Translate(transform.right * -0.25f+Vector3.up*0.7f);
            //    correctionTarget = obj.transform;
            //    correctionTarget.parent = target;
            //    oldTarget = target;
            //}
            transform.forward = new Vector3(target.position.x-transform.position.x,0,target.position.z-transform.position.z);

            //Bow blendshape animation 오른팔을 뒤로 뺄수록 활이 휘어짐
            if (bowSkinnedMeshRenderer != null && bow != null && arrowLoad != null)
            {
                float bowWeight = Mathf.InverseLerp(0, -0.7f, arrowLoad.localPosition.z);
                //위치를 백분률(0~1)로 0과 -0.7 사이 어디쯤인지 반환 
                bowSkinnedMeshRenderer.SetBlendShapeWeight(0, bowWeight * 100);//블렌더 가중치 활을 휘게함
            }

            //Draw arrow from quiver and rotate it  화살통에서 화살을 빼고 돌림
            if (arrowToDraw != null &&  arrowLoad != null)
            {
                if (isShootingEnd && arrowLoad.localPosition.y > 0.5f)
                {
                    if (arrowToDraw != null)
                    {
                        arrowOnHand = true;
                        isShootingEnd = false;
                        arrowToDraw.gameObject.SetActive(true);// 화살(드로우용)이 보이게
                    }
                }


                if (arrowLoad.localScale.z < 1f && arrowOnHand)//완전히 땡겨을 때
                {
                    if (arrowToDraw != null)
                    {
                        if(isShotSpot)
                        {
                            ShotToPos(RandomPosInCircle(target.position,AllyRange.SpotRadius));
                        }
                        else
                            ShotToPos(target.position);
                    }
                }
                if (arrowLoad.localPosition == Vector3.zero)
                {
                    isShootingEnd = true;
                }
            }
        }
		
	}
    private Vector3 RandomPosInCircle(Vector3 center,float radius)
    {
        Vector3 pos = center;
        Vector2 randomPoint = Random.insideUnitCircle.normalized * Random.Range(0, radius);
        pos += new Vector3(randomPoint.x,0,randomPoint.y);

        return pos;
    }
    private void ShotToPos(Vector3 targetPos)
    {
        GameObject arrowObj = arrowPool.GetObjectFromPool();
        Transform aTr = arrowObj.transform;
        aTr.position = arrowToDraw.position;
        aTr.forward = (targetPos - transform.position).normalized;
        aTr.rotation *= Quaternion.Euler(new Vector3(-shotAngle, 0, 0));
        
        aTr.parent = null;
        float distance = Vector2.Distance(new Vector2(aTr.position.x, aTr.position.z),
            new Vector2(targetPos.x, targetPos.z));
        float deltaH = aTr.position.y - targetPos.y-1;
        Arrow arrow = aTr.GetComponent<Arrow>();
        arrow.SetSpeed(GetArrowVelocity(distance, deltaH, arrow.GravityForce));
        Vector3 noise = Random.insideUnitSphere * (1 - accuracy);
        arrow.MakeNoise(noise);
        arrowToDraw.gameObject.SetActive(false);
        arrowOnHand = false;
    }
    private float GetArrowVelocity(float L, float dH, float g)
    {
        float vel;
        float cos=Mathf.Cos(shotAngle*Mathf.Deg2Rad);
        float sin = Mathf.Sin(shotAngle * Mathf.Deg2Rad);
        vel = Mathf.Sqrt((g * g * L * L) / (cos * cos) / (2 * g * L * sin / cos + 2 * g * (dH)));
        return vel;
    }
    
}

