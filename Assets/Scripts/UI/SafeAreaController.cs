using UnityEngine;

namespace BattleForAltaria.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public sealed class SafeAreaController : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Rect _lastSafeArea;
        private Vector2Int _lastScreenSize;

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
            ApplyIfChanged(true);
        }

        private void OnEnable()
        {
            ApplyIfChanged(true);
        }

        private void Update()
        {
            ApplyIfChanged(false);
        }

        private void ApplyIfChanged(bool force)
        {
            var safeArea = Screen.safeArea;
            var screenSize = new Vector2Int(Screen.width, Screen.height);

            if (!force && safeArea == _lastSafeArea && screenSize == _lastScreenSize)
                return;

            _lastSafeArea = safeArea;
            _lastScreenSize = screenSize;

            if (screenSize.x <= 0 || screenSize.y <= 0)
                return;

            _rectTransform.anchorMin = new Vector2(
                safeArea.xMin / screenSize.x,
                safeArea.yMin / screenSize.y);
            _rectTransform.anchorMax = new Vector2(
                safeArea.xMax / screenSize.x,
                safeArea.yMax / screenSize.y);
            _rectTransform.offsetMin = Vector2.zero;
            _rectTransform.offsetMax = Vector2.zero;
        }
    }
}
