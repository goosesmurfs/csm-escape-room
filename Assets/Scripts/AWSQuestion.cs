using System;
using System.Collections.Generic;

/// <summary>
/// Represents an AWS Cloud Practitioner exam question
/// </summary>
[Serializable]
public class AWSQuestion
{
    public string id;
    public string question;
    public string[] options;
    public int correctIndex;
    public string explanation;
    public ExamDomain domain;
    public DifficultyLevel difficulty;
    public string[] tags;

    public AWSQuestion(string id, string question, string[] options, int correctIndex, string explanation, ExamDomain domain, DifficultyLevel difficulty = DifficultyLevel.Medium, params string[] tags)
    {
        this.id = id;
        this.question = question;
        this.options = options;
        this.correctIndex = correctIndex;
        this.explanation = explanation;
        this.domain = domain;
        this.difficulty = difficulty;
        this.tags = tags;
    }
}

/// <summary>
/// AWS CCP Exam Domains based on official exam guide
/// </summary>
public enum ExamDomain
{
    CloudConcepts,          // Domain 1: 26% - Cloud concepts
    SecurityAndCompliance,  // Domain 2: 25% - Security and compliance
    Technology,             // Domain 3: 33% - Technology
    BillingAndPricing,      // Domain 4: 16% - Billing and pricing
    MixedChallenge,         // Mixed questions from all domains
    FullPracticeExam        // Boss level - full timed exam
}

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}

/// <summary>
/// Database of AWS Cloud Practitioner exam questions
/// </summary>
public static class AWSQuestionDatabase
{
    private static List<AWSQuestion> allQuestions = new List<AWSQuestion>();
    private static bool initialized = false;

    public static void Initialize()
    {
        if (initialized) return;

        // Domain 1: Cloud Concepts (26% of exam)
        AddCloudConceptsQuestions();

        // Domain 2: Security and Compliance (25% of exam)
        AddSecurityQuestions();

        // Domain 3: Technology (33% of exam)
        AddTechnologyQuestions();

        // Domain 4: Billing and Pricing (16% of exam)
        AddBillingQuestions();

        // Add expanded question database (145+ additional questions)
        AWSQuestionExpanded.AddExpandedQuestions(allQuestions);

        initialized = true;
        UnityEngine.Debug.Log($"[AWSQuestionDatabase] Initialized with {allQuestions.Count} total questions");
    }

