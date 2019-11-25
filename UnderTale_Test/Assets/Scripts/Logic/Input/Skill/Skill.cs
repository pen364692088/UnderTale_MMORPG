using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//public interface ISkillEventHandler
//{
//    void OnSkillStart(Skill skill);
//    void OnSkillDone(Skill skill);
//    void OnSkillPartStart(Skill skill);
//}

[Serializable]
public class Skill 
{

    public enum ESkillState
    {
        Idle,
        Firing,
    }


    private static readonly HashSet<Entity> _tempTargets = new HashSet<Entity>();

    public ISkillEventHandler eventHandler;

    public Player player { get; private set; }
    public SkillInfo SkillInfo;
    public float CD => SkillInfo.CD;
    public float DoneDelay => SkillInfo.doneDelay;
    public List<SkillPart> Parts => SkillInfo.parts;
    public int TargetLayer => SkillInfo.targetLayer;
    public float MaxPartTime => SkillInfo.maxPartTime;
    public AnimationCode AnimName => SkillInfo.animName;

    public float CdTimer;
    public ESkillState _state;
    private float _skillTimer;
    private SkillPart _curPart;

    public void ForceStop() { }

    public void Init(Player entity, SkillInfo info, ISkillEventHandler eventHandler)
    {
        this.player = entity;
        this.SkillInfo = info;
        this.eventHandler = eventHandler;
        _skillTimer = MaxPartTime;
        _state = ESkillState.Idle;
        _curPart = null;
    }

    public bool Fire()
    {
        if (CdTimer <= 0 && _state == ESkillState.Idle)
        {
            CdTimer = CD;
            _skillTimer = 0;
            foreach (var part in Parts)
            {
                part.counter = 0;
            }

            _state = ESkillState.Firing;
            //   entity.animator?.Play(AnimName);
            player.ani.Play(AnimName);
            player.canMove = false;
             //entity.needMove = false;
            OnFire();
            return true;
        }

        return false;
    }
    public void OnFire()
    {
        eventHandler.OnSkillStart(this);
    }
    public void Done()
    {
        eventHandler.OnSkillDone(this);
        _state = ESkillState.Idle;
    }
    public void DoUpdate(float deltaTime)
    {
        
        CdTimer -= deltaTime;
        _skillTimer += deltaTime;
        if (_skillTimer < MaxPartTime)
        {
            foreach (var part in Parts)
            {
                CheckSkillPart(part);
            }
        }
        else
        {
            _curPart = null;
            if (_state == ESkillState.Firing)
            {
                Done();
            }
        }
#if DEBUG_SKILL
            if (_showTimer < UnityEngine.Time.realtimeSinceStartup)
            {
                _curPart = null;
            }
#endif

    }
    void CheckSkillPart(SkillPart part)
    {
        if (part.counter > part.otherCount) return;
        if (_skillTimer > part.NextTriggerTimer())
        {
            TriggerPart(part);
            part.counter++;
        }
    }
    void TriggerPart(SkillPart part)
    {
        eventHandler.OnSkillPartStart(this);
        _curPart = part;
#if DEBUG_SKILL
            _showTimer = UnityEngine.Time.realtimeSinceStartup + 0.1f;
#endif

        var col = part.collider;
        if (col.radius > 0)
        {
            //circle
            //CollisionManager.QueryRegion(TargetLayer, entity.transform.TransformPoint(col.pos), col.radius,
            //    _OnTriggerEnter);
        }
        else
        {
            //aabb
            //CollisionManager.QueryRegion(TargetLayer, entity.transform.TransformPoint(col.pos), col.size,
            //    entity.transform.forward,
            //    _OnTriggerEnter);
        }

        foreach (var other in _tempTargets)
        {
            //other.Entity.TakeDamage(_curPart.damage, other.Entity.transform.pos.ToLVector3());
        }

        //add force
        if (part.needForce)
        {
            var force = part.impulseForce;
            var forward = player.obj.transform.forward;
            var right = forward;
            var z = forward * force.z + right * force.x;
            force.x = z.x;
            force.z = z.y;
            foreach (var other in _tempTargets)
            {
                //other.Entity.rigidbody.AddImpulse(force);
            }
        }

        if (part.isResetForce)
        {
            foreach (var other in _tempTargets)
            {
                //other.Entity.rigidbody.ResetSpeed(new LFloat(3));
            }
        }

        _tempTargets.Clear();
    }
    //private void _OnTriggerEnter(ColliderProxy other)
    //{
    //    if (_curPart.collider.IsCircle && _curPart.collider.deg > 0)
    //    {
    //        var deg = (other.Transform2D.pos - entity.transform.pos).ToDeg();
    //        var degDiff = entity.transform.deg.Abs() - deg;
    //        if (LMath.Abs(degDiff) <= _curPart.collider.deg)
    //        {
    //            _tempTargets.Add(other);
    //        }
    //    }
    //    else
    //    {
    //        _tempTargets.Add(other);
    //    }
    //}
    public void OnDrawGizmos()
    {
#if UNITY_EDITOR && DEBUG_SKILL
            float tintVal = 0.3f;
            Gizmos.color = new Color(0, 1.0f - tintVal, tintVal, 0.25f);
            if (Application.isPlaying) {
                if (entity == null) return;
                if (_curPart == null) return;
                ShowPartGizmons(_curPart);
            }
            else {
                foreach (var part in Parts) {
                    if (part._DebugShow) {
                        ShowPartGizmons(part);
                    }
                }
            }

            Gizmos.color = Color.white;
#endif
    }

    private void ShowPartGizmons(SkillPart part)
    {
#if UNITY_EDITOR
        var col = part.collider;
        if (col.radius > 0)
        {
            //circle
            var pos =  col.pos;
            Gizmos.DrawSphere(pos, col.radius);
        }
        else
        {
            //aabb
            var pos =col.pos;
            Gizmos.DrawCube(pos, col.size);
        }
#endif
    }

}
