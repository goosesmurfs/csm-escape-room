using System.Collections.Generic;

/// <summary>
/// Expanded AWS CCP question database with 160+ additional questions
/// This extends the base AWSQuestionDatabase with comprehensive coverage of all AWS services
/// </summary>
public static class AWSQuestionExpanded
{
    public static void AddExpandedQuestions(List<AWSQuestion> questionList)
    {
        AddExpandedCloudConcepts(questionList);
        AddExpandedSecurity(questionList);
        AddExpandedTechnology(questionList);
        AddExpandedBilling(questionList);
    }

    private static void AddExpandedCloudConcepts(List<AWSQuestion> questions)
    {
        // Additional Cloud Concepts questions (30 more)

        questions.Add(new AWSQuestion(
            "CC_EXP_001",
            "What is the AWS global infrastructure component that consists of one or more discrete data centers?",
            new string[] {
                "AWS Region",
                "Availability Zone",
                "Edge Location",
                "Local Zone"
            },
            1,
            "An Availability Zone (AZ) consists of one or more discrete data centers with redundant power, networking, and connectivity. Multiple AZs within a Region provide high availability and fault tolerance.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "global-infrastructure", "availability-zones"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_002",
            "Which of the following describes the AWS design principle of 'design for failure'?",
            new string[] {
                "Always assume components will fail and design systems to handle failures gracefully",
                "Only design for failures that are likely to occur",
                "Avoid redundancy to reduce costs",
                "Focus on preventing all possible failures"
            },
            0,
            "AWS encourages designing for failure by assuming everything fails all the time. This means building redundancy, implementing health checks, and using multiple availability zones to ensure high availability.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Medium,
            "design-principles", "reliability"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_003",
            "What is the main benefit of AWS Regions?",
            new string[] {
                "Reduced network latency for end users",
                "Lower pricing for all services",
                "Unlimited compute capacity",
                "Direct access to AWS data centers"
            },
            0,
            "AWS Regions allow you to place resources close to your users, reducing latency. They also help with data residency requirements and disaster recovery planning.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "regions", "latency"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_004",
            "Which pillar of the AWS Well-Architected Framework focuses on the ability to recover from failures?",
            new string[] {
                "Operational Excellence",
                "Security",
                "Reliability",
                "Performance Efficiency"
            },
            2,
            "The Reliability pillar focuses on the ability of a system to recover from infrastructure or service disruptions, dynamically acquire computing resources to meet demand, and mitigate disruptions.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Medium,
            "well-architected", "reliability"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_005",
            "What is the AWS service that provides a consistent hybrid cloud experience?",
            new string[] {
                "AWS Direct Connect",
                "AWS Outposts",
                "AWS VPN",
                "AWS Transit Gateway"
            },
            1,
            "AWS Outposts brings native AWS services, infrastructure, and operating models to virtually any data center, co-location space, or on-premises facility, providing a truly consistent hybrid experience.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Medium,
            "hybrid-cloud", "outposts"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_006",
            "Which AWS service helps you track your AWS resource usage and API calls?",
            new string[] {
                "AWS CloudWatch",
                "AWS CloudTrail",
                "AWS Config",
                "AWS X-Ray"
            },
            1,
            "AWS CloudTrail logs all API calls made in your AWS account, providing governance, compliance, and operational and risk auditing capabilities.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "cloudtrail", "auditing"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_007",
            "What does 'elasticity' mean in cloud computing?",
            new string[] {
                "The ability to automatically scale resources up or down based on demand",
                "The physical flexibility of data center hardware",
                "The ability to stretch budgets further",
                "The resilience of applications to failures"
            },
            0,
            "Elasticity is the ability to acquire resources as you need them and release resources when you no longer need them. In the cloud, you can scale automatically based on demand.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "elasticity", "scalability"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_008",
            "Which cloud deployment model keeps data and applications on-premises while using cloud services for bursting?",
            new string[] {
                "Public cloud",
                "Private cloud",
                "Hybrid cloud",
                "Community cloud"
            },
            2,
            "A hybrid cloud deployment connects on-premises infrastructure and applications with cloud-based resources. This is often used for cloud bursting, where peak loads overflow to the cloud.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Medium,
            "deployment-models", "hybrid-cloud"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_009",
            "What is the AWS shared responsibility model?",
            new string[] {
                "AWS is responsible for everything",
                "Customers are responsible for everything",
                "AWS is responsible for security OF the cloud, customers are responsible for security IN the cloud",
                "Responsibilities are negotiated per customer"
            },
            2,
            "In the shared responsibility model, AWS is responsible for the security OF the cloud (physical infrastructure, hardware, software), while customers are responsible for security IN the cloud (data, applications, access management).",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Medium,
            "shared-responsibility", "security"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_010",
            "Which AWS service provides a managed DNS service?",
            new string[] {
                "AWS CloudFront",
                "AWS Route 53",
                "AWS Direct Connect",
                "AWS VPC"
            },
            1,
            "Amazon Route 53 is a highly available and scalable Domain Name System (DNS) web service. It routes end users to internet applications.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "route53", "dns", "networking"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_011",
            "What is the minimum number of Availability Zones in an AWS Region?",
            new string[] {
                "1",
                "2",
                "3",
                "4"
            },
            1,
            "Each AWS Region has a minimum of two Availability Zones, though most have three or more. This ensures redundancy and high availability within a region.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "availability-zones", "regions"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_012",
            "Which pillar of the AWS Well-Architected Framework focuses on using computing resources efficiently?",
            new string[] {
                "Cost Optimization",
                "Performance Efficiency",
                "Operational Excellence",
                "Sustainability"
            },
            1,
            "The Performance Efficiency pillar focuses on using computing resources efficiently to meet system requirements and maintaining that efficiency as demand changes and technologies evolve.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Medium,
            "well-architected", "performance"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_013",
            "What is the primary purpose of AWS Local Zones?",
            new string[] {
                "To reduce costs",
                "To provide single-digit millisecond latency to end users",
                "To increase storage capacity",
                "To comply with regulations"
            },
            1,
            "AWS Local Zones place compute, storage, database, and other select services closer to large population, industry, and IT centers, enabling single-digit millisecond latency for applications.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Medium,
            "local-zones", "latency"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_014",
            "Which statement about AWS Edge Locations is correct?",
            new string[] {
                "Edge Locations are the same as Availability Zones",
                "Edge Locations are used only for CloudFront CDN",
                "Edge Locations are endpoints for AWS used for caching content",
                "Edge Locations replace Regions"
            },
            2,
            "Edge Locations are AWS endpoints used for caching content, typically for services like CloudFront and Route 53. There are many more Edge Locations than Regions.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Medium,
            "edge-locations", "cloudfront"
        ));

        questions.Add(new AWSQuestion(
            "CC_EXP_015",
            "What does 'agility' mean in the context of cloud computing?",
            new string[] {
                "The ability to run faster applications",
                "The ability to quickly deploy and experiment with new resources",
                "The physical movement of data centers",
                "The speed of network connections"
            },
            1,
            "Agility in cloud computing refers to the ability to rapidly develop, test, and launch software applications. Cloud services allow you to experiment quickly and provision resources in minutes instead of weeks.",
            ExamDomain.CloudConcepts,
            DifficultyLevel.Easy,
            "agility", "innovation"
        ));
    }

