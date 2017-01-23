using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

  InputProcessingChain proc = new InputProcessingChain(128, 16);
  XmmTrainingSet trainingSet = new XmmTrainingSet();
  XmmModel gmm = new XmmModel("gmm");

  bool recording = false;
  bool newPhrase = false;
  float[] desc = new float[3];
  string currentLabel = "STILL";
  string likeliest = "UNKNOWN";
  List<float> phraseList = new List<float>();
  string[] colNames = new string[] { "magnitude", "frequency", "periodicity" };

  // REC Button's ColorBlock
  ColorBlock recButtonColorBlock = new ColorBlock();
  Text displayText;

  //============================= Mono Behaviour =============================//

	// Use this for initialization
	void Start() {
    gmm.SetLikelihoodWindow(20);

    if (SystemInfo.supportsGyroscope) {
      Input.gyro.enabled = true;
    } else  {
      likeliest = "NO GYRO ON THIS DEVICE";
    }

    // Set REC Button's ColorBlock constant values
    recButtonColorBlock.colorMultiplier = 1f;
    recButtonColorBlock.normalColor = Color.white;
    recButtonColorBlock.pressedColor = Color.white;

    // Get displayText Component
    displayText = GameObject.Find("DisplayText").GetComponent<Text>();
    displayText.text = likeliest;
	}
	
	// Update is called once per frame
	void Update() {
    if (!SystemInfo.supportsGyroscope) return;

    gmm.Filter(desc);
    likeliest = gmm.GetLikeliest();
    Debug.Log("likeliest : " + likeliest);
    displayText.text = likeliest;
	}

  void FixedUpdate() {
    if (!SystemInfo.supportsGyroscope) return;

    float rr = Mathf.Sqrt(Input.gyro.rotationRate.x * Input.gyro.rotationRate.x 
              + Input.gyro.rotationRate.y * Input.gyro.rotationRate.y
              + Input.gyro.rotationRate.z * Input.gyro.rotationRate.z);

    proc.feed(rr);

    if(proc.hasNewFrame()) {
      desc = proc.getLastFrame();

      if(recording) {
        foreach(float f in desc) {
          phraseList.Add(f);
        }
      }
    }
  }

  void OnGUI() {
    // GUI.Label(new Rect(10,10,200,20), "Aha ! " + recording);
  }

  //=============================== UI Events ================================//

  public void OnRecButtonEvent(Button b) {
    recording = !recording;

    if (recording) {
      // START REC
      recButtonColorBlock.highlightedColor = Color.red;
      phraseList = new List<float>();
      newPhrase = false;
    } else {
      // STOP REC
      recButtonColorBlock.highlightedColor = Color.white;
      if (phraseList.Count > 0) {
        newPhrase = true;
      }
    }
    
    b.colors = recButtonColorBlock;
  }

  public void OnLabelDropDownEvent(Dropdown dd) {
    Debug.Log(dd.captionText.text);
    currentLabel = dd.captionText.text;
  }

  public void OnAddButtonEvent(Button b) {
    if (!SystemInfo.supportsGyroscope) return;

    if (newPhrase) {
      float[] phrase = phraseList.ToArray();
      trainingSet.AddPhraseFromData(currentLabel, colNames, phrase, 3, 0);
      gmm.Train(trainingSet);
      gmm.Reset();
    }
  }

  public void OnClearLabelButtonEvent(Button b) {
    if (!SystemInfo.supportsGyroscope) return;

    trainingSet.RemovePhrasesOfLabel(currentLabel);
    gmm.Train(trainingSet);
    gmm.Reset();
  }

  public void OnClearAllButtonEvent(Button b) {
    if (!SystemInfo.supportsGyroscope) return;

    trainingSet.Clear();
    gmm.Train(trainingSet);
    gmm.Reset();
  }
}
