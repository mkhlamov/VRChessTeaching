using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageProgressBar : MonoBehaviour
{
    public GameObject interactiveObject;
	public UnityEvent onBarFilled;

    [Header("Custom Settiings")]
    public bool disableOnFill    = false;

	// Время в секундах необходимое для заполнения Progressbar'а
	public float timeToFill = 1.0f;

    public bool enableImage = true;

	// Переменная, куда будет сохранена ссылка на компонент Image
	// текущего объекта, который является ProgressBar'ом
	private Image progressBarImage = null;

    private Image uiImage = null;
	public Coroutine barFillCoroutine = null;

    private bool fillingProcessIsRunning = false;
    private bool gazeOver = false;

    public bool GazeOver {
        get { return gazeOver; }
        set { gazeOver = value; }
    }

	void Start ()
	{
		progressBarImage = GetComponent<Image>();

		// Если у данного объекта нет компонента Image выводим ошибку
		// в консоль
		if(progressBarImage == null)
		{
			Debug.LogError("There is no referenced image on this component!");
		}

        uiImage = transform.GetChild(0).GetComponent<Image>();
        if (!enableImage)
        {
            uiImage.enabled = false;
        }

	}

	public void StartFillingProgressBar()
	{
        if (gazeOver && !fillingProcessIsRunning)
        {
            if (!enableImage)
            {
                uiImage.enabled = true;
            }
            barFillCoroutine = StartCoroutine("Fill");
            fillingProcessIsRunning = true;
        }
	}

    public void StopFillingProgressBar()
	{
        if (!gazeOver && fillingProcessIsRunning)
        {
            if (!enableImage)
            {
                uiImage.enabled = false;
            }
            StopCoroutine(barFillCoroutine);
            progressBarImage.fillAmount = 0.0f;
            fillingProcessIsRunning = false;
        }
	}

	IEnumerator Fill()
	{
        

        float startTime = Time.time;
		float overTime = startTime + timeToFill;

		while(Time.time < overTime)
		{
			progressBarImage.fillAmount = Mathf.Lerp(0, 1, (Time.time - startTime) / timeToFill);
			yield return null;
		}

		progressBarImage.fillAmount = 0.0f;


		if(onBarFilled != null)
		{
			onBarFilled.Invoke();
		}

        if(disableOnFill)
        {
            transform.parent.gameObject.SetActive(false);
        }

        if (!enableImage)
        {
            uiImage.enabled = false;
        }
    }
}