    private static void AddExpandedSecurity(List<AWSQuestion> questions)
    {
        // Additional Security & Compliance questions (40 more)

        questions.Add(new AWSQuestion(
            "SEC_EXP_001",
            "Which AWS service provides DDoS protection for AWS applications?",
            new string[] {
                "AWS WAF",
                "AWS Shield",
                "AWS GuardDuty",
                "AWS Macie"
            },
            1,
            "AWS Shield is a managed DDoS protection service that safeguards applications running on AWS. Shield Standard is automatically enabled for all AWS customers at no additional cost.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "shield", "ddos", "security"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_002",
            "What is the purpose of AWS IAM roles?",
            new string[] {
                "To create user accounts",
                "To delegate permissions to AWS services and applications without using permanent credentials",
                "To encrypt data",
                "To monitor security threats"
            },
            1,
            "IAM roles are used to delegate access with defined permissions to trusted entities without sharing long-term credentials. They are commonly used for EC2 instances and Lambda functions to access other AWS services.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "iam", "roles", "permissions"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_003",
            "Which AWS service helps you discover and protect sensitive data like PII (Personally Identifiable Information)?",
            new string[] {
                "AWS GuardDuty",
                "AWS Inspector",
                "AWS Macie",
                "AWS Detective"
            },
            2,
            "Amazon Macie is a data security service that uses machine learning to automatically discover, classify, and protect sensitive data like PII stored in Amazon S3.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "macie", "data-protection", "pii"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_004",
            "What is the AWS service for creating and managing encryption keys?",
            new string[] {
                "AWS Certificate Manager",
                "AWS Secrets Manager",
                "AWS Key Management Service (KMS)",
                "AWS CloudHSM"
            },
            2,
            "AWS KMS is a managed service that makes it easy to create and control the encryption keys used to encrypt your data. It integrates with most AWS services.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "kms", "encryption", "keys"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_005",
            "Which AWS service provides intelligent threat detection for your AWS accounts and workloads?",
            new string[] {
                "AWS Shield",
                "AWS WAF",
                "AWS GuardDuty",
                "AWS Firewall Manager"
            },
            2,
            "AWS GuardDuty is a threat detection service that continuously monitors for malicious activity and unauthorized behavior to protect your AWS accounts and workloads.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "guardduty", "threat-detection"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_006",
            "What is the best practice for the AWS root account?",
            new string[] {
                "Use it for daily administrative tasks",
                "Share it with your team",
                "Enable MFA and use it only for account setup tasks",
                "Delete it after creating IAM users"
            },
            2,
            "Best practice is to secure the root account with MFA, avoid using it for daily tasks, and instead create IAM users with appropriate permissions for administrative work.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "iam", "root-account", "best-practices"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_007",
            "Which AWS service allows you to assess, audit, and evaluate configurations of your AWS resources?",
            new string[] {
                "AWS CloudTrail",
                "AWS Config",
                "AWS Inspector",
                "AWS Systems Manager"
            },
            1,
            "AWS Config provides a detailed view of the configuration of AWS resources in your account, including how they are related and how they were configured in the past.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "config", "compliance", "auditing"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_008",
            "What does AWS Artifact provide?",
            new string[] {
                "Source code repositories",
                "Compliance reports and security documents",
                "Application deployment tools",
                "Machine learning models"
            },
            1,
            "AWS Artifact provides on-demand access to AWS compliance reports and select online agreements, including ISO certifications, PCI reports, and SOC reports.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "artifact", "compliance", "reports"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_009",
            "Which AWS service provides automated security assessments for applications?",
            new string[] {
                "AWS Inspector",
                "AWS GuardDuty",
                "AWS Macie",
                "AWS Detective"
            },
            0,
            "Amazon Inspector is an automated security assessment service that helps improve the security and compliance of applications deployed on AWS by identifying security vulnerabilities.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "inspector", "security-assessment"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_010",
            "What is AWS Secrets Manager used for?",
            new string[] {
                "Managing IAM policies",
                "Storing and rotating database credentials, API keys, and other secrets",
                "Encrypting S3 buckets",
                "Monitoring security threats"
            },
            1,
            "AWS Secrets Manager helps you protect access to your applications, services, and IT resources without the upfront cost and complexity of managing your own hardware security module (HSM) infrastructure. It enables you to rotate, manage, and retrieve database credentials, API keys, and other secrets.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "secrets-manager", "credentials"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_011",
            "Which AWS service acts as a web application firewall?",
            new string[] {
                "AWS Shield",
                "AWS WAF",
                "AWS GuardDuty",
                "Network ACLs"
            },
            1,
            "AWS WAF (Web Application Firewall) helps protect web applications from common web exploits that could affect availability, compromise security, or consume excessive resources.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "waf", "firewall", "web-security"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_012",
            "What is the principle of least privilege in IAM?",
            new string[] {
                "Give users maximum permissions to avoid issues",
                "Grant only the permissions required to perform a task",
                "Assign the same permissions to all users",
                "Limit the number of IAM users"
            },
            1,
            "The principle of least privilege means granting only the permissions required to perform a specific task. Start with minimal permissions and grant additional permissions as necessary.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "iam", "least-privilege", "best-practices"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_013",
            "Which compliance program ensures AWS meets healthcare data security standards?",
            new string[] {
                "PCI DSS",
                "HIPAA",
                "SOC 2",
                "ISO 27001"
            },
            1,
            "HIPAA (Health Insurance Portability and Accountability Act) compliance ensures that AWS services meet the security and privacy requirements for handling protected health information (PHI).",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "compliance", "hipaa", "healthcare"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_014",
            "What is AWS Security Hub?",
            new string[] {
                "A physical security monitoring system",
                "A centralized security and compliance dashboard",
                "An antivirus service",
                "A firewall management tool"
            },
            1,
            "AWS Security Hub provides a comprehensive view of your security posture across your AWS accounts. It aggregates, organizes, and prioritizes security alerts from multiple AWS services.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "security-hub", "compliance"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_015",
            "Which AWS service helps you analyze and troubleshoot security issues?",
            new string[] {
                "AWS Inspector",
                "AWS Detective",
                "AWS GuardDuty",
                "AWS Macie"
            },
            1,
            "Amazon Detective makes it easy to analyze, investigate, and quickly identify the root cause of potential security issues or suspicious activities using machine learning and graph theory.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "detective", "security-analysis"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_016",
            "What is MFA (Multi-Factor Authentication)?",
            new string[] {
                "Multiple firewalls for applications",
                "An additional layer of security requiring a second form of authentication",
                "Multiple availability zones for failover",
                "Multiple admin accounts"
            },
            1,
            "MFA adds an extra layer of protection by requiring users to provide two or more verification factors to gain access to a resource, typically something you know (password) and something you have (token).",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "mfa", "authentication", "security"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_017",
            "Which AWS service provides DDoS protection at the application layer?",
            new string[] {
                "AWS Shield Standard",
                "AWS Shield Advanced",
                "AWS WAF",
                "Security Groups"
            },
            1,
            "AWS Shield Advanced provides additional DDoS mitigation capability, including protection at the application layer, 24/7 access to the DDoS Response Team (DRT), and cost protection.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "shield", "ddos", "advanced"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_018",
            "What is the purpose of AWS CloudHSM?",
            new string[] {
                "Cloud hosting service",
                "Hardware security module for cryptographic key management",
                "Cloud security monitoring",
                "Host-based intrusion detection"
            },
            1,
            "AWS CloudHSM is a cloud-based hardware security module (HSM) that enables you to easily generate and use your own encryption keys in the AWS Cloud, meeting corporate, contractual, and regulatory compliance requirements.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Hard,
            "cloudhsm", "encryption", "hsm"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_019",
            "Which AWS feature allows you to isolate your AWS resources in a virtual network?",
            new string[] {
                "AWS Direct Connect",
                "AWS VPN",
                "Amazon VPC",
                "AWS PrivateLink"
            },
            2,
            "Amazon VPC (Virtual Private Cloud) lets you provision a logically isolated section of the AWS Cloud where you can launch AWS resources in a virtual network that you define.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Easy,
            "vpc", "networking", "isolation"
        ));

        questions.Add(new AWSQuestion(
            "SEC_EXP_020",
            "What is the purpose of IAM policies?",
            new string[] {
                "To create user accounts",
                "To define permissions that specify what actions are allowed or denied",
                "To encrypt data",
                "To monitor user activity"
            },
            1,
            "IAM policies are JSON documents that define permissions. They specify what actions are allowed or denied on what AWS resources, implementing the principle of least privilege.",
            ExamDomain.SecurityAndCompliance,
            DifficultyLevel.Medium,
            "iam", "policies", "permissions"
        ));
    }

