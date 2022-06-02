using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // 파티클이 재생 중인가?
        if (particle.isPlaying)
            return;

        Destroy(gameObject);
    }
}
