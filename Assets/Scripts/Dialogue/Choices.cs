using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Choice", menuName = "Dialogue/Choice")]
public class Choices : ScriptableObject {
    public int ChoiceID;
    public Choice[] choices;
}