    private static void AddCloudConceptsQuestions()
    {
        allQuestions.Add(new AWSQuestion(
            "CC001",
            "What are the key characteristics of cloud computing? (Select THREE)",
            new string[] {
                "On-demand self-service",
                "Requires significant upfront investment",
                "Rapid elasticity",
                "Limited network access",
                "Measured service/pay-as-you-go"
            },
            0, // Multiple correct: 0, 2, 4
            "The five essential characteristics of cloud computing according to NIST are: on-demand self-service, broad network access, resource pooling, rapid elasticity, and measured service. These characteristics define what makes cloud computing unique from traditional infrastructure.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "cloud-fundamentals", "nist-definition"
        ));

        allQuestions.Add(new AWSQuestion(
            "CC002",
            "Which AWS benefit allows you to pay only for the compute resources you use?",
            new string[] {
                "Economies of scale",
                "Pay-as-you-go pricing",
                "Global reach",
                "High availability"
            },
            1,
            "Pay-as-you-go pricing is a fundamental AWS benefit that allows customers to pay only for the individual services they need, for as long as they use them, without requiring long-term contracts or complex licensing. This eliminates large upfront investments in hardware and reduces the cost of provisioning and maintaining data centers.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "pricing-model", "cost-optimization"
        ));

        allQuestions.Add(new AWSQuestion(
            "CC003",
            "What is the AWS Well-Architected Framework pillar that focuses on the ability to support development and run workloads effectively?",
            new string[] {
                "Cost Optimization",
                "Operational Excellence",
                "Performance Efficiency",
                "Reliability"
            },
            1,
            "Operational Excellence is the pillar of the AWS Well-Architected Framework that focuses on the ability to support development and run workloads effectively, gain insight into their operations, and continuously improve supporting processes and procedures to deliver business value.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Medium,
            "well-architected", "operational-excellence"
        ));

        allQuestions.Add(new AWSQuestion(
            "CC004",
            "Which cloud deployment model allows an organization to have complete control over their infrastructure?",
            new string[] {
                "Public Cloud",
                "Private Cloud",
                "Hybrid Cloud",
                "Community Cloud"
            },
            1,
            "A Private Cloud deployment model provides infrastructure operated solely for a single organization. It offers the greatest level of control over security, privacy, and infrastructure, though it requires the organization to manage and maintain the infrastructure.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "deployment-models", "cloud-types"
        ));

        allQuestions.Add(new AWSQuestion(
            "CC005",
            "What is the benefit of using AWS Global Infrastructure with multiple Availability Zones?",
            new string[] {
                "Reduced costs",
                "Improved security",
                "High availability and fault tolerance",
                "Faster application development"
            },
            2,
            "Multiple Availability Zones (AZs) within AWS Regions provide high availability and fault tolerance. Each AZ is a physically separate location with independent power, cooling, and networking. By deploying applications across multiple AZs, you can protect against datacenter failures and maintain service availability.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Medium,
            "availability-zones", "high-availability", "fault-tolerance"
        ));

        allQuestions.Add(new AWSQuestion(
            "CC006",
            "Which of the following is NOT one of the six pillars of the AWS Well-Architected Framework?",
            new string[] {
                "Operational Excellence",
                "Security",
                "Scalability",
                "Cost Optimization"
            },
            2,
            "The six pillars of the AWS Well-Architected Framework are: Operational Excellence, Security, Reliability, Performance Efficiency, Cost Optimization, and Sustainability. While scalability is important, it is addressed within other pillars (primarily Reliability and Performance Efficiency) rather than being a separate pillar.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Medium,
            "well-architected", "pillars"
        ));

        allQuestions.Add(new AWSQuestion(
            "CC007",
            "What type of cloud computing model provides networking, storage, servers, and virtualization as a service?",
            new string[] {
                "Software as a Service (SaaS)",
                "Platform as a Service (PaaS)",
                "Infrastructure as a Service (IaaS)",
                "Function as a Service (FaaS)"
            },
            2,
            "Infrastructure as a Service (IaaS) provides fundamental computing resources including networking, storage, servers, and virtualization. Amazon EC2 is an example of IaaS where you have control over the operating system and can configure the infrastructure as needed.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "service-models", "iaas"
        ));

        allQuestions.Add(new AWSQuestion(
            "CC008",
            "How does AWS pricing provide cost advantages over traditional on-premises infrastructure?",
            new string[] {
                "AWS requires long-term contracts for better rates",
                "AWS passes savings from economies of scale to customers",
                "AWS charges the same rates regardless of usage",
                "AWS requires significant upfront capital expenditure"
            },
            1,
            "AWS aggregates usage from hundreds of thousands of customers in the cloud, achieving higher economies of scale. This translates into lower pay-as-you-go prices. As AWS grows and becomes more efficient, they reduce prices and pass savings to customers.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "pricing", "economies-of-scale"
        ));

        allQuestions.Add(new AWSQuestion(
            "CC009",
            "Which AWS service enables you to quickly deploy and manage applications in the AWS Cloud without worrying about the infrastructure?",
            new string[] {
                "Amazon EC2",
                "AWS Elastic Beanstalk",
                "Amazon VPC",
                "AWS CloudFormation"
            },
            1,
            "AWS Elastic Beanstalk is a Platform as a Service (PaaS) that allows you to quickly deploy and manage applications without worrying about the underlying infrastructure. You simply upload your code and Elastic Beanstalk handles deployment, capacity provisioning, load balancing, auto-scaling, and application health monitoring.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Medium,
            "paas", "elastic-beanstalk", "deployment"
        ));

        allQuestions.Add(new AWSQuestion(
            "CC010",
            "What is the primary benefit of AWS's ability to innovate faster?",
            new string[] {
                "Lower costs",
                "Better security",
                "Faster time to market for new features",
                "Simplified compliance"
            },
            2,
            "AWS's global infrastructure and broad set of services enable organizations to innovate faster by reducing the time needed to provision resources from weeks to minutes. This agility allows companies to experiment quickly, fail fast if needed, and bring new features and applications to market much faster than with traditional infrastructure.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "agility", "innovation"
        ));
    }

