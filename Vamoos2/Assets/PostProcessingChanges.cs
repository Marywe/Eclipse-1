using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingChanges : MonoBehaviour
{
    PostProcessVolume processVolume;
    DepthOfField depth;

    float originalFL = 156;
    float originalFD = 35;
    float originalA = 2;
    // Start is called before the first frame update
    void Start()
    {
        processVolume = GetComponent<PostProcessVolume>();
        processVolume.profile.TryGetSettings<DepthOfField>(out depth);

        SetOriginalParameters();
    }

    // Update is called once per frame
    void Update()
    {
        if(Controlador.instance.dondeEstas == Controlador.DondeEstas.sLab || 
           Controlador.instance.dondeEstas == Controlador.DondeEstas.sBossRush || 
           Controlador.instance.dondeEstas == Controlador.DondeEstas.sArriba ||
           Controlador.instance.dondeEstas == Controlador.DondeEstas.LabDer)
        {
            depth.enabled.value = false;

        }
        else if(Controlador.instance.dondeEstas == Controlador.DondeEstas.LabIzq)
        {
            depth.enabled.value = true;
            depth.focusDistance.value = 16;
            depth.aperture.value = 5.1f;
            depth.focalLength.value = 146;
        }
        else
        {
            depth.enabled.value = true;
            SetOriginalParameters();
        }
    }

    void SetOriginalParameters()
    {
        depth.focalLength.value = originalFL;
        depth.focusDistance.value = originalFD;
        depth.aperture.value = originalA;
    }
}
