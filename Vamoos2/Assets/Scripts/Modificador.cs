using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modificador : MonoBehaviour
{
    private string tagMod;
    // Start is called before the first frame update
    void Start()
    {
        tagMod = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (tagMod)
            {
                case "Vida":
                    ModificarVida(other.gameObject);
                    break;
                case "Recarga":
                    ModificarRecarga(other.gameObject);
                    break;
                case "Velocidad":
                    ModificarVel(other.gameObject);
                    break;
                case "Ataque":
                    ModificarDano(other.gameObject);
                    break;
            }

            Destroy(gameObject);
        }

    }

    void ModificarDano(GameObject other)
    {
        other.GetComponent<Jugador>().dano += 1;
    }
    void ModificarVel(GameObject other)
    {
        other.GetComponent<Jugador>().speed += 1;
    }
    void ModificarRecarga(GameObject other)
    {
        //other.GetComponent<Jugador>().dano += .5f;
    }
    void ModificarVida(GameObject other)
    {
        other.GetComponent<Jugador>().line_sc.ModificarVidaMax();
    }
}
