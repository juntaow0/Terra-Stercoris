using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISiphonable {

    void Siphon(int amount);

    Transform transform {get;}

    bool IsSiphonable {get; set;}
}
