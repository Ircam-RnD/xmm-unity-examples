using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class XmmEngineTest : MonoBehaviour {

  float mouseDistanceThreshold = 2;
  float[] prevMouseCoords = new float[2];
  float[] mouseCoords = new float[2];
  float[] mouseDelta = new float[2];
  List<float> phrase;
  bool recordEnabled = false;
  bool record = false;
  bool filter = false;
  string label = "";
  string likeliest = "";
  float[] likelihoods = new float[0];
  private XmmEngine xmm = new XmmEngine();

	// Use this for initialization
	void Start () {
    xmm.clearTrainingSet();
    xmm.clearModel();
    xmm.setModelType(1); // HMM
    xmm.setStates(10);
    xmm.setLikelihoodWindow(5);
    xmm.setRelativeRegularization(0.01f);
    xmm.setGaussians(1);
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.anyKey) {
      if (!String.IsNullOrEmpty(Input.inputString)) {
        char c = Input.inputString[0];
        label = (c >= 'a' && c <= 'z' && c != 'r') ? c.ToString() : label;

        if (c == 'r' && !Input.GetMouseButton(0)) {
          recordEnabled = !recordEnabled;
        }
      }
    }

		if (Input.GetMouseButtonDown(0)) {
      if (recordEnabled) {
        startRecording();
      } else {
        startFiltering();
      }
    }

    if (Input.GetMouseButtonUp(0)) {
      if (recordEnabled) {
        stopRecording();
      } else {
        stopFiltering();
      }
    }

    if (record || filter) {
      mouseCoords[0] = Input.mousePosition[0];
      mouseCoords[1] = Input.mousePosition[1];

      if (distance(mouseCoords, prevMouseCoords) > mouseDistanceThreshold) {
        prevMouseCoords[0] = mouseCoords[0];
        prevMouseCoords[1] = mouseCoords[1];

        if (record) {
          phrase.Add(mouseDelta[0]);
          phrase.Add(mouseDelta[1]);
        } else { //filter
          xmm.filter(mouseDelta);
          likeliest = xmm.getLikeliest();
          likelihoods = xmm.getLikelihoods();
        }
      }
    }    
	}

  void OnGUI() {
    GUI.Label(new Rect(10, 10, 200, 50),
              "recording " + (recordEnabled ? "enabled" : "disabled"));    
    GUI.Label(new Rect(10, 30, 200, 50),
              "current label : " + label);
    GUI.Label(new Rect(10, 50, 200, 50), "nb of models : " + xmm.getNbOfModels());
    GUI.Label(new Rect(10, 70, 200, 50), "nb of phrases : " + xmm.getTrainingSetSize());
    GUI.Label(new Rect(10, 90, 200, 50), "likeliest : " + likeliest);

    string l = "";
    for (int i = 0; i < likelihoods.Length; ++i) {
      l += likelihoods[i] + " ";
    }
    GUI.Label(new Rect(10, 110, 200, 50), "likelihoods : " + l);
  }

  private void startRecording() {
    record = true;
    phrase = new List<float>();
    prevMouseCoords[0] = Input.mousePosition[0];
    prevMouseCoords[1] = Input.mousePosition[1];
  }

  private void stopRecording() {
    record = false;
    float[] p = phrase.ToArray();
    string[] colNames = { "mouseX", "mouseY" };
    xmm.addPhraseFromData(label, colNames, p, 2, 0);
    xmm.train();
    xmm.reset();
  }

  private void startFiltering() {
    filter = true;
    prevMouseCoords[0] = Input.mousePosition[0];
    prevMouseCoords[1] = Input.mousePosition[1];
  }

  private void stopFiltering() {
    filter = false;
    xmm.reset();
  }

  private float distance(float[] newPos, float[] prevPos) {
    mouseDelta[0] = newPos[0] - prevPos[0];
    mouseDelta[1] = newPos[1] - prevPos[1];

    return (float)Math.Sqrt(mouseDelta[0] * mouseDelta[0] +
                            mouseDelta[1] * mouseDelta[1]);
  }

  private void logLabels() {
    string[] labels = xmm.getTrainingSetLabels();
    Debug.Log("nb of labels : " + labels.Length);
    for (int i = 0; i < labels.Length; ++i) {
      Debug.Log("label " + i + " : " + labels[i]);
    }
  }
}
