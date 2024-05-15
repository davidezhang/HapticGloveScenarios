
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Visualizer : MonoBehaviour
{
    private Light _light;

    public short BoneId;

    public OVRHand.Hand hand;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        _light.intensity = 0.05f;
        _light.range = 0.1f;
        _light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnForceChanged(float force)
    {
        _light.color = Color.Lerp(UnityEngine.Color.blue, UnityEngine.Color.red, force / 150f);
        _light.enabled = true;
    }

    public void OnForceDisable()
    {
        _light.enabled = false;
    }
}
