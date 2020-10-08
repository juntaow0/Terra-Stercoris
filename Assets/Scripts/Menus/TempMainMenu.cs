using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMainMenu : MonoBehaviour
{
    public GameObject credit;

    private void Start() {
        credit.SetActive(false);
    }

    public void showCredit() {
        credit.SetActive(true);
    }
}
