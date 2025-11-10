using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all AWS service simulators
/// Provides common functionality for hands-on AWS service exercises
/// </summary>
public abstract class AWSServiceSimulator : MonoBehaviour
{
    [Header("Simulator Configuration")]
    public string serviceName;
    public string serviceDescription;
    public ExamDomain relatedDomain;

    [Header("Lab Configuration")]
    public List<LabStep> labSteps = new List<LabStep>();
    protected int currentStepIndex = 0;

    // Events
    public event Action<LabStep> OnStepCompleted;
    public event Action<string> OnLabCompleted;
    public event Action<string> OnValidationFailed;

    protected bool isLabActive = false;
    protected Dictionary<string, object> labState = new Dictionary<string, object>();

    public virtual void StartLab()
    {
        isLabActive = true;
        currentStepIndex = 0;
        labState.Clear();

        Debug.Log($"[{serviceName}] Lab started: {serviceDescription}");

        if (labSteps.Count > 0)
        {
            ShowCurrentStep();
        }
    }

    public virtual void ShowCurrentStep()
    {
        if (currentStepIndex < labSteps.Count)
        {
            var step = labSteps[currentStepIndex];
            Debug.Log($"[{serviceName}] Step {currentStepIndex + 1}/{labSteps.Count}: {step.instruction}");

            // Show UI for current step
            ShowStepUI(step);
        }
    }

    protected abstract void ShowStepUI(LabStep step);

    public virtual bool ValidateCurrentStep(Dictionary<string, object> userInput)
    {
        if (currentStepIndex >= labSteps.Count)
            return false;

        var step = labSteps[currentStepIndex];
        bool isValid = ValidateStep(step, userInput);

        if (isValid)
        {
            CompleteCurrentStep();
        }
        else
        {
            OnValidationFailed?.Invoke(step.validationFailedMessage);
        }

        return isValid;
    }

    protected abstract bool ValidateStep(LabStep step, Dictionary<string, object> userInput);

    protected virtual void CompleteCurrentStep()
    {
        if (currentStepIndex < labSteps.Count)
        {
            var step = labSteps[currentStepIndex];
            step.isCompleted = true;

            OnStepCompleted?.Invoke(step);
            Debug.Log($"[{serviceName}] Step completed: {step.instruction}");

            // Award experience for completing step
            if (PlayerProgress.Instance != null)
            {
                PlayerProgress.Instance.AddExperience(step.experienceReward);
            }

            currentStepIndex++;

            // Check if lab is complete
            if (currentStepIndex >= labSteps.Count)
            {
                CompleteLab();
            }
            else
            {
                ShowCurrentStep();
            }
        }
    }

    protected virtual void CompleteLab()
    {
        isLabActive = false;
        string completionMessage = $"Congratulations! You've completed the {serviceName} lab.";

        OnLabCompleted?.Invoke(completionMessage);
        Debug.Log($"[{serviceName}] Lab completed!");

        // Award completion badge
        if (PlayerProgress.Instance != null)
        {
            string badgeName = $"{serviceName} Hands-On Expert";
            PlayerProgress.Instance.AwardBadge(badgeName);
        }

        // Update quest objectives if connected to a quest
        UpdateQuestProgress();
    }

    protected virtual void UpdateQuestProgress()
    {
        // Override in specific simulators to update quest objectives
    }

    public float GetLabProgress()
    {
        if (labSteps.Count == 0) return 0f;
        return (float)currentStepIndex / labSteps.Count * 100f;
    }

    public bool IsLabActive()
    {
        return isLabActive;
    }

    public LabStep GetCurrentStep()
    {
        if (currentStepIndex < labSteps.Count)
            return labSteps[currentStepIndex];
        return null;
    }

    protected void SetLabState(string key, object value)
    {
        labState[key] = value;
    }

    protected object GetLabState(string key)
    {
        return labState.ContainsKey(key) ? labState[key] : null;
    }

    protected bool HasLabState(string key)
    {
        return labState.ContainsKey(key);
    }
}

/// <summary>
/// Represents a single step in a hands-on lab
/// </summary>
[Serializable]
public class LabStep
{
    public string stepId;
    public string instruction;
    public string hint;
    public LabStepType stepType;
    public bool isCompleted;
    public int experienceReward = 50;
    public string validationFailedMessage = "That's not quite right. Check the hint and try again.";

    // Validation criteria (override in specific implementations)
    public Dictionary<string, string> expectedValues = new Dictionary<string, string>();

    public LabStep(string id, string instruction, LabStepType type, string hint = "")
    {
        this.stepId = id;
        this.instruction = instruction;
        this.stepType = type;
        this.hint = hint;
        this.isCompleted = false;
    }
}

public enum LabStepType
{
    TextInput,          // User enters text (names, descriptions, etc.)
    Dropdown,           // Select from options
    MultipleChoice,     // Choose from multiple options
    Configuration,      // Configure multiple settings
    CodeEditor,         // Write/edit code or policies
    DragAndDrop,        // Drag items to correct places
    ClickInteraction,   // Click on UI elements
    WaitForAction       // Wait for automated process
}

/// <summary>
/// Manages all AWS service simulators
/// </summary>
public class AWSSimulatorManager : MonoBehaviour
{
    public static AWSSimulatorManager Instance { get; private set; }

    [Header("Service Simulators")]
    public S3Simulator s3Simulator;
    public EC2Simulator ec2Simulator;
    public LambdaSimulator lambdaSimulator;
    public IAMSimulator iamSimulator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSimulators();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSimulators()
    {
        // Simulators will be attached as components or created dynamically
        if (s3Simulator == null)
            s3Simulator = gameObject.AddComponent<S3Simulator>();

        if (ec2Simulator == null)
            ec2Simulator = gameObject.AddComponent<EC2Simulator>();

        if (lambdaSimulator == null)
            lambdaSimulator = gameObject.AddComponent<LambdaSimulator>();

        if (iamSimulator == null)
            iamSimulator = gameObject.AddComponent<IAMSimulator>();

        Debug.Log("[AWSSimulatorManager] All simulators initialized");
    }

    public AWSServiceSimulator GetSimulator(string serviceName)
    {
        switch (serviceName.ToLower())
        {
            case "s3":
                return s3Simulator;
            case "ec2":
                return ec2Simulator;
            case "lambda":
                return lambdaSimulator;
            case "iam":
                return iamSimulator;
            default:
                Debug.LogWarning($"[AWSSimulatorManager] Simulator not found: {serviceName}");
                return null;
        }
    }

    public void StartSimulator(string serviceName)
    {
        var simulator = GetSimulator(serviceName);
        if (simulator != null)
        {
            simulator.StartLab();
        }
    }
}