    private static void AddSecurityQuestions()
    {
        allQuestions.Add(new AWSQuestion(
            "SEC001",
            "According to the AWS Shared Responsibility Model, which of the following is AWS's responsibility?",
            new string[] {
                "Customer data encryption",
                "Physical security of data centers",
                "Operating system patches on EC2 instances",
                "IAM user access management"
            },
            1,
            "Under the AWS Shared Responsibility Model, AWS is responsible for 'Security OF the Cloud,' which includes physical security of data centers, hardware, networking, and the infrastructure that runs AWS services. Customers are responsible for 'Security IN the Cloud,' including data encryption, OS patches, and IAM management.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "shared-responsibility", "security"
        ));

        allQuestions.Add(new AWSQuestion(
            "SEC002",
            "Which AWS service should you use to create and manage cryptographic keys?",
            new string[] {
                "AWS IAM",
                "AWS KMS (Key Management Service)",
                "AWS Certificate Manager",
                "AWS Secrets Manager"
            },
            1,
            "AWS Key Management Service (KMS) is a managed service that makes it easy to create and control the cryptographic keys used to encrypt your data. KMS uses Hardware Security Modules (HSMs) to protect the security of your keys and integrates with most AWS services.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "kms", "encryption", "keys"
        ));

        allQuestions.Add(new AWSQuestion(
            "SEC003",
            "What is the AWS service that provides threat detection by analyzing AWS CloudTrail, VPC Flow Logs, and DNS logs?",
            new string[] {
                "AWS Shield",
                "AWS GuardDuty",
                "AWS Inspector",
                "AWS WAF"
            },
            1,
            "Amazon GuardDuty is a threat detection service that continuously monitors for malicious activity and unauthorized behavior. It analyzes multiple AWS data sources including CloudTrail event logs, VPC Flow Logs, and DNS logs to identify potential security threats.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "guardduty", "threat-detection", "monitoring"
        ));

        allQuestions.Add(new AWSQuestion(
            "SEC004",
            "Which AWS feature allows you to define who can access your AWS resources and what actions they can perform?",
            new string[] {
                "Security Groups",
                "IAM Policies",
                "Network ACLs",
                "AWS WAF Rules"
            },
            1,
            "IAM (Identity and Access Management) Policies are JSON documents that define permissions. They specify which principals (users, groups, roles) can access which AWS resources and what actions they can perform. IAM policies follow the principle of least privilege.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "iam", "policies", "access-control"
        ));

        allQuestions.Add(new AWSQuestion(
            "SEC005",
            "What is the best practice for managing root account credentials?",
            new string[] {
                "Use them for daily administrative tasks",
                "Share them with the team",
                "Enable MFA and use them only for account setup tasks",
                "Store them in a text file for easy access"
            },
            2,
            "AWS best practice for the root account is to enable Multi-Factor Authentication (MFA), use it only for initial account setup and billing tasks that require root access, and then use IAM users or roles for all other operations. Never share root credentials and store them securely.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "root-account", "mfa", "best-practices"
        ));

        allQuestions.Add(new AWSQuestion(
            "SEC006",
            "Which AWS service provides DDoS protection?",
            new string[] {
                "AWS WAF",
                "AWS Shield",
                "AWS GuardDuty",
                "Amazon Inspector"
            },
            1,
            "AWS Shield is a managed DDoS (Distributed Denial of Service) protection service. AWS Shield Standard is automatically enabled for all AWS customers at no additional cost, providing protection against common DDoS attacks. AWS Shield Advanced provides enhanced protections and 24/7 access to the AWS DDoS Response Team.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "shield", "ddos", "protection"
        ));

        allQuestions.Add(new AWSQuestion(
            "SEC007",
            "Which compliance program allows AWS customers in the healthcare industry to process, store, and transmit protected health information?",
            new string[] {
                "PCI DSS",
                "SOC 2",
                "HIPAA",
                "GDPR"
            },
            2,
            "HIPAA (Health Insurance Portability and Accountability Act) is a U.S. regulation that requires healthcare providers and their business associates to protect patient health information. AWS offers HIPAA-eligible services and signs Business Associate Agreements (BAAs) with customers who need to process protected health information (PHI).",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "compliance", "hipaa", "healthcare"
        ));

        allQuestions.Add(new AWSQuestion(
            "SEC008",
            "What AWS service enables you to assess, audit, and evaluate the configurations of your AWS resources for compliance?",
            new string[] {
                "AWS CloudTrail",
                "AWS Config",
                "AWS Inspector",
                "AWS Trusted Advisor"
            },
            1,
            "AWS Config is a service that enables you to assess, audit, and evaluate the configurations of your AWS resources. It continuously monitors and records AWS resource configurations and allows you to automate evaluation against desired configurations for compliance checking.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "config", "compliance", "auditing"
        ));

        allQuestions.Add(new AWSQuestion(
            "SEC009",
            "Which AWS service helps you meet compliance requirements by providing audit-ready reports?",
            new string[] {
                "AWS Artifact",
                "AWS CloudTrail",
                "AWS Config",
                "AWS Systems Manager"
            },
            0,
            "AWS Artifact provides on-demand access to AWS security and compliance reports and select online agreements. You can download audit-ready compliance reports and agreements such as SOC reports, PCI reports, and ISO certifications directly from the AWS Management Console.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "artifact", "compliance", "reports"
        ));

        allQuestions.Add(new AWSQuestion(
            "SEC010",
            "What is the principle that recommends granting only the permissions required to perform a task?",
            new string[] {
                "Defense in depth",
                "Least privilege",
                "Separation of duties",
                "Fail securely"
            },
            1,
            "The principle of least privilege is a security best practice that means granting only the permissions necessary to perform a specific task. This minimizes the potential impact of security breaches and reduces the risk of accidental or malicious actions. AWS IAM enables you to implement least privilege through fine-grained policies.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "least-privilege", "security-principles", "iam"
        ));
    }

