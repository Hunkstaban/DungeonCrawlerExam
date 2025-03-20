using System;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthbar : MonoBehaviour
{
    private Slider slider;
    private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    private void Start()
    {
        slider = GetComponent<Slider>();
        camera = Camera.main;

        if (!slider)
        {
            Debug.LogError("Slider component not found.");
        }
        
        if (!camera)
        {
            Debug.LogError("Main Camera not found. Ensure your camera has the 'MainCamera' tag.");
        }
    }

    private void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = target.position + offset;
    }

    public void DestroyHealthbar()
    {
        Destroy(this);
    }

    private void OnDrawGizmos()
    {
        if (target == null) return;
        
        Gizmos.color = Color.green;
        Vector3 healthbarPosition = target.position + offset;

        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform)
        {
            float width = rectTransform.rect.width * transform.lossyScale.x;
            float height = rectTransform.rect.height * transform.lossyScale.y;
            float depth = 0.1f;
            Vector3 size = new Vector3(width, height, depth);
            Gizmos.DrawWireCube(healthbarPosition, size);
        }
        else
        {
            Gizmos.DrawWireCube(healthbarPosition, new Vector3(1f, 0.3f, 0.1f));
        }
    }
}
