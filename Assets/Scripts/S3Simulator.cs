using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simulates Amazon S3 service for hands-on learning
/// </summary>
public class S3Simulator : AWSServiceSimulator
{
    private Dictionary<string, S3Bucket> buckets = new Dictionary<string, S3Bucket>();
    private string currentBucketName = "";

    private void Start()
    {
        serviceName = "Amazon S3";
        serviceDescription = "Learn to create and configure S3 buckets for object storage";
        relatedDomain = ExamDomain.Technology;

        InitializeLab();
    }

    private void InitializeLab()
    {
        labSteps.Clear();

        // Step 1: Create a bucket
        var step1 = new LabStep(
            "create_bucket",
            "Create an S3 bucket named 'my-first-bucket-123' (bucket names must be globally unique)",
            LabStepType.TextInput,
            "Bucket names must be lowercase, unique globally, and follow DNS naming conventions"
        );
        step1.expectedValues.Add("bucket_name", "my-first-bucket-123");
        step1.experienceReward = 100;
        labSteps.Add(step1);

        // Step 2: Select a region
        var step2 = new LabStep(
            "select_region",
            "Select the AWS Region 'us-east-1' for your bucket",
            LabStepType.Dropdown,
            "Choosing a region close to your users reduces latency"
        );
        step2.expectedValues.Add("region", "us-east-1");
        step2.experienceReward = 50;
        labSteps.Add(step2);

        // Step 3: Configure versioning
        var step3 = new LabStep(
            "enable_versioning",
            "Enable versioning on your bucket to protect against accidental deletions",
            LabStepType.ClickInteraction,
            "Versioning keeps multiple versions of an object in the same bucket"
        );
        step3.expectedValues.Add("versioning", "enabled");
        step3.experienceReward = 75;
        labSteps.Add(step3);

        // Step 4: Configure encryption
        var step4 = new LabStep(
            "enable_encryption",
            "Enable server-side encryption with SSE-S3 (AES-256)",
            LabStepType.MultipleChoice,
            "Server-side encryption protects data at rest"
        );
        step4.expectedValues.Add("encryption", "SSE-S3");
        step4.experienceReward = 75;
        labSteps.Add(step4);

        // Step 5: Set bucket policy
        var step5 = new LabStep(
            "set_access",
            "Configure the bucket to be private (block all public access)",
            LabStepType.ClickInteraction,
            "By default, all S3 buckets are private. Never make sensitive data public."
        );
        step5.expectedValues.Add("public_access", "blocked");
        step5.experienceReward = 100;
        labSteps.Add(step5);

        // Step 6: Choose storage class
        var step6 = new LabStep(
            "storage_class",
            "Select S3 Standard storage class for frequently accessed data",
            LabStepType.Dropdown,
            "Different storage classes optimize for cost vs. access frequency"
        );
        step6.expectedValues.Add("storage_class", "STANDARD");
        step6.experienceReward = 50;
        labSteps.Add(step6);

        // Step 7: Configure lifecycle policy
        var step7 = new LabStep(
            "lifecycle_policy",
            "Create a lifecycle policy to move objects to Glacier after 90 days",
            LabStepType.Configuration,
            "Lifecycle policies automate cost savings by moving data to cheaper storage tiers"
        );
        step7.expectedValues.Add("transition_days", "90");
        step7.expectedValues.Add("transition_class", "GLACIER");
        step7.experienceReward = 150;
        labSteps.Add(step7);

        // Step 8: Review and create
        var step8 = new LabStep(
            "review_create",
            "Review your configuration and create the bucket",
            LabStepType.ClickInteraction,
            "Always review your settings before creating resources"
        );
        step8.experienceReward = 100;
        labSteps.Add(step8);
    }

    protected override void ShowStepUI(LabStep step)
    {
        // This would be implemented with actual UI components
        // For now, we'll log the step information
        Debug.Log($"[S3 Simulator] Showing step UI: {step.instruction}");
        Debug.Log($"[S3 Simulator] Hint: {step.hint}");
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
                Debug.Log($"[S3 Simulator] Missing input: {key}");
                isValid = false;
                break;
            }

            string userVal = userInput[key].ToString().Trim();

            // Case-insensitive comparison for most fields
            if (key == "bucket_name")
            {
                // Bucket names are case-sensitive and must be lowercase
                if (userVal != expectedVal)
                {
                    Debug.Log($"[S3 Simulator] Invalid bucket name. Expected: {expectedVal}, Got: {userVal}");
                    isValid = false;
                }
            }
            else if (!string.Equals(userVal, expectedVal, System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log($"[S3 Simulator] Invalid {key}. Expected: {expectedVal}, Got: {userVal}");
                isValid = false;
            }
        }

        // Store state for later steps
        if (isValid)
        {
            foreach (var input in userInput)
            {
                SetLabState(input.Key, input.Value);
            }

            // Special handling for bucket creation
            if (step.stepId == "create_bucket")
            {
                currentBucketName = userInput["bucket_name"].ToString();
                CreateBucket(currentBucketName);
            }
        }

        return isValid;
    }

    private void CreateBucket(string bucketName)
    {
        if (!buckets.ContainsKey(bucketName))
        {
            var bucket = new S3Bucket
            {
                name = bucketName,
                region = "us-east-1",
                createdDate = System.DateTime.Now.ToString(),
                versioningEnabled = false,
                encryptionEnabled = false,
                publicAccessBlocked = true,
                storageClass = "STANDARD"
            };

            buckets.Add(bucketName, bucket);
            Debug.Log($"[S3 Simulator] Bucket created: {bucketName}");
        }
    }

    protected override void UpdateQuestProgress()
    {
        // Update quest objective for S3 lab completion
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.CompleteObjective("TECH_STORAGE_001", "create_bucket");
        }
    }

    // Public methods for UI interaction
    public bool CreateBucket(string name, string region)
    {
        if (string.IsNullOrEmpty(name) || name.Length < 3)
        {
            Debug.LogWarning("[S3 Simulator] Bucket name must be at least 3 characters");
            return false;
        }

        if (buckets.ContainsKey(name))
        {
            Debug.LogWarning("[S3 Simulator] Bucket name already exists");
            return false;
        }

        var userInput = new Dictionary<string, object>
        {
            { "bucket_name", name },
            { "region", region }
        };

        return ValidateCurrentStep(userInput);
    }

    public List<string> GetAvailableRegions()
    {
        return new List<string>
        {
            "us-east-1",
            "us-west-2",
            "eu-west-1",
            "ap-southeast-1",
            "ap-northeast-1"
        };
    }

    public List<string> GetStorageClasses()
    {
        return new List<string>
        {
            "STANDARD",
            "STANDARD_IA",
            "INTELLIGENT_TIERING",
            "GLACIER",
            "GLACIER_DEEP_ARCHIVE"
        };
    }

    public Dictionary<string, S3Bucket> GetBuckets()
    {
        return new Dictionary<string, S3Bucket>(buckets);
    }
}

[System.Serializable]
public class S3Bucket
{
    public string name;
    public string region;
    public string createdDate;
    public bool versioningEnabled;
    public bool encryptionEnabled;
    public string encryptionType;
    public bool publicAccessBlocked;
    public string storageClass;
    public List<string> tags = new List<string>();
    public S3LifecyclePolicy lifecyclePolicy;
}

[System.Serializable]
public class S3LifecyclePolicy
{
    public int transitionDays;
    public string transitionToClass;
    public int expirationDays;
}