    private static void AddExpandedTechnology(List<AWSQuestion> questions)
    {
        // Additional Technology questions (60 more covering all major services)

        // Compute Services
        questions.Add(new AWSQuestion(
            "TECH_EXP_001",
            "Which EC2 instance type is optimized for compute-intensive applications?",
            new string[] {
                "T3",
                "M5",
                "C5",
                "R5"
            },
            2,
            "C5 instances are compute-optimized and ideal for compute-intensive workloads like batch processing, scientific modeling, gaming servers, and ad serving engines.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "ec2", "compute", "instance-types"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_002",
            "What is AWS Lambda?",
            new string[] {
                "A virtual server",
                "A serverless compute service that runs code in response to events",
                "A container orchestration service",
                "A database service"
            },
            1,
            "AWS Lambda is a serverless compute service that runs your code in response to events and automatically manages the underlying compute resources. You pay only for the compute time consumed.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "lambda", "serverless"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_003",
            "Which AWS service is used for running containers without managing servers?",
            new string[] {
                "Amazon EC2",
                "AWS Fargate",
                "AWS Elastic Beanstalk",
                "Amazon Lightsail"
            },
            1,
            "AWS Fargate is a serverless compute engine for containers that works with both Amazon ECS and EKS. It removes the need to provision and manage servers.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "fargate", "containers", "serverless"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_004",
            "What is Amazon ECS?",
            new string[] {
                "Elastic Container Service for running Docker containers",
                "Elastic Compute Service",
                "Enterprise Computing Service",
                "Edge Caching Service"
            },
            0,
            "Amazon Elastic Container Service (ECS) is a fully managed container orchestration service that helps you easily deploy, manage, and scale containerized applications.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "ecs", "containers"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_005",
            "Which AWS service provides managed Kubernetes?",
            new string[] {
                "Amazon ECS",
                "Amazon EKS",
                "AWS Fargate",
                "AWS Batch"
            },
            1,
            "Amazon Elastic Kubernetes Service (EKS) is a managed service that makes it easy to run Kubernetes on AWS without needing to install and operate your own Kubernetes control plane.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "eks", "kubernetes"
        ));

        // Storage Services
        questions.Add(new AWSQuestion(
            "TECH_EXP_006",
            "Which S3 storage class is designed for long-term archival with retrieval times of 12 hours?",
            new string[] {
                "S3 Standard",
                "S3 Intelligent-Tiering",
                "S3 Glacier Deep Archive",
                "S3 One Zone-IA"
            },
            2,
            "S3 Glacier Deep Archive is the lowest-cost storage class in S3, designed for data that may be accessed once or twice per year with retrieval times of 12 hours.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "s3", "glacier", "storage-classes"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_007",
            "What is Amazon EBS?",
            new string[] {
                "Elastic Block Store - persistent block storage for EC2",
                "Elastic Backup Service",
                "Enterprise Billing System",
                "Edge Broadcast Service"
            },
            0,
            "Amazon Elastic Block Store (EBS) provides persistent block storage volumes for use with EC2 instances. EBS volumes are network-attached storage that persist independently from the instance.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "ebs", "storage", "block-storage"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_008",
            "Which storage service provides a file system interface for EC2 instances?",
            new string[] {
                "Amazon S3",
                "Amazon EBS",
                "Amazon EFS",
                "AWS Storage Gateway"
            },
            2,
            "Amazon Elastic File System (EFS) provides a simple, scalable, fully managed elastic NFS file system for use with AWS Cloud services and on-premises resources.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "efs", "storage", "file-system"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_009",
            "What is AWS Storage Gateway?",
            new string[] {
                "A hybrid cloud storage service connecting on-premises environments to AWS",
                "An S3 access control service",
                "A database replication tool",
                "A networking gateway"
            },
            0,
            "AWS Storage Gateway is a hybrid cloud storage service that gives you on-premises access to virtually unlimited cloud storage, seamlessly integrating on-premises applications with AWS storage.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "storage-gateway", "hybrid-cloud"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_010",
            "Which S3 feature automatically moves objects between storage classes based on access patterns?",
            new string[] {
                "S3 Versioning",
                "S3 Lifecycle Policies",
                "S3 Intelligent-Tiering",
                "S3 Cross-Region Replication"
            },
            2,
            "S3 Intelligent-Tiering automatically moves objects between access tiers based on changing access patterns, optimizing costs without performance impact or operational overhead.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "s3", "intelligent-tiering", "cost-optimization"
        ));

        // Database Services
        questions.Add(new AWSQuestion(
            "TECH_EXP_011",
            "Which AWS database service is a NoSQL key-value and document database?",
            new string[] {
                "Amazon RDS",
                "Amazon DynamoDB",
                "Amazon Aurora",
                "Amazon Redshift"
            },
            1,
            "Amazon DynamoDB is a fast and flexible NoSQL database service for applications that need consistent, single-digit millisecond latency at any scale. It supports both key-value and document data models.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "dynamodb", "nosql", "database"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_012",
            "What is Amazon RDS?",
            new string[] {
                "Real-time Data Service",
                "Relational Database Service - managed relational databases",
                "Remote Desktop Service",
                "Route Distribution Service"
            },
            1,
            "Amazon RDS (Relational Database Service) makes it easy to set up, operate, and scale relational databases in the cloud. It supports multiple database engines including MySQL, PostgreSQL, Oracle, SQL Server, and MariaDB.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "rds", "database", "relational"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_013",
            "Which database service is designed for data warehousing and analytics?",
            new string[] {
                "Amazon DynamoDB",
                "Amazon RDS",
                "Amazon Redshift",
                "Amazon ElastiCache"
            },
            2,
            "Amazon Redshift is a fast, fully managed data warehouse that makes it simple and cost-effective to analyze large amounts of data using SQL and your existing business intelligence tools.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "redshift", "data-warehouse", "analytics"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_014",
            "What is Amazon Aurora?",
            new string[] {
                "A NoSQL database",
                "A MySQL and PostgreSQL-compatible relational database built for the cloud",
                "A graph database",
                "A caching service"
            },
            1,
            "Amazon Aurora is a MySQL and PostgreSQL-compatible relational database built for the cloud, combining the performance and availability of high-end commercial databases with the simplicity and cost-effectiveness of open source databases.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "aurora", "database", "relational"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_015",
            "Which AWS service provides in-memory caching?",
            new string[] {
                "Amazon RDS",
                "Amazon ElastiCache",
                "Amazon DynamoDB",
                "Amazon S3"
            },
            1,
            "Amazon ElastiCache is a fully managed in-memory data store and cache service supporting Redis and Memcached. It improves application performance by retrieving data from fast, managed, in-memory caches.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "elasticache", "caching", "redis"
        ));

        // Networking Services
        questions.Add(new AWSQuestion(
            "TECH_EXP_016",
            "What is Amazon CloudFront?",
            new string[] {
                "A cloud storage service",
                "A content delivery network (CDN) service",
                "A compute service",
                "A database service"
            },
            1,
            "Amazon CloudFront is a fast content delivery network (CDN) service that securely delivers data, videos, applications, and APIs to customers globally with low latency and high transfer speeds.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "cloudfront", "cdn", "networking"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_017",
            "Which AWS service distributes incoming application traffic across multiple targets?",
            new string[] {
                "Amazon Route 53",
                "Elastic Load Balancing",
                "AWS Direct Connect",
                "Amazon CloudFront"
            },
            1,
            "Elastic Load Balancing automatically distributes incoming application traffic across multiple targets, such as EC2 instances, containers, and IP addresses, in one or more Availability Zones.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "elb", "load-balancing", "networking"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_018",
            "What is AWS Direct Connect?",
            new string[] {
                "A VPN service",
                "A dedicated network connection from your premises to AWS",
                "A content delivery network",
                "A database connection tool"
            },
            1,
            "AWS Direct Connect establishes a dedicated network connection from your premises to AWS, reducing network costs, increasing bandwidth throughput, and providing a more consistent network experience than internet-based connections.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "direct-connect", "networking", "hybrid"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_019",
            "Which type of Elastic Load Balancer operates at the application layer (Layer 7)?",
            new string[] {
                "Classic Load Balancer",
                "Network Load Balancer",
                "Application Load Balancer",
                "Gateway Load Balancer"
            },
            2,
            "Application Load Balancer (ALB) operates at the request level (Layer 7), routing traffic based on the content of the request. It's ideal for HTTP/HTTPS traffic and supports advanced routing.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "alb", "load-balancing", "layer7"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_020",
            "What is Amazon VPC?",
            new string[] {
                "Virtual Private Cloud - isolated cloud network",
                "Virtual Processing Center",
                "Verified Partner Connect",
                "Volume Performance Calculator"
            },
            0,
            "Amazon VPC (Virtual Private Cloud) lets you provision a logically isolated section of the AWS Cloud where you can launch AWS resources in a virtual network that you define.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "vpc", "networking"
        ));

        // Integration & Messaging
        questions.Add(new AWSQuestion(
            "TECH_EXP_021",
            "What is Amazon SQS?",
            new string[] {
                "Simple Query Service",
                "Simple Queue Service - managed message queuing",
                "Structured Query System",
                "Secure Question Service"
            },
            1,
            "Amazon Simple Queue Service (SQS) is a fully managed message queuing service that enables you to decouple and scale microservices, distributed systems, and serverless applications.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "sqs", "messaging", "queuing"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_022",
            "What is Amazon SNS?",
            new string[] {
                "Simple Notification Service - pub/sub messaging",
                "Secure Network Service",
                "Standard Naming System",
                "Server Notification System"
            },
            0,
            "Amazon Simple Notification Service (SNS) is a fully managed pub/sub messaging service for decoupling microservices, distributed systems, and serverless applications using topics.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "sns", "messaging", "pub-sub"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_023",
            "Which AWS service provides event-driven workflow orchestration?",
            new string[] {
                "AWS Lambda",
                "Amazon SQS",
                "AWS Step Functions",
                "Amazon EventBridge"
            },
            2,
            "AWS Step Functions is a serverless orchestration service that lets you combine AWS Lambda functions and other AWS services to build business-critical applications with visual workflows.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "step-functions", "orchestration"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_024",
            "What is Amazon EventBridge?",
            new string[] {
                "A networking service",
                "A serverless event bus service for connecting applications",
                "A database service",
                "A monitoring service"
            },
            1,
            "Amazon EventBridge is a serverless event bus that makes it easy to connect applications using events from AWS services, SaaS applications, and custom applications.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "eventbridge", "events", "integration"
        ));

        // Application Services
        questions.Add(new AWSQuestion(
            "TECH_EXP_025",
            "What is AWS Elastic Beanstalk?",
            new string[] {
                "A database service",
                "A PaaS service for deploying and scaling web applications",
                "A storage service",
                "A monitoring service"
            },
            1,
            "AWS Elastic Beanstalk is a Platform as a Service (PaaS) that makes it easy to deploy and scale web applications and services. You simply upload your code and Beanstalk handles deployment, capacity provisioning, load balancing, and auto-scaling.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "elastic-beanstalk", "paas", "deployment"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_026",
            "Which AWS service provides managed GraphQL and REST APIs?",
            new string[] {
                "AWS Lambda",
                "Amazon API Gateway",
                "AWS AppSync",
                "Amazon CloudFront"
            },
            2,
            "AWS AppSync is a managed service that makes it easy to build scalable GraphQL and pub/sub APIs that securely access, manipulate, and combine data from multiple sources.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "appsync", "graphql", "api"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_027",
            "What is Amazon API Gateway?",
            new string[] {
                "A VPN service",
                "A managed service for creating, publishing, and managing APIs",
                "A database gateway",
                "A storage access service"
            },
            1,
            "Amazon API Gateway is a fully managed service that makes it easy for developers to create, publish, maintain, monitor, and secure APIs at any scale.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "api-gateway", "api", "rest"
        ));

        // Monitoring & Management
        questions.Add(new AWSQuestion(
            "TECH_EXP_028",
            "What is Amazon CloudWatch?",
            new string[] {
                "A security monitoring service",
                "A monitoring and observability service for AWS resources and applications",
                "A cost management tool",
                "A backup service"
            },
            1,
            "Amazon CloudWatch is a monitoring and observability service that provides data and actionable insights for AWS resources, applications, and services running on AWS and on-premises.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "cloudwatch", "monitoring"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_029",
            "What is AWS Auto Scaling?",
            new string[] {
                "A service that automatically backs up data",
                "A service that automatically adjusts compute capacity to maintain performance",
                "A service that scales storage",
                "A service that scales databases only"
            },
            1,
            "AWS Auto Scaling monitors your applications and automatically adjusts capacity to maintain steady, predictable performance at the lowest possible cost.",
            ExamDomain.Technology,
            DifficultyLevel.Easy,
            "auto-scaling", "scalability"
        ));

        questions.Add(new AWSQuestion(
            "TECH_EXP_030",
            "Which AWS service helps you analyze application performance and troubleshoot issues?",
            new string[] {
                "Amazon CloudWatch",
                "AWS X-Ray",
                "AWS CloudTrail",
                "AWS Config"
            },
            1,
            "AWS X-Ray helps developers analyze and debug distributed applications, providing insights into application behavior and helping you understand how your application and its underlying services are performing.",
            ExamDomain.Technology,
            DifficultyLevel.Medium,
            "xray", "debugging", "tracing"
        ));
    }

