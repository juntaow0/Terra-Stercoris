﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public void Play(string cue) {
        AudioManager.instance.Play(cue);
    } 
}
