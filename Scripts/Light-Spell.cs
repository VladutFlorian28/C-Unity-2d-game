using UnityEngine;


public class LightControl2D : MonoBehaviour
{
    // Numele GameObject-ului care conține componenta Light2D
    public string lightObjectName = "Character-Light";

    // Variabile pentru a reține componenta Light2D și raza inițială a luminii
    private UnityEngine.Rendering.Universal.Light2D characterLight;
    private float initialOuterRadius;

    // Durata în secunde pentru care lumina va fi mărită
    public float boostDuration = 3.0f;

    // Factorul de creștere al razei (300% înseamnă un factor de 3)
    public float radiusBoostFactor = 3.0f;

    // Variabilă pentru a verifica dacă tasta Q a fost deja apăsată
    private bool qPressed = false;

    // Timpul minim în secunde între apăsări succesive ale tastei Q
    public float cooldownTime = 10.0f;
    private float lastPressTime = -9999.0f; // Inițializăm cu o valoare foarte mică pentru a permite prima apăsare

    void Start()
    {
        // Găsește GameObject-ul cu numele specificat și componenta Light2D a acestuia
        GameObject lightObject = GameObject.Find(lightObjectName);
        if (lightObject != null)
        {
            characterLight = lightObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
            if (characterLight != null)
            {
                // Salvează valoarea inițială a razei externe a luminii
                initialOuterRadius = characterLight.pointLightOuterRadius;
            }
            else
            {
                Debug.LogError("Light2D component not found on " + lightObjectName);
            }
        }
        else
        {
            Debug.LogError("GameObject with name " + lightObjectName + " not found");
        }
    }

    void Update()
    {
        // Verifică dacă tasta Q a fost apăsată și nu a mai fost apăsată înainte și a trecut cooldownTime de la ultima apăsare
        if (Input.GetKeyDown(KeyCode.Q) && !qPressed && Time.time >= lastPressTime + cooldownTime && characterLight != null)
        {
            // Mărește raza externă a luminii cu factorul specificat
            characterLight.pointLightOuterRadius *= radiusBoostFactor;

            // Setează qPressed la true pentru a indica că tasta Q a fost apăsată
            qPressed = true;

            // Actualizează timpul ultimei apăsări a tastei Q
            lastPressTime = Time.time;

            // Pornim timer-ul pentru a reduce raza luminii la valoarea inițială
            Invoke(nameof(ResetLightRadius), boostDuration);
        }
    }

    // Funcție apelată pentru a reseta raza luminii la valoarea inițială după boostDuration secunde
    void ResetLightRadius()
    {
        characterLight.pointLightOuterRadius = initialOuterRadius;
        qPressed = false; // Permite reapăsarea tastă Q după resetarea razei
    }
}
