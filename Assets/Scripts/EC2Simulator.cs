using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simulates Amazon EC2 service for hands-on learning
/// </summary>
public class EC2Simulator : AWSServiceSimulator
{
    private Dictionary<string, EC2Instance> instances = new Dictionary<string, EC2Instance>();
    private string currentInstanceId = "";

    private void Start()
    {
        serviceName = "Amazon EC2";
        serviceDescription = "Learn to launch and configure EC2 instances";
        relatedDomain = ExamDomain.Technology;

        InitializeLab();
    }

    private void InitializeLab()
    {
        labSteps.Clear();

        // Step 1: Choose AMI
        var step1 = new LabStep(
            "choose_ami",
            "Select the Amazon Linux 2 AMI (Amazon Machine Image)",
            LabStepType.MultipleChoice,
            "An AMI is a template that contains the software configuration (OS, application server, applications) required to launch your instance"
        );
        step1.expectedValues.Add("ami", "Amazon Linux 2");
        step1.experienceReward = 75;
        labSteps.Add(step1);

        // Step 2: Choose instance type
        var step2 = new LabStep(
            "choose_instance_type",
            "Choose the t3.micro instance type (part of the free tier)",
            LabStepType.Dropdown,
            "Instance types comprise varying combinations of CPU, memory, storage, and networking capacity"
        );
        step2.expectedValues.Add("instance_type", "t3.micro");
        step2.experienceReward = 100;
        labSteps.Add(step2);

        // Step 3: Configure instance
        var step3 = new LabStep(
            "instance_count",
            "Set the number of instances to launch: 1",
            LabStepType.TextInput,
            "You can launch multiple instances from a single AMI"
        );
        step3.expectedValues.Add("count", "1");
        step3.experienceReward = 50;
        labSteps.Add(step3);

        // Step 4: Add storage
        var step4 = new LabStep(
            "add_storage",
            "Keep the default 8 GB General Purpose SSD (gp3) volume",
            LabStepType.Configuration,
            "EBS volumes persist independently from the instance. Choose volume type based on performance needs."
        );
        step4.expectedValues.Add("volume_size", "8");
        step4.expectedValues.Add("volume_type", "gp3");
        step4.experienceReward = 75;
        labSteps.Add(step4);

        // Step 5: Add tags
        var step5 = new LabStep(
            "add_tags",
            "Add a tag with Key='Name' and Value='WebServer'",
            LabStepType.TextInput,
            "Tags help you organize and identify your AWS resources"
        );
        step5.expectedValues.Add("tag_key", "Name");
        step5.expectedValues.Add("tag_value", "WebServer");
        step5.experienceReward = 50;
        labSteps.Add(step5);

        // Step 6: Configure security group
        var step6 = new LabStep(
            "security_group",
            "Create a security group that allows HTTP (port 80) and SSH (port 22) traffic",
            LabStepType.Configuration,
            "Security groups act as virtual firewalls controlling inbound and outbound traffic"
        );
        step6.expectedValues.Add("port_http", "80");
        step6.expectedValues.Add("port_ssh", "22");
        step6.experienceReward = 150;
        labSteps.Add(step6);

        // Step 7: Select key pair
        var step7 = new LabStep(
            "key_pair",
            "Select an existing key pair or create a new one named 'my-key-pair'",
            LabStepType.Dropdown,
            "Key pairs are used to securely connect to your instance via SSH"
        );
        step7.expectedValues.Add("key_pair", "my-key-pair");
        step7.experienceReward = 75;
        labSteps.Add(step7);

        // Step 8: Review and launch
        var step8 = new LabStep(
            "launch_instance",
            "Review your configuration and launch the instance",
            LabStepType.ClickInteraction,
            "Review all settings before launching to avoid unexpected charges"
        );
        step8.experienceReward = 125;
        labSteps.Add(step8);

        // Step 9: View instance status
        var step9 = new LabStep(
            "check_status",
            "Wait for the instance to reach 'running' state",
            LabStepType.WaitForAction,
            "Instances typically take 1-2 minutes to launch and become available"
        );
        step9.experienceReward = 50;
        labSteps.Add(step9);
    }

    protected override void ShowStepUI(LabStep step)
    {
        Debug.Log($"[EC2 Simulator] Showing step UI: {step.instruction}");
        Debug.Log($"[EC2 Simulator] Hint: {step.hint}");
    }

