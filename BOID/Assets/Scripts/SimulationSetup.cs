using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimulationSetup : MonoBehaviour
{
    public GameObject boidPrefab;

    public TMP_InputField separationTurningSpeed;
    public TMP_InputField alignmentTurningSpeed;
    public TMP_InputField cohesionTurningSpeed;
    public TMP_InputField separationVisionDistance;
    public TMP_InputField alignmentVisionDistance;
    public TMP_InputField cohesionVisionDistance;
    public Toggle separationEnabled;
    public Toggle alignmentEnabled;
    public Toggle cohesionEnabled;
    public TMP_InputField boidSpeed;
    public TMP_InputField boidAmount;

    private BoidController boidController;

    // Start is called before the first frame update
    void Start()
    {
        boidController = boidPrefab.GetComponent<BoidController>();

        alignmentEnabled.isOn = boidController.activateAlignment;
        alignmentVisionDistance.text = boidController.alignmentVisionZone.ToString();
        alignmentTurningSpeed.text = boidController.alignmentTurningSpeed.ToString();

        separationEnabled.isOn = boidController.activateSeparation;
        separationVisionDistance.text = boidController.separationVisionZone.ToString();
        separationTurningSpeed.text = boidController.separationTurningSpeed.ToString();

        cohesionEnabled.isOn = boidController.activateCohesion;
        cohesionVisionDistance.text = boidController.cohesionVisionZone.ToString();
        cohesionTurningSpeed.text = boidController.cohesionTurningSpeed.ToString();

        boidSpeed.text = boidController.movementSpeed.ToString();
        boidAmount.text = PlayerPrefs.GetInt("Amount", 100).ToString();
    }

    public void OnSimulationStart()
    {
        boidController.activateAlignment = alignmentEnabled.isOn;
        boidController.alignmentVisionZone = float.Parse(alignmentVisionDistance.text);
        boidController.alignmentTurningSpeed = float.Parse(alignmentTurningSpeed.text);

        boidController.activateSeparation = separationEnabled.isOn;
        boidController.separationVisionZone = float.Parse(separationVisionDistance.text);
        boidController.separationTurningSpeed = float.Parse(separationTurningSpeed.text);

        boidController.activateCohesion = cohesionEnabled.isOn;
        boidController.cohesionVisionZone = float.Parse(cohesionVisionDistance.text);
        boidController.cohesionTurningSpeed = float.Parse(cohesionTurningSpeed.text);

        boidController.movementSpeed = float.Parse(boidSpeed.text);
        PlayerPrefs.SetInt("Amount", int.Parse(boidAmount.text));

        SceneManager.LoadScene("SimulationScene");
    }
}
