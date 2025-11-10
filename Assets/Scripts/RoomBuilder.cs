using UnityEngine;

/// <summary>
/// Procedurally builds rooms at runtime
/// </summary>
public class RoomBuilder : MonoBehaviour
{
    [Header("Room Configuration")]
    public int roomIndex = 0;
    public Vector2 roomSize = new Vector2(25, 20);
    public Color wallColor = new Color(0.55f, 0.27f, 0.07f);
    public string roomName = "Room";

    [Header("Prefabs")]
    public GameObject collectiblePrefab;
    public GameObject doorPrefab;

    [Header("Materials")]
    public Material floorMaterial;
    public Material wallMaterial;
    public Material ceilingMaterial;

    void Start()
    {
        BuildRoom();
    }

    public void BuildRoom()
    {
        float w = roomSize.x;
        float d = roomSize.y;

        // Create floor
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Floor";
        floor.transform.SetParent(transform);
        floor.transform.localPosition = new Vector3(0, 0, 0);
        floor.transform.localScale = new Vector3(w, 0.5f, d);
        if (floorMaterial != null)
        {
            floor.GetComponent<Renderer>().material = floorMaterial;
        }
        else
        {
            floor.GetComponent<Renderer>().material.color = Color.gray;
        }

        // Create ceiling
        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ceiling.name = "Ceiling";
        ceiling.transform.SetParent(transform);
        ceiling.transform.localPosition = new Vector3(0, 6, 0);
        ceiling.transform.localScale = new Vector3(w, 0.5f, d);
        if (ceilingMaterial != null)
        {
            ceiling.GetComponent<Renderer>().material = ceilingMaterial;
        }
        else
        {
            ceiling.GetComponent<Renderer>().material.color = Color.gray * 0.5f;
        }
        Destroy(ceiling.GetComponent<Collider>()); // No collision for ceiling

        // Create walls
        CreateWall("LeftWall", new Vector3(-w / 2, 3, 0), new Vector3(0.5f, 6, d), wallColor);
        CreateWall("RightWall", new Vector3(w / 2, 3, 0), new Vector3(0.5f, 6, d), wallColor);
        CreateWall("BackWall", new Vector3(0, 3, -d / 2), new Vector3(w, 6, 0.5f), wallColor);

        // Front walls (with door gap)
        CreateWall("FrontWallLeft", new Vector3(-w / 4 - 1.5f, 3, d / 2), new Vector3(w / 2 - 3, 6, 0.5f), wallColor);
        CreateWall("FrontWallRight", new Vector3(w / 4 + 1.5f, 3, d / 2), new Vector3(w / 2 - 3, 6, 0.5f), wallColor);

        // Create room title
        CreateRoomTitle();

        // Spawn collectibles
        SpawnCollectibles();

        // Spawn door
        SpawnDoor();

        // Add lights
        AddLighting();
    }

    void CreateWall(string name, Vector3 position, Vector3 scale, Color color)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = name;
        wall.transform.SetParent(transform);
        wall.transform.localPosition = position;
        wall.transform.localScale = scale;

        if (wallMaterial != null)
        {
            wall.GetComponent<Renderer>().material = wallMaterial;
        }