    private static void AddTechnologyQuestions()
    {
        allQuestions.Add(new AWSQuestion(
            "TECH001",
            "Which AWS compute service allows you to run code without provisioning or managing servers?",
            new string[] {
                "Amazon EC2",
                "AWS Lambda",
                "Amazon ECS",
                "AWS Batch"
            },
            1,
            "AWS Lambda is a serverless compute service that lets you run code without provisioning or managing servers. You pay only for the compute time you consume. Lambda automatically scales your application by running code in response to triggers such as HTTP requests, file uploads, or database updates.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "lambda", "serverless", "compute"
        ));

        allQuestions.Add(new AWSQuestion(
            "TECH002",
            "Which AWS service is a fully managed NoSQL database service?",
            new string[] {
                "Amazon RDS",
                "Amazon Aurora",
                "Amazon DynamoDB",
                "Amazon Redshift"
            },
            2,
            "Amazon DynamoDB is a fully managed NoSQL database service that provides fast and predictable performance with seamless scalability. It's a key-value and document database that can handle any scale of traffic. Unlike RDS and Aurora (relational databases), DynamoDB is designed for non-relational data.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "dynamodb", "nosql", "database"
        ));

        allQuestions.Add(new AWSQuestion(
            "TECH003",
            "What AWS service would you use to distribute content to users globally with low latency?",
            new string[] {
                "Amazon S3",
                "Amazon CloudFront",
                "AWS Direct Connect",
                "Amazon Route 53"
            },
            1,
            "Amazon CloudFront is a content delivery network (CDN) service that securely delivers data, videos, applications, and APIs to customers globally with low latency and high transfer speeds. CloudFront uses a global network of edge locations to cache content closer to end users.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "cloudfront", "cdn", "content-delivery"
        ));

        allQuestions.Add(new AWSQuestion(
            "TECH004",
            "Which AWS storage service is best suited for storing frequently accessed data that requires millisecond access times?",
            new string[] {
                "Amazon S3 Glacier",
                "Amazon S3 Standard",
                "Amazon S3 Glacier Deep Archive",
                "AWS Backup"
            },
            1,
            "Amazon S3 Standard is designed for frequently accessed data and provides millisecond access times, high durability (99.999999999%), and high availability. S3 Glacier and Glacier Deep Archive are for long-term archival storage with retrieval times ranging from minutes to hours.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "s3", "storage", "performance"
        ));

        allQuestions.Add(new AWSQuestion(
            "TECH005",
            "What AWS service automatically distributes incoming application traffic across multiple targets?",
            new string[] {
                "Amazon Route 53",
                "AWS Auto Scaling",
                "Elastic Load Balancing",
                "Amazon CloudFront"
            },
            2,
            "Elastic Load Balancing (ELB) automatically distributes incoming application traffic across multiple targets, such as EC2 instances, containers, and IP addresses, in one or more Availability Zones. This increases the availability and fault tolerance of your applications.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "elb", "load-balancing", "high-availability"
        ));

        allQuestions.Add(new AWSQuestion(
            "TECH006",
            "Which AWS database service is optimized for data warehousing and analytics?",
            new string[] {
                "Amazon RDS",
                "Amazon DynamoDB",
                "Amazon Redshift",
                "Amazon ElastiCache"
            },
            2,
            "Amazon Redshift is a fast, fully managed data warehouse service optimized for analyzing data using standard SQL and existing Business Intelligence tools. It's designed for online analytical processing (OLAP) workloads and can analyze petabytes of data efficiently.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "redshift", "data-warehouse", "analytics"
        ));

        allQuestions.Add(new AWSQuestion(
            "TECH007",
            "What is the AWS service that provides a fully managed MySQL and PostgreSQL-compatible relational database?",
            new string[] {
                "Amazon DynamoDB",
                "Amazon Aurora",
                "Amazon DocumentDB",
                "Amazon Neptune"
            },
            1,
            "Amazon Aurora is a MySQL and PostgreSQL-compatible relational database built for the cloud. It combines the performance and availability of high-end commercial databases with the simplicity and cost-effectiveness of open-source databases, providing up to 5x better performance than standard MySQL and 3x better than PostgreSQL.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "aurora", "database", "rds"
        ));

        allQuestions.Add(new AWSQuestion(
            "TECH008",
            "Which AWS service enables you to create and manage Docker containers at scale?",
            new string[] {
                "AWS Lambda",
                "Amazon EC2",
                "Amazon ECS (Elastic Container Service)",
                "AWS Elastic Beanstalk"
            },
            2,
            "Amazon ECS (Elastic Container Service) is a fully managed container orchestration service that makes it easy to deploy, manage, and scale containerized applications using Docker. ECS eliminates the need to install and operate your own container orchestration infrastructure.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "ecs", "containers", "docker"
        ));

        allQuestions.Add(new AWSQuestion(
            "TECH009",
            "What AWS service provides a scalable DNS and domain registration service?",
            new string[] {
                "AWS Direct Connect",
                "Amazon Route 53",
                "Amazon CloudFront",
                "AWS Global Accelerator"
            },
            1,
            "Amazon Route 53 is a highly available and scalable Domain Name System (DNS) web service. It's designed to route end users to internet applications by translating domain names into IP addresses. Route 53 also offers domain registration and health checking of resources.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "route53", "dns", "networking"
        ));

        allQuestions.Add(new AWSQuestion(
            "TECH010",
            "Which AWS service allows you to run code in response to events such as changes to data in Amazon S3 or DynamoDB?",
            new string[] {
                "Amazon EC2",
                "AWS Lambda",
                "Amazon ECS",
                "AWS Batch"
            },
            1,
            "AWS Lambda can be triggered by various AWS services including S3, DynamoDB, Kinesis, SNS, and others. This event-driven architecture allows you to build applications that automatically respond to changes in data or system state without managing infrastructure.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "lambda", "event-driven", "serverless"
        ));

        allQuestions.Add(new AWSQuestion(
            "TECH011",
            "Which AWS service provides block storage volumes for use with Amazon EC2 instances?",
            new string[] {
                "Amazon S3",
                "Amazon EFS",
                "Amazon EBS (Elastic Block Store)",
                "AWS Storage Gateway"
            },
            2,
            "Amazon EBS (Elastic Block Store) provides persistent block storage volumes for use with EC2 instances. EBS volumes behave like raw, unformatted block devices that can be formatted with a file system and used like a hard drive. They are automatically replicated within their Availability Zone.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "ebs", "storage", "block-storage"
        ));

        allQuestions.Add(new AWSQuestion(
            "TECH012",
            "What AWS service provides a fully managed message queuing service?",
            new string[] {
                "Amazon SNS",
                "Amazon SQS (Simple Queue Service)",
                "Amazon Kinesis",
                "AWS Step Functions"
            },
            1,
            "Amazon SQS (Simple Queue Service) is a fully managed message queuing service that enables you to decouple and scale microservices, distributed systems, and serverless applications. SQS eliminates the complexity of managing message-oriented middleware and allows you to focus on differentiating work.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "sqs", "messaging", "queues"
        ));

        allQuestions.Add(new AWSQuestion(
            "TECH013",
            "Which AWS service automatically adds or removes compute capacity to meet demand?",
            new string[] {
                "Elastic Load Balancing",
                "AWS Auto Scaling",
                "Amazon EC2",
                "AWS Lambda"
            },
            1,
            "AWS Auto Scaling monitors your applications and automatically adjusts capacity to maintain steady, predictable performance at the lowest possible cost. It can scale EC2 instances, ECS tasks, DynamoDB tables, and other resources based on demand or a schedule you define.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "auto-scaling", "scalability", "elasticity"
        ));
    }

