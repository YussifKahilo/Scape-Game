using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePower : MonoBehaviour , PlayerPower
{

    [SerializeField] Vector3[] scales;
    [SerializeField] Vector3[] effectScales;
    [SerializeField] GameObject effectColor;

    [SerializeField] ParticleSystem scaleEffect;

    bool canChangeScale = true;
    bool scaleChanging = false;
    int scaleLevel = 1;

    Vector3 currentScale;
    Vector3 newScale;

    Vector3 currentEffectScale;
    Vector3 newEffectScale;


    [SerializeField]float lerpDuration = 0.25f;
    float timeElapsed = 0;

    public void Power1()
    {
        ChangeScale(1);
    }

    public void Power2()
    {
        ChangeScale(-1);
    }

    void ChangeScale(int scale)
    {
        if (scaleLevel  + scale == -1 || scaleLevel + scale == 3 || scaleChanging || !canChangeScale)
        {
            return;
        }

        currentScale = scales[scaleLevel];
        currentEffectScale = effectScales[scaleLevel];

        scaleLevel += scale;

        scaleEffect.Play();

        newScale = scales[scaleLevel];
        newEffectScale = effectScales[scaleLevel];

        GetComponent<PlayerController>().JumpForce = scaleLevel == 0 ? 2 : scaleLevel == 1 ? 5 : 13;

        GetComponent<Rigidbody>().mass = scaleLevel == 0 ? 0.75f : scaleLevel == 1 ? 1f : 2f;
        scaleChanging = true;
    }
    
    void Update()
    {
        if (scaleChanging)
        {
            if (timeElapsed < lerpDuration)
            {
                transform.localScale = Vector3.Lerp(currentScale, newScale, timeElapsed / lerpDuration);

                scaleEffect.transform.localScale = Vector3.Lerp(currentEffectScale, newEffectScale, timeElapsed / lerpDuration);

                timeElapsed += Time.deltaTime;
            }
            else
            {
                transform.localScale = newScale;
                scaleEffect.transform.localScale = newEffectScale;
                scaleChanging = false;
                timeElapsed = 0;
            }
        }
        //effectColor.SetActive(scaleChanging);
        GetComponent<PlayerController>().Speed = Mathf.Ceil( (transform.localScale.y + 0.65f ) * 2 );
    }

    public void SetCanScaleState(bool state)
    {
        canChangeScale = state;
    }

}