        wall.GetComponent<Renderer>().material.color = color;
    }

    void CreateRoomTitle()
    {
        // Create a 3D text object (simplified)
        GameObject titleObj = new GameObject("RoomTitle");
        titleObj.transform.SetParent(transform);
        titleObj.transform.localPosition = new Vector3(0, 5, -roomSize.y / 4);

        // Add TextMesh component
        TextMesh textMesh = titleObj.AddComponent<TextMesh>();
        textMesh.text = roomName;
        textMesh.fontSize = 50;
        textMesh.color = Color.yellow;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.characterSize = 0.1f;
    }

    void SpawnCollectibles()
    {
        if (collectiblePrefab == null)
        {
            // Create default collectible
            collectiblePrefab = CreateDefaultCollectible();
        }

        for (int i = 0; i < 3; i++)
        {
            float angle = (i / 3f) * Mathf.PI * 2f;
            float radius = roomSize.x / 4f;

            Vector3 pos = new Vector3(
                Mathf.Cos(angle) * radius,
                2f,
                Mathf.Sin(angle) * radius
            );

            GameObject collectible = Instantiate(collectiblePrefab, transform);
            collectible.transform.localPosition = pos;
            collectible.name = $"Artifact_{i + 1}";

            var collectibleScript = collectible.GetComponent<Collectible>();
            if (collectibleScript != null)
            {
                collectibleScript.artifactName = $"Artifact {i + 1}";
            }
        }
    }

    void SpawnDoor()
    {
        if (doorPrefab == null)
        {
            doorPrefab = CreateDefaultDoor();
        }

        GameObject door = Instantiate(doorPrefab, transform);
        door.transform.localPosition = new Vector3(0, 2.5f, roomSize.y / 2);
        door.name = "Door";

        // Setup door script with questions
        var doorScript = door.GetComponent<Door>();
        if (doorScript != null)
        {
            doorScript.roomIndex = roomIndex;
            SetupQuestions(doorScript);
        }
    }

    void SetupQuestions(Door door)
    {
        // Room-specific questions
        CSMQuestion[] questionsData = GetQuestionsForRoom(roomIndex);

        door.questions.Clear();
        foreach (var q in questionsData)
        {
            door.questions.Add(q);
        }
    }

    CSMQuestion[] GetQuestionsForRoom(int room)
    {
        // Sample questions - customize per room
        switch (room)
        {
            case 0: // Foundation
                return new CSMQuestion[]
                {
                    new CSMQuestion
                    {
                        question = "What are the three pillars of Scrum?",
                        options = new string[]
                        {
                            "A) Planning, Execution, Review",
                            "B) Transparency, Inspection, Adaptation",
                            "C) Communication, Collaboration, Commitment",
                            "D) Vision, Planning, Delivery"
                        },
                        correctIndex = 1,
                        explanation = "The three pillars of Scrum are Transparency, Inspection, and Adaptation."
                    },
                    new CSMQuestion
                    {
                        question = "How long should a Sprint be?",
                        options = new string[]
                        {
                            "A) No more than one month",
                            "B) Exactly 2 weeks",
                            "C) Between 1-3 months",
                            "D) As long as needed"
                        },
                        correctIndex = 0,
                        explanation = "Sprints should be no more than one month long."
                    }
                };

            case 1: // Guardians
                return new CSMQuestion[]
                {
                    new CSMQuestion
                    {
                        question = "Who is responsible for maximizing product value?",
                        options = new string[]
                        {
                            "A) Scrum Master",
                            "B) Product Owner",
                            "C) Development Team",
                            "D) Stakeholders"
                        },
                        correctIndex = 1,
                        explanation = "The Product Owner is responsible for maximizing product value."
                    },
                    new CSMQuestion
                    {
                        question = "Who removes impediments?",
                        options = new string[]
                        {
                            "A) Product Owner",
                            "B) Development Team",
                            "C) Scrum Master",
                            "D) Manager"
                        },
                        correctIndex = 2,
                        explanation = "The Scrum Master removes impediments to the team's progress."
                    }
                };

            default:
                return new CSMQuestion[0];
        }
    }

    void AddLighting()
    {
        // Add some point lights for ambiance
        for (int i = 0; i < 4; i++)
        {
            GameObject lightObj = new GameObject($"Light_{i}");
            lightObj.transform.SetParent(transform);

            float x = (i % 2 == 0) ? -roomSize.x / 3 : roomSize.x / 3;
            float z = (i < 2) ? -roomSize.y / 3 : roomSize.y / 3;

            lightObj.transform.localPosition = new Vector3(x, 5, z);

            Light light = lightObj.AddComponent<Light>();
            light.type = LightType.Point;
            light.range = 15f;
            light.intensity = 1f;
            light.color = wallColor;
        }
    }

    GameObject CreateDefaultCollectible()
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.localScale = Vector3.one * 0.8f;
        obj.GetComponent<Renderer>().material.color = Color.yellow;
        obj.AddComponent<Collectible>();
        return obj;
    }

    GameObject CreateDefaultDoor()
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.localScale = new Vector3(4, 5, 0.3f);
        obj.GetComponent<Renderer>().material.color = new Color(0.55f, 0.27f, 0.07f);
        obj.AddComponent<Door>();
        return obj;
    }
}
