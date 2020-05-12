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
           Controlador.instance.dondeEstas == Controlador.DondeEstas.LabDer ||
           Controlador.instance.dondeEstas == Controlador.DondeEstas.sBoss ||
           Controlador.instance.dondeEstas == Controlador.DondeEstas.Ascensor)
        {
            depth.enabled.value = false;

        }
        else if(Controlador.instance.dondeEstas == Controlador.DondeEstas.LabIzq)
        {
            depth.enabled.value = true;
            depth.focalLength.value = 35;
            depth.focusDistance.value = 20;
            depth.aperture.value = 0.18f;
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
