
using UnityEngine;
using System;
using System.IO; // File operations
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;


/// <summary>
/// The XmmInterface class communicates with the dynamic library plugin.
/// It provides a C# interface to the XmmEngine class which is the one that
/// will be instantiated in a Unity script.
/// </summary>

public class XmmInterface {

  private const string LIBRARY_IMPORT_NAME = 
#if UNITY_IPHONE || UNITY_XBOX360
  "__Internal"
#else
  "XmmEngine"
#endif
  ;
  //==========================================================================//
  //==================== Interface with the native plugin ====================//
  //==========================================================================//

  //========================= Phrases / TrainingSets =========================//

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "addPhraseFromData")]
  public static extern void addPhraseFromData(string label, string[] colNames,
                                               IntPtr phrase, int dimIn,
                                               int dimOut, int phraseSize);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "addPhrase")]
  public static extern void addPhrase(string phrase);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getPhrase")]
  public static extern IntPtr getPhrase(int phraseIndex);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "removePhrase")]
  public static extern void removePhrase(int phraseIndex);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "removePhraseOfLabel")]
  public static extern void removePhrasesOfLabel(string label);  

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getTrainingSetSize")]
  public static extern int getTrainingSetSize();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getTrainingSetNbOfLabels")]
  public static extern int getTrainingSetNbOfLabels();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getTrainingSetLabels")]
  public static extern IntPtr getTrainingSetLabels();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getTrainingSet")]
  public static extern IntPtr getTrainingSet();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getSubTrainingSet")]
  public static extern IntPtr getSubTrainingSet(string label);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "setTrainingSet")]
  public static extern void setTrainingSet(string trainingSet);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "addTrainingSet")]
  public static extern void addTrainingSet(string trainingSet);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "clearTrainingSet")]
  public static extern void clearTrainingSet();

  //=========================== Config getters ===============================//

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getModelType")]
  public static extern int getModelType();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getNbOfModels")]
  public static extern int getNbOfModels();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getInputDimension")]
  public static extern int getInputDimension();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getOutputDimension")]
  public static extern int getOutputDimension();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getBimodal")]
  public static extern int getBimodal();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getGaussians")]
  public static extern int getGaussians();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getRelativeRegularization")]
  public static extern float getRelativeRegularization();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getAbsoluteRegularization")]
  public static extern float getAbsoluteRegularization();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getCovarianceMode")]
  public static extern int getCovarianceMode();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getHierarchical")]
  public static extern int getHierarchical();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getStates")]
  public static extern int getStates();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getTransitionMode")]
  public static extern int getTransitionMode();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getRegressionEstimator")]
  public static extern int getRegressionEstimator();

  //=========================== Config setters ===============================//

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "setModelType")]
  public static extern void setModelType(int modelType);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "setGaussians")]
  public static extern void setGaussians(int gaussians);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "setRelativeRegularization")]
  public static extern void setRelativeRegularization(float relReg);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "setAbsoluteRegularization")]
  public static extern void setAbsoluteRegularization(float absReg);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "setCovarianceMode")]
  public static extern void setCovarianceMode(int covarianceMode);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "setHierarchical")]
  public static extern void setHierarchical(int hierarchical);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "setStates")]
  public static extern void setStates(int states);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "setTransitionMode")]
  public static extern void setTransitionMode(int transitionMode);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "setRegressionEstimator")]
  public static extern void setRegressionEstimator(int regressionEstimator);

  //============================= "Core" methods =============================//

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getLikelihoodWindow")]
  public static extern int getLikelihoodWindow();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "setLikelihoodWindow")]
  public static extern void setLikelihoodWindow(int window);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "train")]
  public static extern void train();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "isTraining")]
  public static extern int isTraining();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "cancelTraining")]
  public static extern void cancelTraining();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getModel")]
  public static extern IntPtr getModel();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "setModel")]
  public static extern void setModel(string model);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "clearModel")]
  public static extern void clearModel();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "filter")]
  public static extern void filter(IntPtr observation, int observationSize);

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getFilteringResults")]
  public static extern IntPtr getFilteringResults();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getLikeliest")]
  public static extern IntPtr getLikeliest();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getLikelihoods")]
  public static extern IntPtr getLikelihoods();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getTimeProgressions")]
  public static extern IntPtr getTimeProgressions();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "getRegression")]
  public static extern IntPtr getRegression();

  [DllImport (LIBRARY_IMPORT_NAME, EntryPoint = "reset")]
  public static extern void reset();
}


