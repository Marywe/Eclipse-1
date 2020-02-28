using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script que controla las características de los chips para abrir la puerta final
/// </summary>
public class Chip : MonoBehaviour
{
    Transform tr;
    public Transform cam;
    private bool PuertaGrandeAbierta = false;
    private void Start()
    {
        tr = gameObject.transform.GetChild(0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Controlador.instance.AddChips();
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (Controlador.instance.chips == 4)
        {
            PuertaGrandeAbierta = true;
        }

        #region Camera
        tr.rotation = Quaternion.LookRotation(transform.position - cam.position);
        Vector3 look;
        look.x = transform.position.x - cam.position.x;
        look.y = 0;
        look.z = transform.position.z - cam.position.z;
        transform.rotation = Quaternion.LookRotation(look);
        #endregion
    }
}
