using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
public class MyJoyStickModule : EventDispatcher
{
    public EventListener onStart {
        get; private set;
    }
    public EventListener onMove {
        get; private set;
    }
    public EventListener onEnd {
        get; private set;
    }


    private GComponent fightCom;

    private GObject thumb;
    private GObject center;
    private GButton moveBtn;



    private Vector2 initPos = new Vector2();
    private Vector2 startPos = new Vector2();
    private Vector2 lastPos = new Vector2();
    private Vector2 nowPos = new Vector2();

    private Vector2 maxDistance = new Vector2();

    private int touchId = -1;
    private float radius;

    private GTweener tween = new GTweener();

    private InputEvent input;

    private bool isTouching = false;

    private Vector2 offset =new Vector2();

    public MyJoyStickModule(GComponent gcm)
    {
        fightCom = gcm.GetChild("moveCom").asCom;
        onMove = new EventListener(this, "onMove");
        onEnd = new EventListener(this, "onEnd");
        onStart = new EventListener(this, "onStart");

        moveBtn = fightCom.GetChild("joyStickIcon").asButton;

        // moveBtn.changeStateOnClick = false;
        //  initPos= new Vector2(moveBtn.x , moveBtn.y );
        initPos = new Vector2(moveBtn.x , moveBtn.y);





        touchId = -1;
        radius = fightCom.width / 2;
        maxDistance = new Vector2(initPos.x + radius, initPos.y + radius);
    

        moveBtn.onTouchBegin.Add(onTouchBegin);
        
        moveBtn.onTouchMove.Add(onTouchMove);
        moveBtn.onTouchEnd.Add(onTouchEnd);

        offset = new Vector2(fightCom.x + moveBtn.width / 2, fightCom.y + moveBtn.height / 2);
    }

    public void onTouchBegin(EventContext context)
    {


        if (touchId == -1)
        { //第一次触摸
            input = context.inputEvent;

            touchId = input.touchId;
            if (tween != null)
            {
                tween.Kill();
                tween = null;
            }
            nowPos = GRoot.inst.GlobalToLocal(new Vector2(input.x, input.y));
            startPos = nowPos;
            lastPos = nowPos;


            context.CaptureTouch();
            isTouching = true;

            onStart.Call();
            // while (isTouching) {
            //      onTouchMove(context);
            // }

        }


    }
    public void onTouchMove(EventContext context)
    {

        if (touchId != -1)
        {

            input = context.inputEvent;

            touchId = input.touchId;

            nowPos = GRoot.inst.GlobalToLocal(new Vector2(input.x, input.y));
            //   Debug.Log(nowPos -new Vector2(fightCom.x, fightCom.y));
            // nowPos = new Vector2(nowPos.x - lastPos.x+moveBtn.x+moveBtn.width/2, nowPos.y - lastPos.y+ moveBtn.y +moveBtn.height / 2);
            nowPos = nowPos - offset;

            
            
            float x = nowPos.x - initPos.x;
            float y = nowPos.y - initPos.y;
            

            float rad = Mathf.Atan2(y, x);


            float degree = (rad * 180) / Mathf.PI;
           

            //if (rad < 0)
            //{
            //    if (nowPos.x < initPos.x)
            //    {
            //        rad += Mathf.PI;
            //    }
            //    else
            //    {
            //        rad += 2 * Mathf.PI;
            //    }
            //}
            //else
            //{
            //    if (nowPos.x < initPos.x)
            //    {
            //        rad += Mathf.PI;
            //    }
            //}
            //rad = (rad * 180) / Mathf.PI;

            float rx = radius * Mathf.Cos(rad);
            float ry = radius * Mathf.Sin(rad);

          //  Debug.Log(rad);

            if (Mathf.Abs(x) > Mathf.Abs(rx))
            {
                x = rx;
            }
            if (Mathf.Abs(y) > Mathf.Abs(ry))
            {
                y = ry;
            }

            Vector2 detalPos = new Vector2(x + initPos.x,  initPos.y+y);
            moveBtn.TweenMove(detalPos, 0.1f);

           
         //   Debug.Log("  x:" + x / Mathf.Abs(rx) + "  y:" + y / Mathf.Abs(ry));
          // onMove.Call(new Vector3(rad, degree, x / Mathf.Cos(rad) / radius));
            onMove.Call(new Vector2(  Mathf.Cos(rad), -Mathf.Sin(rad)));

            context.CaptureTouch();

            lastPos = nowPos;
        }

        //  context.CaptureTouch();
    }
    public void onTouchEnd(EventContext context)
    {
        input = context.inputEvent;
        isTouching = false;
        if (touchId != -1 && input.touchId == touchId)
        {
            touchId = -1;

            tween = moveBtn.TweenMove(initPos, 1f).OnComplete(() => {
                tween = null;
                moveBtn.selected = false;

            });



        }
        onEnd.Call();
    }
}