/// <summary>
/// The XmmEngine class will be used in Unity scripts to acces XMM's
/// functionalities. It should be instantiated only once
/// (as all the XmmInterface class' methods are static).
/// That means that (for now) one can only use one model instance at a time
/// (wrapped with one training set instance).
/// </summary>

public class XmmEngine {

  //==========================================================================//
  //================= Wrapper methods for the native plugin ==================//
  //==========================================================================//

  public void addPhraseFromData(string label, string[] colNames, float[] phrase,
                        int dimIn, int dimOut) {

    IntPtr unmanagedFArray = Marshal.AllocHGlobal(phrase.Length * sizeof(float));
    Marshal.Copy(phrase, 0, unmanagedFArray, phrase.Length);

    XmmInterface.addPhraseFromData(label, colNames, unmanagedFArray,
                          dimIn, dimOut, phrase.Length / (dimIn + dimOut));

    Marshal.FreeHGlobal(unmanagedFArray);
  }

  public void addPhrase(string phrase) {
    XmmInterface.addPhrase(phrase);
  }

  public string getPhrase(int phraseIndex) {
    IntPtr phraseIntPtr = XmmInterface.getPhrase(phraseIndex);
    return Marshal.PtrToStringAnsi(phraseIntPtr);
  }

  public void removePhrase(int phraseIndex) {
    XmmInterface.removePhrase(phraseIndex);
  }

  public void removePhrasesOfLabel(string label) {
    XmmInterface.removePhrasesOfLabel(label);
  }

  public int getTrainingSetSize() {
    return XmmInterface.getTrainingSetSize();
  }

  public int getTrainingSetNbOfLabels() {
    return XmmInterface.getTrainingSetNbOfLabels();
  }

  public string[] getTrainingSetLabels() {
    int nLabels = XmmInterface.getTrainingSetNbOfLabels();
    string[] labels = new string[nLabels];
    IntPtr stringArrayIntPtr = XmmInterface.getTrainingSetLabels();

    for (int i = 0; i < nLabels; i++) {
      IntPtr currentIntPtr = Marshal.ReadIntPtr(stringArrayIntPtr, i * IntPtr.Size); 
      labels[i] = Marshal.PtrToStringAnsi(currentIntPtr);
    }

    return labels;
  }

  public string getTrainingSet() {
    return Marshal.PtrToStringAnsi(XmmInterface.getTrainingSet());
  }

  public string getSubTrainingSet(string label) {
    return Marshal.PtrToStringAnsi(XmmInterface.getSubTrainingSet(label));
  }

  public void setTrainingSet(string trainingSet) {
    XmmInterface.setTrainingSet(trainingSet);
  }

  public void addTrainingSet(string trainingSet) {
    XmmInterface.addTrainingSet(trainingSet);
  }

  public void clearTrainingSet() {
    XmmInterface.clearTrainingSet();
  }

  //==========================================================================//

  public int getModelType() {
    return XmmInterface.getModelType();
  }

  public int getNbOfModels() {
    return XmmInterface.getNbOfModels();
  }

  public int getInputDimension() {
    return XmmInterface.getInputDimension();
  }

  public int getOutputDimension() {
    return XmmInterface.getOutputDimension();
  }

  public bool getBimodal() {
    return XmmInterface.getBimodal() != 0;
  }

  public int getGaussians() {
    return XmmInterface.getGaussians();
  }

  public float getRelativeRegularization() {
    return XmmInterface.getRelativeRegularization();
  }

  public float getAbsoluteRegularization() {
    return XmmInterface.getAbsoluteRegularization();
  }

