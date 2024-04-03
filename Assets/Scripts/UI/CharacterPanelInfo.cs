// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CharacterPanelInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TMP_Text _characterName;
    [SerializeField] private TMP_Text _characterHealth;
    [SerializeField] private TMP_Text _characterSpeed;
    [SerializeField] private TMP_Text _characterDelay;
    [SerializeField] private Image _characterSprite;

    private Vector3 _originalScale;
    private const float IncreasedSize = 1.2f;
    private const float AnimatedSize = 1.3f;
    private const float ClickedScale = 0.9f;
    private const float Duration = 0.2f;
    private const int DelayMultiplayer = 100;

    private CharacterSettings _characterSettings;

    private void Start()
    {
        _originalScale = transform.localScale;
    }

    public void SetData(CharacterSettings characterSettings)
    {
        _characterName.text = characterSettings.Name;
        _characterHealth.text = characterSettings.Health.ToString();
        _characterSpeed.text = characterSettings.Speed.ToString();
        _characterDelay.text = (characterSettings.ShootDelay * DelayMultiplayer).ToString();

        _characterSprite.sprite = characterSettings.Sprite;
        _characterSettings = characterSettings;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(_originalScale * AnimatedSize, Duration).SetEase(Ease.InQuad)
            .OnComplete(() => transform.DOScale(_originalScale * IncreasedSize, Duration).SetEase(Ease.OutSine))
            .SetAutoKill(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(_originalScale, Duration).SetEase(Ease.InQuad).SetAutoKill(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CharactersSelectManager.Instance.SetSelectedCharacter(_characterSettings);

        AudioManager.Instance.PlaySound("Click");
        transform.DOScale(_originalScale * ClickedScale, Duration).SetEase(Ease.InQuad)
            .OnComplete(() => transform.DOScale(_originalScale, Duration).SetEase(Ease.OutSine))
            .SetAutoKill(true);
    }
}