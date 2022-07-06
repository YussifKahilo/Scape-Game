using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PositionPower : MonoBehaviour, PlayerPower
{
    [SerializeField] ParticleSystem positionSaveEffect;
    [SerializeField] GameObject effectColor;
    [SerializeField] ParticleSystem traceEffect;
    [SerializeField] GameObject modelMesh;

    NavMeshAgent agent;

    GameObject currentSaveEffect;

    bool positionSaved = false, returningEffectPlaying = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    public void Power1()
    {
        if (returningEffectPlaying)
        {
            return;
        }

        if (positionSaved)
        {
            modelMesh.SetActive(false);
            traceEffect.gameObject.SetActive(true);
            traceEffect.Play();
            effectColor.SetActive(true);
            GetComponent<PlayerController>().enabled = false;
            agent.enabled = true;
            returningEffectPlaying = true;
            agent.SetDestination(currentSaveEffect.transform.position);
        }
        else
        {
            currentSaveEffect = Instantiate(positionSaveEffect , transform.position + new Vector3(0 , 1 , 0), Quaternion.identity).gameObject;
            currentSaveEffect.GetComponent<ParticleSystem>().Play();
            positionSaved = true;
        }
    }

    void EndPower()
    {
        effectColor.SetActive(false);
        agent.isStopped = true;
        agent.enabled = false;
        returningEffectPlaying = false;
        positionSaved = false;
        modelMesh.SetActive(true);
        traceEffect.gameObject.SetActive(false);
        traceEffect.Stop();
        GetComponent<PlayerController>().enabled = true;
        transform.position = currentSaveEffect.transform.position - new Vector3(0 , 1 , 0);
        Destroy(currentSaveEffect);
    }

    void CancelPower()
    {
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
        if (positionSaved && returningEffectPlaying && Vector3.Distance(transform.position , currentSaveEffect.transform.position) < 1)
        {
            EndPower();
        }
    }
}
