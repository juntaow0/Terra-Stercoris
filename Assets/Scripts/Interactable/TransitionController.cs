﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour {

    public void LoadScene(string scene) {
        TransitionManager.instance.LoadScene(scene);
    }
}