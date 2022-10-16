using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using Logger;
using UnityEngine;


public class Entry : MonoBehaviour {

    private void Awake() {
        DontDestroyOnLoad(this);
        Game.Logger = new LoggerHelper();
        Game.Start();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { Game.Update(Time.deltaTime); }

    private void FixedUpdate() { Game.FixedUpdate(Time.fixedDeltaTime); }

    private void LateUpdate() { Game.LateUpdate(Time.deltaTime); }

}