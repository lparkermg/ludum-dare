using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _growParent;

    [SerializeField]
    private GameObject _growChild;

    [SerializeField]
    private GameObject[] _growStages;

    private Material _crystalMaterial;

    [SerializeField]
    private ParticleSystem _particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupPlant(Material material, Color particleColor, int initialStage)
    {
        _crystalMaterial = material;

        var main = _particles.main;
        main.startColor = particleColor;

        UpdatePlant(initialStage);
    }

    public void UpdatePlant(int newStage)
    {
        Destroy(_growChild);

        _growChild = GameObject.Instantiate(_growStages[newStage], _growParent);
        _growChild.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        _growChild.GetComponent<MeshRenderer>().materials = new[] { _crystalMaterial };
    }

    public void Collecting()
    {
        _particles.Emit(20);
    }
}
