using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlowlyRotate : MonoBehaviour
{
    public float RotateSpeed = 35f;
    public List<GameObject> Targets = new List<GameObject>();
    public TextMeshProUGUI txtDisplay = null;

    private int Current = 0;
    private bool xTurn = true;
    private bool yTurn = true;
    private bool zTurn = true;

    private void Update()
    {
        UserInput();
        SlowlyRotateTowardTarget();
        UpdateTextDisplay();
    }
    private void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            Current = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {
            Current = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) == true)
        {
            Current = 2;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
            Current += 1;
            if (Current >= Targets.Count)
                Current = 0;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
            Current -= 1;
            if (Current < 0)
                Current = Targets.Count - 1;
        }
    }
    private void SlowlyRotateTowardTarget()
    {
        // 1. what are source and target rotations currently?
        Transform src = this.transform;
        Transform tgt = Targets[Current].transform;

        // 2. what is the rotation to point at target?
        Quaternion rotTargetFull = Quaternion.LookRotation(tgt.position - src.position);
        Quaternion rotTargetPartial = Quaternion.identity;
        float angX = (xTurn == true ? rotTargetFull.eulerAngles.x : 0);
        float angY = (yTurn == true ? rotTargetFull.eulerAngles.y : 0);
        float angZ = (zTurn == true ? rotTargetFull.eulerAngles.z : 0);        
        rotTargetPartial.eulerAngles = new Vector3(angX, angY, angZ);

        // 3. during this frame, incrementally pointing to new target
        this.transform.rotation = Quaternion.RotateTowards(src.rotation, rotTargetPartial, RotateSpeed * Time.deltaTime);
    }
    private void UpdateTextDisplay()
    {
        Vector3 ang = this.transform.localEulerAngles;
        txtDisplay.text = string.Format("Angles:\nX: {0}\nY: {1}\nZ: {2}", ang.x, ang.y, ang.z);
    }
    public void ToggleOnTurnX()
    {
        if (xTurn == true)
            xTurn = false;
        else
            xTurn = true;
    }
    public void ToggleOnTurnY()
    {
        if (yTurn == true)
            yTurn = false;
        else
            yTurn = true;
    }
    public void ToggleOnTurnZ()
    {
        if (zTurn == true)
            zTurn = false;
        else
            zTurn = true;
    }
}
