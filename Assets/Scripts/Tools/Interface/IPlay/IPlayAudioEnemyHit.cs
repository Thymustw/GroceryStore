using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayAudioEnemyHit : IPlayAudioBehavior
{
    public string AudioPath()
    {
        return "Audio/Enemy_Hit";
    }
}
