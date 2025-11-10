using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simulates AWS Lambda service for hands-on learning
/// </summary>
public class LambdaSimulator : AWSServiceSimulator
{
    private Dictionary<string, LambdaFunction> functions = new Dictionary<string, LambdaFunction>();
    private string currentFunctionName = "";

    private void Start()
    {
        serviceName = "AWS Lambda";
        serviceDescription = "Learn to create and deploy serverless functions";
        relatedDomain = ExamDomain.Technology;

        InitializeLab();
    }

    private void InitializeLab()
    {
        labSteps.Clear();

        // Step 1: Create function
        var step1 = new LabStep(
            "create_function",
            "Create a Lambda function named 'ImageProcessor'",
            LabStepType.TextInput,
            "Lambda function names must be unique within your account and region"
        );
        step1.expectedValues.Add("function_name", "ImageProcessor");
        step1.experienceReward = 100;
        labSteps.Add(step1);

        // Step 2: Choose runtime
        var step2 = new LabStep(
            "choose_runtime",
            "Select Python 3.9 as the runtime environment",
            LabStepType.Dropdown,
            "Lambda supports multiple runtimes including Python, Node.js, Java, Go, and .NET"
        );
        step2.expectedValues.Add("runtime", "Python 3.9");
        step2.experienceReward = 75;
        labSteps.Add(step2);

        // Step 3: Configure permissions
        var step3 = new LabStep(
            "create_role",
            "Create a new IAM execution role with basic Lambda permissions",
            LabStepType.ClickInteraction,
            "Lambda needs an execution role to access AWS services and write logs"
        );
        step3.expectedValues.Add("role_name", "lambda-execution-role");
        step3.experienceReward = 100;
        labSteps.Add(step3);

        // Step 4: Write function code
        var step4 = new LabStep(
            "write_code",
            "Write a simple Lambda function that processes an S3 event:\n\ndef lambda_handler(event, context):\n    return {'statusCode': 200, 'body': 'Success'}",
            LabStepType.CodeEditor,
            "The handler is the entry point for your Lambda function"
        );
        step4.expectedValues.Add("has_handler", "true");
        step4.experienceReward = 150;
        labSteps.Add(step4);

        // Step 5: Configure memory
        var step5 = new LabStep(
            "set_memory",
            "Set the memory allocation to 512 MB",
            LabStepType.TextInput,
            "Lambda allocates CPU power proportional to the memory. More memory = more CPU."
        );
        step5.expectedValues.Add("memory", "512");
        step5.experienceReward = 50;
        labSteps.Add(step5);

        // Step 6: Set timeout
        var step6 = new LabStep(
            "set_timeout",
            "Set the timeout to 30 seconds",
            LabStepType.TextInput,
            "Timeout determines how long Lambda allows your function to run before stopping it"
        );
        step6.expectedValues.Add("timeout", "30");
        step6.experienceReward = 50;
        labSteps.Add(step6);

        // Step 7: Add trigger
        var step7 = new LabStep(
            "add_trigger",
            "Add an S3 trigger for the 'my-bucket' to invoke the function on object creation",
            LabStepType.Configuration,
            "Triggers automatically invoke your Lambda function in response to events"
        );
        step7.expectedValues.Add("trigger_service", "S3");
        step7.expectedValues.Add("event_type", "ObjectCreated");
        step7.experienceReward = 150;
        labSteps.Add(step7);

        // Step 8: Configure environment variables
        var step8 = new LabStep(
            "env_variables",
            "Add an environment variable: KEY='S3_BUCKET', VALUE='my-bucket'",
            LabStepType.TextInput,
            "Environment variables let you pass configuration to your function without changing code"
        );
        step8.expectedValues.Add("env_key", "S3_BUCKET");
        step8.expectedValues.Add("env_value", "my-bucket");
        step8.experienceReward = 75;
        labSteps.Add(step8);

        // Step 9: Deploy function
        var step9 = new LabStep(
            "deploy_function",
            "Deploy your Lambda function",
            LabStepType.ClickInteraction,
            "Deployment packages and uploads your code to Lambda"
        );
        step9.experienceReward = 100;
        labSteps.Add(step9);

        // Step 10: Test function
        var step10 = new LabStep(
            "test_function",
            "Create a test event and invoke the function",
            LabStepType.ClickInteraction,
            "Testing ensures your function works correctly before using it in production"
        );
        step10.experienceReward = 100;
        labSteps.Add(step10);
    }