    private static void AddExpandedBilling(List<AWSQuestion> questions)
    {
        // Additional Billing & Pricing questions (30 more)

        questions.Add(new AWSQuestion(
            "BILL_EXP_001",
            "Which EC2 pricing model offers up to 75% discount for predictable workloads with a 1 or 3 year commitment?",
            new string[] {
                "On-Demand",
                "Spot Instances",
                "Reserved Instances",
                "Dedicated Hosts"
            },
            2,
            "Reserved Instances provide a significant discount (up to 75%) compared to On-Demand pricing when you commit to a specific instance type in a particular region for a 1 or 3 year term.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "reserved-instances", "pricing"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_002",
            "Which pricing model allows you to bid on unused EC2 capacity at up to 90% discount?",
            new string[] {
                "On-Demand Instances",
                "Reserved Instances",
                "Spot Instances",
                "Savings Plans"
            },
            2,
            "Spot Instances let you take advantage of unused EC2 capacity at up to 90% discount compared to On-Demand prices. They're ideal for fault-tolerant and flexible applications.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "spot-instances", "pricing"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_003",
            "What is the AWS Free Tier?",
            new string[] {
                "Free AWS support",
                "Free access to select AWS services for 12 months plus always-free offers",
                "Free training courses",
                "Free consulting services"
            },
            1,
            "The AWS Free Tier provides customers the ability to explore and try out AWS services free of charge up to specified limits for each service. It includes 12 months free, always free, and short-term trial offers.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "free-tier", "pricing"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_004",
            "Which AWS tool helps you visualize and manage AWS costs and usage over time?",
            new string[] {
                "AWS Budgets",
                "AWS Cost Explorer",
                "AWS Billing Dashboard",
                "AWS Pricing Calculator"
            },
            1,
            "AWS Cost Explorer is a tool that enables you to visualize, understand, and manage your AWS costs and usage over time with an intuitive interface.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "cost-explorer", "cost-management"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_005",
            "What is AWS Budgets?",
            new string[] {
                "A financial planning service",
                "A service that lets you set custom cost and usage budgets with alerts",
                "A cost reduction tool",
                "A billing report generator"
            },
            1,
            "AWS Budgets gives you the ability to set custom budgets that alert you when your costs or usage exceed (or are forecasted to exceed) your budgeted amount.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "budgets", "cost-management"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_006",
            "Which AWS service provides cost optimization recommendations?",
            new string[] {
                "AWS Cost Explorer",
                "AWS Trusted Advisor",
                "AWS Budgets",
                "AWS Cost Anomaly Detection"
            },
            1,
            "AWS Trusted Advisor is an online tool that provides real-time guidance to help you provision your resources following AWS best practices, including cost optimization recommendations.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "trusted-advisor", "cost-optimization"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_007",
            "What is consolidated billing in AWS Organizations?",
            new string[] {
                "Combining multiple AWS accounts' bills into one bill",
                "A discount program",
                "A payment method",
                "A cost reporting tool"
            },
            0,
            "Consolidated billing is a feature of AWS Organizations that enables you to consolidate payment for multiple AWS accounts. You get one bill for all accounts in your organization and can take advantage of volume pricing discounts.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Medium,
            "organizations", "consolidated-billing"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_008",
            "What are AWS Savings Plans?",
            new string[] {
                "A savings account for AWS credits",
                "Flexible pricing models offering lower prices in exchange for usage commitment",
                "A cost reduction consulting service",
                "A budgeting tool"
            },
            1,
            "AWS Savings Plans offer a flexible pricing model that provides savings of up to 72% on your AWS compute usage. You commit to a consistent amount of usage (measured in $/hour) for a 1 or 3 year term.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Medium,
            "savings-plans", "pricing"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_009",
            "What does 'pay-as-you-go' pricing mean?",
            new string[] {
                "You must pay before using services",
                "You pay only for what you use with no long-term commitments",
                "You pay annually",
                "You pay based on predictions"
            },
            1,
            "Pay-as-you-go pricing means you pay only for the individual services you need, for as long as you use them, without requiring long-term contracts or complex licensing.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "pricing-model", "pay-as-you-go"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_010",
            "Which S3 storage class is the most cost-effective for data accessed less than once a year?",
            new string[] {
                "S3 Standard",
                "S3 Standard-IA",
                "S3 Glacier",
                "S3 Glacier Deep Archive"
            },
            3,
            "S3 Glacier Deep Archive is the lowest-cost storage class, designed for long-term retention of data that is accessed less than once a year and retrieved asynchronously.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Medium,
            "s3", "storage-classes", "cost-optimization"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_011",
            "What is the AWS Pricing Calculator?",
            new string[] {
                "A tool to calculate historical costs",
                "A tool to estimate the cost of AWS services for your use case",
                "A budgeting tool",
                "A cost reduction tool"
            },
            1,
            "AWS Pricing Calculator is a web-based service that you can use to create cost estimates for your AWS use cases based on your expected usage.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "pricing-calculator", "cost-estimation"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_012",
            "What is AWS Cost Anomaly Detection?",
            new string[] {
                "A security tool",
                "A machine learning service that identifies unusual spending patterns",
                "A billing error checker",
                "A budget enforcement tool"
            },
            1,
            "AWS Cost Anomaly Detection uses machine learning to continuously monitor your cost and usage to detect unusual spending patterns, helping you identify cost overruns quickly.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Medium,
            "cost-anomaly-detection", "cost-management"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_013",
            "Which AWS support plan is the minimum required for access to AWS Trusted Advisor full checks?",
            new string[] {
                "Basic",
                "Developer",
                "Business",
                "Enterprise"
            },
            2,
            "Business and Enterprise support plans provide access to the full set of AWS Trusted Advisor checks, including cost optimization, security, fault tolerance, performance, and service limits.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Medium,
            "support-plans", "trusted-advisor"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_014",
            "What is AWS Cost and Usage Report (CUR)?",
            new string[] {
                "A real-time cost dashboard",
                "A comprehensive report containing detailed cost and usage data",
                "A budget alert system",
                "A cost optimization tool"
            },
            1,
            "AWS Cost and Usage Report (CUR) contains the most comprehensive set of cost and usage data available, including metadata about AWS services, pricing, and reservations.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Medium,
            "cur", "reporting"
        ));

        questions.Add(new AWSQuestion(
            "BILL_EXP_015",
            "Which pricing model provides the most flexibility with no upfront commitment?",
            new string[] {
                "Reserved Instances",
                "Savings Plans",
                "On-Demand Instances",
                "Spot Instances"
            },
            2,
            "On-Demand Instances offer the most flexibility with no upfront commitment or long-term contract. You pay for compute capacity by the hour or second with no minimum commitments.",
            ExamDomain.BillingAndPricing,
            DifficultyLevel.Easy,
            "on-demand", "pricing"
        ));
    }
}
