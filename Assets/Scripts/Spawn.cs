using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to spawn
    public Button spawnButton; // The UI Button to trigger spawning
    public Button destroyAllButton; // The UI Button to destroy all spawned objects
    public Slider colorSlider; // The UI Slider to change the color of spawned objects

    private List<GameObject> spawnedObjects = new List<GameObject>(); // List to track spawned objects

    void Start()
    {
        // Ensure the buttons, slider, and prefab are assigned
        if (spawnButton == null)
        {
            Debug.LogError("Spawn Button is not assigned!");
            return;
        }

        if (destroyAllButton == null)
        {
            Debug.LogError("Destroy All Button is not assigned!");
            return;
        }

        if (colorSlider == null)
        {
            Debug.LogError("Color Slider is not assigned!");
            return;
        }

        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab to Spawn is not assigned!");
            return;
        }

        // Add listeners to the buttons and slider
        spawnButton.onClick.AddListener(SpawnPrefab);
        destroyAllButton.onClick.AddListener(DestroyAllObjects);
        colorSlider.onValueChanged.AddListener(ChangeObjectColors);
    }

    void SpawnPrefab()
    {
        // Spawn the prefab at a default position (e.g., world origin)
        GameObject newObject = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity);
        spawnedObjects.Add(newObject); // Add the new object to the list

        // Set the initial color based on the slider value
        ChangeObjectColor(newObject, colorSlider.value);
    }

    void DestroyAllObjects()
    {
        // Destroy all objects in the list
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null) // Check if the object still exists
            {
                Destroy(obj);
            }
        }

        // Clear the list
        spawnedObjects.Clear();
    }

    void ChangeObjectColors(float sliderValue)
    {
        // Update the color of all spawned objects based on the slider value
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null) // Check if the object still exists
            {
                ChangeObjectColor(obj, sliderValue);
            }
        }
    }

    void ChangeObjectColor(GameObject obj, float sliderValue)
    {
        // Get the Renderer component of the object
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Change the color based on the slider value
            Color newColor = Color.HSVToRGB(sliderValue, 1f, 1f); // Use HSV for smooth color transitions
            renderer.material.color = newColor;
        }
    }
}
