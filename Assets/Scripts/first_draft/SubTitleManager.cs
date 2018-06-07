using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Windows.Speech;

public class SubTitleManager : MonoBehaviour {
    public Text _statusText;
    public RawImage _statusImage;
    public DictationRecognizer _dictationRecognizer;
    public Text _subTitle;

    private GestureRecognizer _gestureRecognizer;
    private bool _isSleeping = true;
    Texture2D sleepingTexture;
    Texture2D listeningTexture;
    Texture2D thinkingTexture;

    // Use this for initialization
    void Start () {
        sleepingTexture = (Texture2D)Resources.Load("Sleeping");
        listeningTexture = (Texture2D)Resources.Load("Listening");
        thinkingTexture = (Texture2D)Resources.Load("Thinking");
        _gestureRecognizer = new GestureRecognizer();
        _isSleeping = true;
        
        _gestureRecognizer.TappedEvent += _gestureRecognizer_tappedEvent;
        _gestureRecognizer.StartCapturingGestures();
        

    }
    private void Awake()
    {
        _dictationRecognizer = new DictationRecognizer();
        _dictationRecognizer.DictationHypothesis+= _dictationRecognizer_DictationHypothesis;
        _dictationRecognizer.DictationResult    += _dictationRecognizer_DictationResult;
        _dictationRecognizer.DictationComplete  += _dictationRecognizer_DictationComplete;
        
            
        

    }

    private void _dictationRecognizer_DictationComplete(DictationCompletionCause cause)
    {
        //throw new NotImplementedException();
        SetSleeping();
        _dictationRecognizer.Stop();

    }

    private void _dictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        _subTitle.text = text;
        SetListening();
    }

    private void _dictationRecognizer_DictationHypothesis(string text)
    {
        SetThinking();
    }

    private void _gestureRecognizer_tappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        _isSleeping = !_isSleeping;
        if (_isSleeping)
        {
            SetSleeping();
            _dictationRecognizer.Stop();
        }
        else
        {
            SetListening();
            _dictationRecognizer.Start();
        }
        //throw new NotImplementedException();
    }

    private void SetThinking()
    {
        
        this._statusImage.texture = thinkingTexture;
        this._statusText.text = "Thinking";

    }

    private void SetSleeping()
    {
        this._statusText.text = "Sleeping";
        this._statusImage.texture = sleepingTexture;
        this._subTitle.text = "";

    }

    private void SetListening()
    {
     
        this._statusImage.texture = listeningTexture;
        this._statusText.text = "Listening";
        

    }


}
