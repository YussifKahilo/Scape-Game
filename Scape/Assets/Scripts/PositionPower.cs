using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class PositionPower : MonoBehaviour, PlayerPower
{
    [SerializeField] ParticleSystem positionSaveEffect;
    [SerializeField] GameObject effectColor;
    [SerializeField] ParticleSystem traceEffect;
    [SerializeField] GameObject modelMesh;

    Vector3 startPosition;
    Quaternion lookDir , camLookDir;

    [SerializeField]float lerpDuration = 0.5f;
    float timeElapsed = 0;

    GameObject currentSaveEffect;

    bool positionSaved = false, returningEffectPlaying = false;

    public void Power1()
    {
        if (returningEffectPlaying)
        {
            return;
        }

        if (positionSaved)
        {
            startPosition = transform.position;

            Vector3 dir = (currentSaveEffect.transform.position - transform.position).normalized;
            lookDir = Quaternion.LookRotation(dir);
            lookDir.z = 0;
            lookDir.x = 0;

            dir = (currentSaveEffect.transform.position - GetComponent<PlayerController>().CameraHolder.transform.position);
            camLookDir.y = 0;
            camLookDir.z = 0;


            modelMesh.SetActive(false);
            traceEffect.gameObject.SetActive(true);
            traceEffect.Play();
            effectColor.SetActive(true);
            GetComponent<PlayerController>().enabled = false;
            returningEffectPlaying = true;
            GetComponent<CapsuleCollider>().isTrigger = true;
            SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().clip = SoundManager.instance.positionPowerTrace;
            SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
        else
        {
            currentSaveEffect = Instantiate(positionSaveEffect,
                    transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
            
            currentSaveEffect.GetComponent<ParticleSystem>().Play();
            positionSaved = true;

            SoundManager.instance.GetComponent<AudioSource>().clip = SoundManager.instance.positionPowerStill;
            SoundManager.instance.GetComponent<AudioSource>().Play();
            SoundManager.instance.PlaySound(SoundManager.instance.positionPowerElectric);

            StartCoroutine(ElectricSound());

        }
    }

    IEnumerator ElectricSound()
    {
        yield return new WaitForSeconds(2);
        if (positionSaved)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.positionPowerElectric);
            StartCoroutine(ElectricSound());
        }
    }

    void EndPower()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.positionPowerEnd);

        SoundManager.instance.GetComponent<AudioSource>().Stop();
        SoundManager.instance.GetComponent<AudioSource>().clip = null;

        SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().Stop();
        SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().clip = null;

        effectColor.SetActive(false);
        returningEffectPlaying = false;
        positionSaved = false;
        modelMesh.SetActive(true);
        traceEffect.gameObject.SetActive(false);
        traceEffect.Stop();
        GetComponent<CapsuleCollider>().isTrigger = false;
        GetComponent<PlayerController>().enabled = true;
        Destroy(currentSaveEffect);
    }

    public void CancelPower()
    {
        if (!positionSaved || returningEffectPlaying)
        {
            return;
        }
        SoundManager.instance.GetComponent<AudioSource>().Stop();
        SoundManager.instance.GetComponent<AudioSource>().clip = null;
        SoundManager.instance.PlaySound(SoundManager.instance.positionPowerCancel);
        positionSaved = false;
        currentSaveEffect.GetComponent<ParticleSystem>().Stop();
        StartCoroutine(DestroyAfterTime(currentSaveEffect));
        currentSaveEffect = null;
    }

    IEnumerator DestroyAfterTime(GameObject particle)
    {
        GameObject p = particle;
        yield return new WaitForSeconds(3.5f);
        Destroy(p);
    }

    public void Power2()
    {
        CancelPower();
    }

    private void Update()
    {
        if (returningEffectPlaying)
        {
                GetComponent<PlayerController>().CameraHolder.transform.rotation = 
                    Quaternion.Slerp(GetComponent<PlayerController>().CameraHolder.transform.rotation, camLookDir, 2 * Time.deltaTime);
            if (timeElapsed < lerpDuration)
            {
                transform.position = Vector3.Lerp(startPosition, currentSaveEffect.transform.position - new Vector3(0,1,0), timeElapsed / lerpDuration);

                transform.rotation = Quaternion.Slerp(transform.rotation, lookDir, timeElapsed / (lerpDuration / 3 * 2));

                timeElapsed += Time.deltaTime;
            }
            else
            {
                transform.position = currentSaveEffect.transform.position - new Vector3(0, 1, 0);
                EndPower();
                timeElapsed = 0;
            }
        }

        /*if (currentSaveEffect != null) {
            Debug.DrawLine(transform.position, transform.position + dir * 10, Color.red, Mathf.Infinity);
        }*/
        
    }
}
