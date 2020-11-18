using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceTrackerForEvent : MonoBehaviour
{
    public string key;
    public void Track(int choice) {
        ChoiceTracker.Track(key, choice);
    }
}
