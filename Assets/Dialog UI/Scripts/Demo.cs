using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.Dialogs;

public class Demo : MonoBehaviour {
    void Start() {
        // Show Dialog
        DialogUI.Instance
        .SetTitle("Target Hit")
        .SetMessage("Well Done!")
        .Show();
    }
}