  public int getCovarianceMode() {
    return XmmInterface.getCovarianceMode();
  }

  public bool getHierarchical() {
    return XmmInterface.getHierarchical() != 0;
  }

  public int getStates() {
    return XmmInterface.getStates();
  }

  public int getTransitionMode() {
    return XmmInterface.getTransitionMode();
  }

  public int getRegressionEstimator() {
    return XmmInterface.getRegressionEstimator();
  }

  //==========================================================================//

  public void setModelType(int modelType) {
    XmmInterface.setModelType(modelType);
  }

  public void setGaussians(int gaussians) {
    XmmInterface.setGaussians(gaussians);
  }

  public void setRelativeRegularization(float relReg) {
    XmmInterface.setRelativeRegularization(relReg);
  }

  public void setAbsoluteRegularization(float absReg) {
    XmmInterface.setAbsoluteRegularization(absReg);
  }

  public void setCovarianceMode(int covarianceMode) {
    XmmInterface.setCovarianceMode(covarianceMode);
  }

  public void setHierarchical(bool hierarchical) {
    XmmInterface.setHierarchical(hierarchical ? 1 : 0);
  }

  public void setStates(int states) {
    XmmInterface.setStates(states);
  }

  public void setTransitionMode(int transitionMode) {
    XmmInterface.setTransitionMode(transitionMode);
  }

  public void setRegressionEstimator(int regressionEstimator) {
    XmmInterface.setRegressionEstimator(regressionEstimator);
  }

  //==========================================================================//

  public int getLikelihoodWindow() {
    return XmmInterface.getLikelihoodWindow();
  }

  public void setLikelihoodWindow(int window) {
    XmmInterface.setLikelihoodWindow(window);
  }

  public void train() {
    XmmInterface.train();
  }

  public bool isTraining() {
    return XmmInterface.isTraining() != 0;
  }

  public void cancelTraining() {
    XmmInterface.cancelTraining();
  }

  public string getModel() {
    return Marshal.PtrToStringAnsi(XmmInterface.getModel());
  }

  public void setModel(string model) {
    XmmInterface.setModel(model);
  }

  public void clearModel() {
    XmmInterface.clearModel();
  }

  public void filter(float[] observation) {
    IntPtr intPtrFArray = Marshal.AllocHGlobal(observation.Length * sizeof(float));
    Marshal.Copy(observation, 0, intPtrFArray, observation.Length);

    XmmInterface.filter(intPtrFArray, observation.Length);

    Marshal.FreeHGlobal(intPtrFArray);
  }

  public string getFilteringResults() {
    return Marshal.PtrToStringAnsi(XmmInterface.getFilteringResults());
  }

  public string getLikeliest() {
    return Marshal.PtrToStringAnsi(XmmInterface.getLikeliest());
  }

  public float[] getLikelihoods() {
    int nModels = XmmInterface.getNbOfModels();
    float[] likelihoods = new float[nModels];
    IntPtr intPtrLikelihoods = XmmInterface.getLikelihoods();
    Marshal.Copy(intPtrLikelihoods, likelihoods, 0, nModels);
    return likelihoods;
  }

  public float[] getTimeProgressions() {
    int nModels = XmmInterface.getNbOfModels();
    float[] timeProgressions = new float[nModels];
    IntPtr intPtrTimeProgressions = XmmInterface.getTimeProgressions();
    Marshal.Copy(intPtrTimeProgressions, timeProgressions, 0, nModels);
    return timeProgressions;
  }

  public float[] getRegression() {
    int nModels = XmmInterface.getNbOfModels();
    float[] outputValues = new float[nModels];
    IntPtr intPtrOutputValues = XmmInterface.getLikelihoods();
    Marshal.Copy(intPtrOutputValues, outputValues, 0, nModels);
    return outputValues;
  }

  public void reset() {
    XmmInterface.reset();
  }
}

//============================================================================//

// public class Xmm {

//   private int index = -1;

//   Xmm(string modelType) {
//     index = createXmmInstance(modelType);
//   }

//   ~Xmm() {
//     deleteXmmInstance(index);
//   }

// }
