using UnityEngine;

public class Wiggle
{
    private float time = 0f;
    private float startTime = 0f;
    public float frequency = 1f;
    public float strength = 10f;
    public Vector2 direction = Vector2.zero;
    Vector2 noisePositionX = Vector2.zero;
    Vector2 noisePositionY = Vector2.zero;
    private float pi = 0f;

    public Wiggle(float str, float freq, float tim, Vector2 dir)
    {
        direction = dir;
        strength = str;
        frequency = freq;
        startTime = time = tim;
        noisePositionX = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        noisePositionY = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public bool IsDone()
    {
        return time == 0f;
    }

    public void SetDecay(float t)
    {
        startTime = time = t;
    }

    public Quaternion GetRotation(AnimationCurve strengthOverLifetime = null)
    {
        time = Mathf.MoveTowards(time, 0f, Time.deltaTime);
        if (direction != Vector2.zero)
        {
            Vector3 cross = Vector3.Cross(direction, Vector3.forward);
            float angle = strength * GetSine();
            if (strengthOverLifetime != null) angle *= strengthOverLifetime.Evaluate(1f - (time / startTime));
            return Quaternion.AngleAxis(angle, cross);
        }
        else
        {
            Vector2 perlin = GetPerlin();
            float offset = strength;
            if (startTime > 0f)
            {
                if (strengthOverLifetime != null) offset *= strengthOverLifetime.Evaluate(1f - (time / startTime));
                else offset *= (time / startTime);
            }
            return Quaternion.Euler(Mathf.Lerp(-1f, 1f, perlin.x) * offset, Mathf.Lerp(-1f, 1f, perlin.y) * offset, 0f);
        }
    }


    public Vector3 GetPosition(AnimationCurve strengthOverLifetime = null)
    {
        time = Mathf.MoveTowards(time, 0f, Time.deltaTime);
        Vector2 result = Vector2.zero;
        if (direction != Vector2.zero)
        {
            float offset = strength * GetSine();
            if (strengthOverLifetime != null) strength *= strengthOverLifetime.Evaluate(1f - (time / startTime));
            return direction * offset;
        }
        else
        {
            float offset = strength;
            if (startTime > 0f)
            {
                if (strengthOverLifetime != null) offset *= strengthOverLifetime.Evaluate(1f - (time / startTime));
                else offset *= (time / startTime);
            }
            Vector2 perlin = GetPerlin();
            return new Vector3(Mathf.Lerp(-1f, 1f, perlin.x) * offset, Mathf.Lerp(-1f, 1f, perlin.y) * offset, 0f);
        }
    }

    float GetSine()
    {
        pi += Time.deltaTime * frequency;
        if (pi > Mathf.PI * 2f) pi -= Mathf.PI * 2f;
        return Mathf.Sin(pi);
    }

    Vector2 GetPerlin()
    {
        noisePositionX.x += frequency * Time.deltaTime * 0.2f;
        noisePositionX.y += frequency * Time.deltaTime * 0.02f;
        noisePositionY.x += frequency * Time.deltaTime * 0.02f;
        noisePositionY.y += frequency * Time.deltaTime * 0.2f;
        return new Vector2(Mathf.PerlinNoise(noisePositionX.x, noisePositionX.y), Mathf.PerlinNoise(noisePositionY.x, noisePositionY.y));
    }

}
