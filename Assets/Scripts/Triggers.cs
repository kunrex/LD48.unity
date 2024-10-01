using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    [SerializeField] private int playerMaskInt;
    internal enum TriggerType
    {
        crabCheck, piranhaCheck, crabbieCheck, dodgeCheck, piranhasDone, dodgeDone
    }
    [SerializeField] private TriggerType triggerType;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == playerMaskInt && !triggered)
        {
            switch (triggerType)
            {
                case TriggerType.crabCheck:
                    Game_Event_Manager.instance.BossCheck();
                    break;
                case TriggerType.piranhaCheck:
                    Game_Event_Manager.instance.PiranhaCheck();
                    break;
                case TriggerType.crabbieCheck:
                    Game_Event_Manager.instance.CrabbieCheck();
                    break;
                case TriggerType.piranhasDone:
                    Game_Event_Manager.instance.PiranhaDone();
                    break;
                case TriggerType.dodgeCheck:
                    Game_Event_Manager.instance.DodgsCheck();
                    break;
                case TriggerType.dodgeDone:
                    Game_Event_Manager.instance.DodgsDone();
                    break;
            }
            triggered = true;
        }
    }
}
