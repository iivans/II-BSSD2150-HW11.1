using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseController : MonoBehaviour
{
    [SerializeField]
    Transform projectileSpawnPoint;

    private int treesLeft = 3; // Number of trees that can be shot
    private float rotationSpeed = 100f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && treesLeft > 0)
        {
            ShootProjectile();
        }

        // Rotate the house around its forward axis (z-axis) based on input
        float rotationAmount = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationAmount);
    }

    void ShootProjectile()
    {
        // Find the HUDTrees panel
        GameObject hudTreesPanel = GameObject.Find("HUDTrees");

        // Check if the panel is found
        if (hudTreesPanel != null)
        {
            // Check if the panel has any child objects
            if (hudTreesPanel.transform.childCount > 0)
            {
                // Destroy the last child object of the panel
                Destroy(hudTreesPanel.transform.GetChild(hudTreesPanel.transform.childCount - 1).gameObject);
            }
            else
            {
                Debug.LogWarning("HUDTrees panel has no children.");
            }
        }
        else
        {
            Debug.LogWarning("HUDTrees panel not found.");
        }

        // Load the projectile prefab from the Resources folder
        GameObject projectilePrefab = Resources.Load<GameObject>("Projectile");

        // Check if the projectile prefab is loaded successfully
        if (projectilePrefab != null)
        {
            // Instantiate the projectile at the spawn point position and rotation
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            // Decrease the count of trees left
            treesLeft--;
        }
        else
        {
            Debug.LogError("Failed to load projectile prefab from Resources folder.");
        }
    }
}