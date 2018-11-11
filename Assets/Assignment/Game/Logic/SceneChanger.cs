using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour, ISceneLoader {

    public IEnumerator Load() { return null; }

    public void OnSceneReady() { }

    public float LoadProgress { get { return 0.0f; } }

    public string LoadPhase { get { return ""; } }



}