    private static void AddBillingQuestions()
    {
        allQuestions.Add(new AWSQuestion(
            "BILL001",
            "Which AWS service provides alerts when your AWS costs exceed a threshold you define?",
            new string[] {
                "AWS Cost Explorer",
                "AWS Budgets",
                "AWS Billing Dashboard",
                "AWS Cost and Usage Report"
            },
            1,
            "AWS Budgets allows you to set custom cost and usage budgets that alert you when your costs or usage exceed (or are forecasted to exceed) your budgeted amount. You can set budgets for costs, usage, Reserved Instance utilization, and Reserved Instance coverage.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "budgets", "cost-management", "alerts"
        ));

        allQuestions.Add(new AWSQuestion(
            "BILL002",
            "What pricing model allows you to reserve capacity and receive a significant discount compared to On-Demand pricing?",
            new string[] {
                "Spot Instances",
                "Savings Plans",
                "Reserved Instances",
                "Dedicated Hosts"
            },
            2,
            "Reserved Instances provide a significant discount (up to 75%) compared to On-Demand pricing in exchange for committing to use EC2 instances for a 1 or 3-year term. You reserve capacity in a specific Availability Zone and can choose from Standard (maximum savings) or Convertible (flexibility to change instance types) Reserved Instances.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "reserved-instances", "pricing-models", "discounts"
        ));

        allQuestions.Add(new AWSQuestion(
            "BILL003",
            "Which AWS service helps you visualize, understand, and manage your AWS costs and usage over time?",
            new string[] {
                "AWS Budgets",
                "AWS Cost Explorer",
                "AWS Billing Dashboard",
                "AWS Price List API"
            },
            1,
            "AWS Cost Explorer is a tool that enables you to visualize, understand, and manage your AWS costs and usage over time. It provides default reports and allows you to create custom reports to analyze cost and usage data, including trends, and identify opportunities for cost optimization.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "cost-explorer", "cost-analysis", "reporting"
        ));

        allQuestions.Add(new AWSQuestion(
            "BILL004",
            "What is the pricing model for Amazon S3?",
            new string[] {
                "Pay per hour for storage capacity reserved",
                "Pay only for storage used and data transfer out",
                "Fixed monthly subscription fee",
                "Pay per number of files stored"
            },
            1,
            "Amazon S3 pricing is based on pay-as-you-go model. You pay only for the storage you actually use (per GB), requests made (PUT, GET, etc.), and data transfer out of AWS. There's no minimum fee, no upfront commitment, and you only pay for what you use. Data transfer into S3 is free.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "s3-pricing", "storage-pricing", "pay-as-you-go"
        ));

        allQuestions.Add(new AWSQuestion(
            "BILL005",
            "Which EC2 pricing option allows you to bid on spare Amazon EC2 computing capacity?",
            new string[] {
                "On-Demand Instances",
                "Reserved Instances",
                "Spot Instances",
                "Dedicated Instances"
            },
            2,
            "EC2 Spot Instances allow you to request spare EC2 capacity at up to 90% discount compared to On-Demand prices. Spot Instances are ideal for workloads that are flexible about when they run and can handle interruptions, such as batch processing, data analysis, and background jobs.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "spot-instances", "pricing-models", "discounts"
        ));

        allQuestions.Add(new AWSQuestion(
            "BILL006",
            "What is AWS Free Tier?",
            new string[] {
                "A program that provides free access to all AWS services forever",
                "Temporary access to AWS services for testing only",
                "Free or trial usage tiers for many AWS services",
                "Discounted pricing for non-profit organizations"
            },
            2,
            "AWS Free Tier provides customers the ability to explore and try AWS services free of charge. It includes three types: Always Free (services that are always free within usage limits), 12 Months Free (free for 12 months after sign-up), and Trials (short-term trials for specific services).",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "free-tier", "pricing"
        ));

        allQuestions.Add(new AWSQuestion(
            "BILL007",
            "Which AWS tool provides recommendations for cost optimization based on your usage patterns?",
            new string[] {
                "AWS Budgets",
                "AWS Cost Explorer",
                "AWS Trusted Advisor",
                "AWS Organizations"
            },
            2,
            "AWS Trusted Advisor provides recommendations across five categories including cost optimization. It analyzes your AWS environment and identifies opportunities to save money, such as idle resources, Reserved Instance optimization, and right-sizing recommendations. Some Trusted Advisor checks are available to all customers, while advanced checks require a Business or Enterprise Support plan.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Medium,
            "trusted-advisor", "cost-optimization", "recommendations"
        ));

        allQuestions.Add(new AWSQuestion(
            "BILL008",
            "What is AWS Organizations primarily used for?",
            new string[] {
                "Managing user permissions within a single AWS account",
                "Consolidating billing across multiple AWS accounts",
                "Monitoring AWS resource usage",
                "Automating resource deployment"
            },
            1,
            "AWS Organizations helps you centrally manage and govern your environment across multiple AWS accounts. One of its key benefits is consolidated billing, which combines usage across all accounts in the organization to potentially reach volume discount tiers faster. It also provides centralized management and policy-based control.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Medium,
            "organizations", "consolidated-billing", "multi-account"
        ));

        allQuestions.Add(new AWSQuestion(
            "BILL009",
            "Which AWS service provides detailed information about your AWS costs and usage?",
            new string[] {
                "AWS Price List API",
                "AWS Billing Dashboard",
                "AWS Cost and Usage Report",
                "AWS CloudWatch"
            },
            2,
            "AWS Cost and Usage Report contains the most comprehensive set of AWS cost and usage data available. It includes metadata about AWS services, pricing, and your usage, allowing you to track Reserved Instances, understand costs at a resource level, and perform detailed analysis. You can publish these reports to Amazon S3.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Medium,
            "cost-usage-report", "detailed-billing", "reporting"
        ));

        allQuestions.Add(new AWSQuestion(
            "BILL010",
            "What happens when you use more than the AWS Free Tier limits?",
            new string[] {
                "Your AWS account is suspended",
                "You are charged standard pay-as-you-go rates",
                "You receive a warning but no charges",
                "You cannot use the service anymore"
            },
            1,
            "When you exceed the AWS Free Tier usage limits, you are automatically charged at standard pay-as-you-go service rates for the additional usage. There's no suspension or limitation of services. AWS will bill you only for usage beyond the Free Tier limits, so it's important to monitor your usage to avoid unexpected charges.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "free-tier", "billing", "overages"
        ));
    }

