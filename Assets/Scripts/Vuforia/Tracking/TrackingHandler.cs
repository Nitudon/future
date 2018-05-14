using UnityEngine;
using UniRx;
using Vuforia;

/// <summary>
/// VuforiaのARカメラのトラッキングイベントハンドラー
/// </summary>
public class TrackingHandler : MonoBehaviour, ITrackableEventHandler{

    #region MEMBER_VARIABLES
    
    /// <summary>
    /// マーカーの識別イベント
    /// </summary>
    private BoolReactiveProperty _onTrackingFoundStatusChanged = new BoolReactiveProperty(false);
    public IReadOnlyReactiveProperty<bool> OnTrackingFoundStatusChanged => _onTrackingFoundStatusChanged;

    private TrackableBehaviour _trackingBehaviour;

    #endregion MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    private void Start()
    {
        _trackingBehaviour = GetComponent<ImageTargetBehaviour>();
        if (_trackingBehaviour)
        {
            _trackingBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    private void OnDestroy()
    {
        if (_trackingBehaviour)
        {
            _trackingBehaviour.UnregisterTrackableEventHandler(this);
        }
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + _trackingBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + _trackingBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    private void OnTrackingFound()
    {
        _onTrackingFoundStatusChanged.Value = true;
    }


    private void OnTrackingLost()
    {
        _onTrackingFoundStatusChanged.Value = false;
    }

    #endregion // PROTECTED_METHODS
}