    protected override void ShowStepUI(LabStep step)
    {
        Debug.Log($"[Lambda Simulator] Showing step UI: {step.instruction}");
        Debug.Log($"[Lambda Simulator] Hint: {step.hint}");
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
                Debug.Log($"[Lambda Simulator] Missing input: {key}");
                isValid = false;
                break;
            }

            string userVal = userInput[key].ToString().Trim();

            if (!string.Equals(userVal, expectedVal, System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log($"[Lambda Simulator] Invalid {key}. Expected: {expectedVal}, Got: {userVal}");
                isValid = false;
            }
        }

        if (isValid)
        {
            foreach (var input in userInput)
            {
                SetLabState(input.Key, input.Value);
            }

            // Special handling for function creation
            if (step.stepId == "create_function")
            {
                currentFunctionName = userInput["function_name"].ToString();
            }

            // Special handling for function deployment
            if (step.stepId == "deploy_function")
            {
                DeployFunction();
            }

            // Special handling for function test
            if (step.stepId == "test_function")
            {
                TestFunction();
            }
        }

        return isValid;
    }

    private void DeployFunction()
    {
        var function = new LambdaFunction
        {
            functionName = currentFunctionName,
            runtime = GetLabState("runtime")?.ToString() ?? "Python 3.9",
            handler = "lambda_function.lambda_handler",
            role = GetLabState("role_name")?.ToString() ?? "lambda-execution-role",
            memory = int.Parse(GetLabState("memory")?.ToString() ?? "512"),
            timeout = int.Parse(GetLabState("timeout")?.ToString() ?? "30"),
            state = "Active",
            lastModified = System.DateTime.Now.ToString(),
            codeSize = 1024,
            functionArn = $"arn:aws:lambda:us-east-1:123456789012:function:{currentFunctionName}"
        };

        // Add environment variables
        if (HasLabState("env_key") && HasLabState("env_value"))
        {
            function.environmentVariables.Add(
                GetLabState("env_key").ToString(),
                GetLabState("env_value").ToString()
            );
        }

        // Add trigger
        if (HasLabState("trigger_service"))
        {
            function.triggers.Add(new LambdaTrigger
            {
                service = GetLabState("trigger_service").ToString(),
                eventType = GetLabState("event_type")?.ToString() ?? "Unknown",
                enabled = true
            });
        }

        functions.Add(currentFunctionName, function);
        Debug.Log($"[Lambda Simulator] Function deployed: {currentFunctionName}");
    }

    private void TestFunction()
    {
        if (functions.ContainsKey(currentFunctionName))
        {
            Debug.Log($"[Lambda Simulator] Testing function: {currentFunctionName}");
            Debug.Log("[Lambda Simulator] Function executed successfully!");
            Debug.Log("[Lambda Simulator] Response: {'statusCode': 200, 'body': 'Success'}");
            Debug.Log("[Lambda Simulator] Duration: 156 ms, Billed Duration: 200 ms, Memory: 512 MB");
        }
    }

    protected override void UpdateQuestProgress()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.CompleteObjective("TECH_LAMBDA_001", "deploy_function");
        }
    }

    // Public methods for UI
    public List<string> GetAvailableRuntimes()
    {
        return new List<string>
        {
            "Python 3.9",
            "Python 3.8",
            "Node.js 18",
            "Node.js 16",
            "Java 11",
            "Go 1.x",
            ".NET 6"
        };
    }

    public List<int> GetMemoryOptions()
    {
        List<int> options = new List<int>();
        for (int i = 128; i <= 3008; i += 64)
        {
            options.Add(i);
        }
        return options;
    }

    public List<string> GetTriggerServices()
    {
        return new List<string>
        {
            "S3",
            "DynamoDB",
            "SNS",
            "SQS",
            "API Gateway",
            "EventBridge",
            "CloudWatch"
        };
    }

    public Dictionary<string, LambdaFunction> GetFunctions()
    {
        return new Dictionary<string, LambdaFunction>(functions);
    }
}

[System.Serializable]
public class LambdaFunction
{
    public string functionName;
    public string runtime;
    public string handler;
    public string role;
    public int memory;
    public int timeout;
    public string state;
    public string lastModified;
    public int codeSize;
    public string functionArn;
    public Dictionary<string, string> environmentVariables = new Dictionary<string, string>();
    public List<LambdaTrigger> triggers = new List<LambdaTrigger>();
}

[System.Serializable]
public class LambdaTrigger
{
    public string service;
    public string eventType;
    public bool enabled;
}