    public static List<AWSQuestion> GetQuestionsForDomain(ExamDomain domain, int count = -1)
    {
        Initialize();

        List<AWSQuestion> questions = new List<AWSQuestion>();

        if (domain == ExamDomain.MixedChallenge)
        {
            // Get random questions from all domains
            questions.AddRange(GetRandomQuestions(count > 0 ? count : 20));
        }
        else if (domain == ExamDomain.FullPracticeExam)
        {
            // Get 65 questions proportionally distributed across domains (like real exam)
            questions.AddRange(GetRandomQuestions(17, ExamDomain.Technology)); // 33% = ~17 questions
            questions.AddRange(GetRandomQuestions(17, ExamDomain.CloudConcepts)); // 26% = ~17 questions
            questions.AddRange(GetRandomQuestions(16, ExamDomain.SecurityAndCompliance)); // 25% = ~16 questions
            questions.AddRange(GetRandomQuestions(10, ExamDomain.BillingAndPricing)); // 16% = ~10 questions
        }
        else
        {
            // Get questions for specific domain
            foreach (var q in allQuestions)
            {
                if (q.domain == domain)
                {
                    questions.Add(q);
                }
            }

            // Shuffle questions
            ShuffleList(questions);

            // Limit count if specified
            if (count > 0 && questions.Count > count)
            {
                questions.RemoveRange(count, questions.Count - count);
            }
        }

        return questions;
    }

    private static List<AWSQuestion> GetRandomQuestions(int count, ExamDomain? specificDomain = null)
    {
        List<AWSQuestion> sourceQuestions = new List<AWSQuestion>();

        if (specificDomain.HasValue)
        {
            foreach (var q in allQuestions)
            {
                if (q.domain == specificDomain.Value)
                {
                    sourceQuestions.Add(q);
                }
            }
        }
        else
        {
            sourceQuestions.AddRange(allQuestions);
        }

        ShuffleList(sourceQuestions);

        if (count > 0 && sourceQuestions.Count > count)
        {
            sourceQuestions.RemoveRange(count, sourceQuestions.Count - count);
        }

        return sourceQuestions;
    }

    private static void ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static int GetTotalQuestionCount()
    {
        Initialize();
        return allQuestions.Count;
    }
}
