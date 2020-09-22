using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StandingPicture", menuName = "ScriptableObjects/Dialogue/StandingPicture")]
public class StandingPicture : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
}
