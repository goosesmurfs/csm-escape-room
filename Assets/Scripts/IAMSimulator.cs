using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simulates AWS IAM service for hands-on learning
/// </summary>
public class IAMSimulator : AWSServiceSimulator
{
    private Dictionary<string, IAMUser> users = new Dictionary<string, IAMUser>();
    private Dictionary<string, IAMPolicy> policies = new Dictionary<string, IAMPolicy>();
    private string currentUsername = "";
    private string currentPolicyName = "";

    private void Start()
    {
        serviceName = "AWS IAM";
        serviceDescription = "Learn to create users, groups, roles, and policies for secure access management";
        relatedDomain = ExamDomain.SecurityAndCompliance;

        InitializeLab();
    }

    private void InitializeLab()
    {
        labSteps.Clear();

        // Step 1: Create IAM user
        var step1 = new LabStep(
            "create_user",
            "Create an IAM user named 'Developer'",
            LabStepType.TextInput,
            "IAM users represent individual people or applications that interact with AWS"
        );
        step1.expectedValues.Add("username", "Developer");
        step1.experienceReward = 75;
        labSteps.Add(step1);

        // Step 2: Set access type
        var step2 = new LabStep(
            "access_type",
            "Grant 'Programmatic access' for API, CLI, and SDK access",
            LabStepType.MultipleChoice,
            "Programmatic access creates an access key ID and secret access key for AWS API calls"
        );
        step2.expectedValues.Add("access_type", "Programmatic");
        step2.experienceReward = 50;
        labSteps.Add(step2);

        // Step 3: Create policy
        var step3 = new LabStep(
            "create_policy",
            "Create a custom policy named 'S3ReadOnlyAccess'",
            LabStepType.TextInput,
            "Policies define permissions: what actions are allowed or denied on which resources"
        );
        step3.expectedValues.Add("policy_name", "S3ReadOnlyAccess");
        step3.experienceReward = 100;
        labSteps.Add(step3);

        // Step 4: Define policy JSON
        var step4 = new LabStep(
            "policy_json",
            "Build a policy that allows 's3:GetObject' and 's3:ListBucket' actions",
            LabStepType.CodeEditor,
            "IAM policies are JSON documents with Effect, Action, and Resource elements"
        );
        step4.expectedValues.Add("actions", "s3:GetObject,s3:ListBucket");
        step4.expectedValues.Add("effect", "Allow");
        step4.experienceReward = 200;
        labSteps.Add(step4);

        // Step 5: Specify resources
        var step5 = new LabStep(
            "set_resource",
            "Set the resource to all S3 buckets: 'arn:aws:s3:::*'",
            LabStepType.TextInput,
            "ARNs (Amazon Resource Names) uniquely identify AWS resources"
        );
        step5.expectedValues.Add("resource", "arn:aws:s3:::*");
        step5.experienceReward = 75;
        labSteps.Add(step5);

        // Step 6: Attach policy
        var step6 = new LabStep(
            "attach_policy",
            "Attach the 'S3ReadOnlyAccess' policy to the 'Developer' user",
            LabStepType.ClickInteraction,
            "Attaching policies grants the permissions defined in the policy to the user"
        );
        step6.experienceReward = 100;
        labSteps.Add(step6);

        // Step 7: Enable MFA
        var step7 = new LabStep(
            "enable_mfa",
            "Enable Multi-Factor Authentication (MFA) for the user",
            LabStepType.ClickInteraction,
            "MFA adds an extra layer of security by requiring a second authentication factor"
        );
        step7.expectedValues.Add("mfa_enabled", "true");
        step7.experienceReward = 100;
        labSteps.Add(step7);

        // Step 8: Set password policy
        var step8 = new LabStep(
            "password_policy",
            "Configure password policy: minimum 12 characters, require uppercase and numbers",
            LabStepType.Configuration,
            "Strong password policies help protect accounts from unauthorized access"
        );
        step8.expectedValues.Add("min_length", "12");
        step8.expectedValues.Add("require_uppercase", "true");
        step8.expectedValues.Add("require_numbers", "true");
        step8.experienceReward = 100;
        labSteps.Add(step8);

        // Step 9: Review permissions
        var step9 = new LabStep(
            "review_permissions",
            "Review the effective permissions for the Developer user",
            LabStepType.ClickInteraction,
            "Always review permissions to ensure users have appropriate access (principle of least privilege)"
        );
        step9.experienceReward = 75;
        labSteps.Add(step9);

        // Step 10: Test access
        var step10 = new LabStep(
            "test_access",
            "Verify the user can read from S3 but cannot write",
            LabStepType.ClickInteraction,
            "Testing ensures your policies work as intended"
        );
        step10.experienceReward = 100;
        labSteps.Add(step10);
    }

    protected override void ShowStepUI(LabStep step)
    {
        Debug.Log($"[IAM Simulator] Showing step UI: {step.instruction}");
        Debug.Log($"[IAM Simulator] Hint: {step.hint}");
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
                Debug.Log($"[IAM Simulator] Missing input: {key}");
                isValid = false;
                break;
            }

            string userVal = userInput[key].ToString().Trim();

            // Special handling for actions (comma-separated list)
            if (key == "actions")
            {
                var expectedActions = new HashSet<string>(expectedVal.Split(','));
                var userActions = new HashSet<string>(userVal.Split(','));

                foreach (var action in expectedActions)
                {
                    if (!userActions.Contains(action.Trim()))
                    {
                        Debug.Log($"[IAM Simulator] Missing required action: {action}");
                        isValid = false;
                    }
                }
            }
            else if (!string.Equals(userVal, expectedVal, System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log($"[IAM Simulator] Invalid {key}. Expected: {expectedVal}, Got: {userVal}");
                isValid = false;
            }
        }

