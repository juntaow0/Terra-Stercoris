﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// only works within a game session
public static class ChoiceTracker
{
    private static Dictionary<string,int> choices = new Dictionary<string, int>();

    public static void Track(string key, int choice) {
        if (choices.ContainsKey(key)) {
            choices[key] = choice;
        } else {
            choices.Add(key, choice);
        }
    }

    public static void Reset() {
        choices.Clear();
    }

    public static int GetChoiceNumber(string key) {
        return choices[key];
    }
}