    protected override bool ValidateStep(LabStep step, Dictionary<string, object> userInput)
    {
        bool isValid = true;

        foreach (var expectedValue in step.expectedValues)
        {
            string key = expectedValue.Key;
            string expectedVal = expectedValue.Value;

            if (!userInput.ContainsKey(key))
            {
                Debug.Log($"[EC2 Simulator] Missing input: {key}");
                isValid = false;
                break;
            }

            string userVal = userInput[key].ToString().Trim();

            if (!string.Equals(userVal, expectedVal, System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log($"[EC2 Simulator] Invalid {key}. Expected: {expectedVal}, Got: {userVal}");
                isValid = false;
            }
        }

        if (isValid)
        {
            foreach (var input in userInput)
            {
                SetLabState(input.Key, input.Value);
            }

            // Special handling for instance launch
            if (step.stepId == "launch_instance")
            {
                LaunchInstance();
            }
        }

        return isValid;
    }

    private void LaunchInstance()
    {
        currentInstanceId = "i-" + System.Guid.NewGuid().ToString().Substring(0, 8);

        var instance = new EC2Instance
        {
            instanceId = currentInstanceId,
            instanceType = GetLabState("instance_type")?.ToString() ?? "t3.micro",
            ami = GetLabState("ami")?.ToString() ?? "Amazon Linux 2",
            state = "pending",
            launchTime = System.DateTime.Now.ToString(),
            publicIp = GeneratePublicIP(),
            privateIp = GeneratePrivateIP(),
            availabilityZone = "us-east-1a",
            volumeSize = int.Parse(GetLabState("volume_size")?.ToString() ?? "8"),
            volumeType = GetLabState("volume_type")?.ToString() ?? "gp3",
            keyPair = GetLabState("key_pair")?.ToString() ?? "my-key-pair"
        };

        // Add tags
        if (HasLabState("tag_key") && HasLabState("tag_value"))
        {
            instance.tags.Add(GetLabState("tag_key").ToString(), GetLabState("tag_value").ToString());
        }

        // Add security group rules
        if (HasLabState("port_http"))
        {
            instance.securityGroupRules.Add(new SecurityGroupRule
            {
                protocol = "tcp",
                port = 80,
                source = "0.0.0.0/0",
                description = "HTTP access"
            });
        }
        if (HasLabState("port_ssh"))
        {
            instance.securityGroupRules.Add(new SecurityGroupRule
            {
                protocol = "tcp",
                port = 22,
                source = "0.0.0.0/0",
                description = "SSH access"
            });
        }

        instances.Add(currentInstanceId, instance);
        Debug.Log($"[EC2 Simulator] Instance launched: {currentInstanceId}");

        // Simulate instance starting (would be async in real implementation)
        Invoke("SetInstanceRunning", 2f);
    }

    private void SetInstanceRunning()
    {
        if (instances.ContainsKey(currentInstanceId))
        {
            instances[currentInstanceId].state = "running";
            Debug.Log($"[EC2 Simulator] Instance {currentInstanceId} is now running");

            // Auto-complete the wait step
            if (currentStepIndex < labSteps.Count && labSteps[currentStepIndex].stepId == "check_status")
            {
                var userInput = new Dictionary<string, object>
                {
                    { "status", "running" }
                };
                ValidateCurrentStep(userInput);
            }
        }
    }

    private string GeneratePublicIP()
    {
        System.Random rnd = new System.Random();
        return $"{rnd.Next(1, 255)}.{rnd.Next(0, 255)}.{rnd.Next(0, 255)}.{rnd.Next(1, 255)}";
    }

    private string GeneratePrivateIP()
    {
        System.Random rnd = new System.Random();
        return $"10.0.{rnd.Next(0, 255)}.{rnd.Next(1, 255)}";
    }

    protected override void UpdateQuestProgress()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.CompleteObjective("TECH_COMPUTE_001", "configure_instance");
        }
    }

    // Public methods for UI
    public List<string> GetAvailableAMIs()
    {
        return new List<string>
        {
            "Amazon Linux 2",
            "Ubuntu Server 20.04",
            "Windows Server 2019",
            "Red Hat Enterprise Linux 8"
        };
    }

    public List<string> GetInstanceTypes()
    {
        return new List<string>
        {
            "t3.micro",
            "t3.small",
            "t3.medium",
            "m5.large",
            "c5.large",
            "r5.large"
        };
    }

    public List<string> GetVolumeTypes()
    {
        return new List<string>
        {
            "gp3",
            "gp2",
            "io2",
            "st1",
            "sc1"
        };
    }

    public Dictionary<string, EC2Instance> GetInstances()
    {
        return new Dictionary<string, EC2Instance>(instances);
    }
}

[System.Serializable]
public class EC2Instance
{
    public string instanceId;
    public string instanceType;
    public string ami;
    public string state;
    public string launchTime;
    public string publicIp;
    public string privateIp;
    public string availabilityZone;
    public int volumeSize;
    public string volumeType;
    public string keyPair;
    public Dictionary<string, string> tags = new Dictionary<string, string>();
    public List<SecurityGroupRule> securityGroupRules = new List<SecurityGroupRule>();
}

[System.Serializable]
public class SecurityGroupRule
{
    public string protocol;
    public int port;
    public string source;
    public string description;
}