        if (isValid)
        {
            foreach (var input in userInput)
            {
                SetLabState(input.Key, input.Value);
            }

            // Special handling
            if (step.stepId == "create_user")
            {
                currentUsername = userInput["username"].ToString();
                CreateUser(currentUsername);
            }
            else if (step.stepId == "create_policy")
            {
                currentPolicyName = userInput["policy_name"].ToString();
                CreatePolicy(currentPolicyName);
            }
            else if (step.stepId == "attach_policy")
            {
                AttachPolicyToUser();
            }
            else if (step.stepId == "test_access")
            {
                TestUserAccess();
            }
        }

        return isValid;
    }

    private void CreateUser(string username)
    {
        var user = new IAMUser
        {
            username = username,
            userId = "AIDA" + System.Guid.NewGuid().ToString().Substring(0, 16).ToUpper(),
            arn = $"arn:aws:iam::123456789012:user/{username}",
            createDate = System.DateTime.Now.ToString(),
            accessType = "None",
            mfaEnabled = false
        };

        users.Add(username, user);
        Debug.Log($"[IAM Simulator] User created: {username}");
    }

    private void CreatePolicy(string policyName)
    {
        var policy = new IAMPolicy
        {
            policyName = policyName,
            policyId = "ANPA" + System.Guid.NewGuid().ToString().Substring(0, 16).ToUpper(),
            arn = $"arn:aws:iam::123456789012:policy/{policyName}",
            createDate = System.DateTime.Now.ToString(),
            effect = GetLabState("effect")?.ToString() ?? "Allow"
        };

        // Add actions
        if (HasLabState("actions"))
        {
            string actionsStr = GetLabState("actions").ToString();
            policy.actions.AddRange(actionsStr.Split(','));
        }

        // Add resource
        if (HasLabState("resource"))
        {
            policy.resources.Add(GetLabState("resource").ToString());
        }

        policies.Add(policyName, policy);
        Debug.Log($"[IAM Simulator] Policy created: {policyName}");
        Debug.Log($"[IAM Simulator] Policy allows: {string.Join(", ", policy.actions)} on {string.Join(", ", policy.resources)}");
    }

    private void AttachPolicyToUser()
    {
        if (users.ContainsKey(currentUsername) && policies.ContainsKey(currentPolicyName))
        {
            var user = users[currentUsername];
            var policy = policies[currentPolicyName];

            if (!user.attachedPolicies.Contains(currentPolicyName))
            {
                user.attachedPolicies.Add(currentPolicyName);
                Debug.Log($"[IAM Simulator] Policy '{currentPolicyName}' attached to user '{currentUsername}'");
            }
        }
    }

    private void TestUserAccess()
    {
        Debug.Log($"[IAM Simulator] Testing access for user: {currentUsername}");
        Debug.Log("[IAM Simulator] Test 1: s3:GetObject - ALLOWED ✓");
        Debug.Log("[IAM Simulator] Test 2: s3:ListBucket - ALLOWED ✓");
        Debug.Log("[IAM Simulator] Test 3: s3:PutObject - DENIED ✗");
        Debug.Log("[IAM Simulator] Test 4: s3:DeleteObject - DENIED ✗");
        Debug.Log("[IAM Simulator] Policy is working correctly! User has read-only access to S3.");
    }

    protected override void UpdateQuestProgress()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.CompleteObjective("SEC_IAM_001", "create_policy");
        }
    }

    // Public methods for UI
    public List<string> GetManagedPolicies()
    {
        return new List<string>
        {
            "AdministratorAccess",
            "PowerUserAccess",
            "ReadOnlyAccess",
            "AmazonS3FullAccess",
            "AmazonS3ReadOnlyAccess",
            "AmazonEC2FullAccess",
            "AmazonRDSFullAccess"
        };
    }

    public List<string> GetCommonActions()
    {
        return new List<string>
        {
            "s3:GetObject",
            "s3:PutObject",
            "s3:ListBucket",
            "ec2:DescribeInstances",
            "ec2:StartInstances",
            "ec2:StopInstances",
            "lambda:InvokeFunction",
            "dynamodb:GetItem",
            "dynamodb:PutItem"
        };
    }

    public string GeneratePolicyJSON(string effect, List<string> actions, List<string> resources)
    {
        string actionsStr = string.Join("\",\"", actions);
        string resourcesStr = string.Join("\",\"", resources);

        return @"{
  ""Version"": ""2012-10-17"",
  ""Statement"": [
    {
      ""Effect"": """ + effect + @""",
      ""Action"": [
        """ + actionsStr + @"""
      ],
      ""Resource"": [
        """ + resourcesStr + @"""
      ]
    }
  ]
}";
    }

    public Dictionary<string, IAMUser> GetUsers()
    {
        return new Dictionary<string, IAMUser>(users);
    }

    public Dictionary<string, IAMPolicy> GetPolicies()
    {
        return new Dictionary<string, IAMPolicy>(policies);
    }
}

[System.Serializable]
public class IAMUser
{
    public string username;
    public string userId;
    public string arn;
    public string createDate;
    public string accessType;
    public bool mfaEnabled;
    public List<string> attachedPolicies = new List<string>();
    public List<string> groups = new List<string>();
}

[System.Serializable]
public class IAMPolicy
{
    public string policyName;
    public string policyId;
    public string arn;
    public string createDate;
    public string effect;
    public List<string> actions = new List<string>();
    public List<string> resources = new List<string>();
    public int attachedEntities = 0;
}

[System.Serializable]
public class IAMRole
{
    public string roleName;
    public string roleId;
    public string arn;
    public string trustPolicy;
    public List<string> attachedPolicies = new List<string>();
}
