
using MotionSystems;
using System.Runtime.InteropServices;
using UnityEngine;

public class Platform : MonoBehaviour {
    
    private const int PLATFORM_POSITION_LOGIC_MIN = -32767;     // Platform logical min/max coordinates
    private const int PLATFORM_POSITION_LOGIC_MAX = 32767;

    private const float DRAWING_HEAVE_MAX = 1.0f;               // Heave maximum value that is available in the game
    private const float DRAWING_HEAVE_STEP = 0.05f;             // Heave change step

    private const float DRAWING_PITCH_MAX = 16;                 // Maximum value of pitch angle that is available in the game
    private const float DRAWING_PITCH_STEP = 1;                 // Pitch change step

    private const float DRAWING_ROLL_MAX = 16;                  // Maximum value of roll angle that is available in the game
    private const float DRAWING_ROLL_STEP = 1;                  // Roll change step

    private GameObject m_shaft = null;                          // Shaft object
    private GameObject m_board = null;                          // Board object

    private Vector3 m_originPosition;                           // Origin position of the shaft
    private Vector3 m_originRotation;                           // Origin rotation of the board

    private float m_heave = 0;                                  // Current platform's heave in game
    private float m_pitch = 0;                                  // Current platform's pitch in game
    private float m_roll = 0;                                   // Current platform's roll in game

    private ForceSeatMI m_fsmi;                                 // FSMI api
    private FSMI_TopTablePositionLogical m_platformPosition = new FSMI_TopTablePositionLogical();   // Position in logical coordinates that will be send to the platform

    public bool UserUpdate = false;                             // Disable if platform interaction should be programmatic and not according to user input

    private float startTime = 0.0f;                             // Time instant when the game started / the scene is loaded
    private float roundTime = 1.0f;                             // Round time (in seconds) between programmatic calls to platform

    void Start () {

        // Load ForceSeatMI library 
        m_fsmi = new ForceSeatMI();

        if (m_fsmi.IsLoaded()) {

            // Find platform's components
            m_shaft = GameObject.Find("Shaft");
            m_board = GameObject.Find("Board");

            SaveOriginPosition();
            SaveOriginRotation();

            // Prepare data structure by clearing it and setting correct size
            m_platformPosition.mask = 0;
            m_platformPosition.structSize = (byte)Marshal.SizeOf(m_platformPosition);

            m_platformPosition.state = FSMI_State.NO_PAUSE;

            // Set fields that can be changed by demo application
            m_platformPosition.mask = FSMI_POS_BIT.STATE | FSMI_POS_BIT.POSITION;

            m_fsmi.SetAppID(""); // If you have dedicated app id, remove ActivateProfile calls from your code
            m_fsmi.ActivateProfile("SDK - Positioning");
            m_fsmi.BeginMotionControl();

            SendDataToPlatform();

            startTime = Time.time;

        } else {
            Debug.LogError("ForceSeatMI library has not been found!Please install ForceSeatPM.");
        }
    }

    void Update () { 
        if (m_fsmi.IsLoaded()) {

            // Set back origin position and then modify it
            m_shaft.transform.position = m_originPosition;
            m_shaft.transform.Translate(0, m_heave, 0);

            // Set back origin rotation and then modify it
            m_board.transform.eulerAngles = m_originRotation;
            m_board.transform.Rotate(m_pitch, 0, -m_roll);

            SendDataToPlatform();
        }
    }

    void FixedUpdate() {
        if(UserUpdate) {
            
            // Update values according to user's input
            float input_pitch = Input.GetAxis("Vertical");
            float input_roll = Input.GetAxis("Horizontal");
            bool input_heave = Input.GetKey(KeyCode.Space);

            //Debug.Log(input_pitch + ", " + input_roll + ", " + input_heave);

            UpdateValue(ref m_pitch, input_pitch, DRAWING_PITCH_STEP, -DRAWING_PITCH_MAX, DRAWING_PITCH_MAX);
            UpdateValue(ref m_roll, input_roll, DRAWING_ROLL_STEP, -DRAWING_ROLL_MAX, DRAWING_ROLL_MAX);
            UpdateValue(ref m_heave, input_heave ? 1 : 0, DRAWING_HEAVE_STEP, 0, DRAWING_HEAVE_MAX);

        } else {
            ProgrammaticUpdate();
        }
    }

    void ProgrammaticUpdate() {

        // Debug.Log(Time.time + " | " + startTime);

        if (Time.time - startTime >= roundTime) {

            float input_pitch = Random.Range(-1.0f, 1.0f);
            float input_roll = Random.Range(-1.0f, 1.0f);
            bool input_heave = false;

            Debug.Log(input_pitch + ", " + input_roll + ", " + input_heave);

            UpdateValue(ref m_pitch, input_pitch, DRAWING_PITCH_STEP, -DRAWING_PITCH_MAX, DRAWING_PITCH_MAX);
            UpdateValue(ref m_roll, input_roll, DRAWING_ROLL_STEP, -DRAWING_ROLL_MAX, DRAWING_ROLL_MAX);
            UpdateValue(ref m_heave, input_heave ? 1 : 0, DRAWING_HEAVE_STEP, 0, DRAWING_HEAVE_MAX);

            startTime = Time.time;
        }
    }
    
    void OnDestroy() {
        if (m_fsmi.IsLoaded()) {
            m_fsmi.EndMotionControl();
            m_fsmi.Dispose();
        }
    }

    private void UpdateValue(ref float value, float input, float step, float min, float max) {
        if (0 < input) {
            value = Mathf.Clamp(value + step, min, max);
        } else if (0 > input) {
            value = Mathf.Clamp(value - step, min, max);
        } else if (value > 0) {
            value = Mathf.Clamp(value - step, 0, max);
        } else if (value < 0) {
            value = Mathf.Clamp(value + step, min, 0);
        }
    }

    private void SaveOriginPosition() {
        // Save origin position of the platform's shaft
        var x = m_shaft.transform.position.x;
        var y = m_shaft.transform.position.y;
        var z = m_shaft.transform.position.z;

        m_originPosition = new Vector3(x, y, z);
    }

    private void SaveOriginRotation() {
        // Save origin rotation of the platform's board
        var x = m_board.transform.eulerAngles.x;
        var y = m_board.transform.eulerAngles.y;
        var z = m_board.transform.eulerAngles.z;

        m_originRotation = new Vector3(x, y, z);
    }

    private void SendDataToPlatform() {
        // Convert parameters to logical units
        m_platformPosition.state = FSMI_State.NO_PAUSE;
        m_platformPosition.roll = (short)Mathf.Clamp(m_roll / DRAWING_ROLL_MAX * PLATFORM_POSITION_LOGIC_MAX, PLATFORM_POSITION_LOGIC_MIN, PLATFORM_POSITION_LOGIC_MAX);
        m_platformPosition.pitch = (short)Mathf.Clamp(m_pitch / DRAWING_PITCH_MAX * PLATFORM_POSITION_LOGIC_MAX, PLATFORM_POSITION_LOGIC_MIN, PLATFORM_POSITION_LOGIC_MAX);
        m_platformPosition.heave = (short)Mathf.Clamp(m_heave / DRAWING_HEAVE_MAX * PLATFORM_POSITION_LOGIC_MAX, PLATFORM_POSITION_LOGIC_MIN, PLATFORM_POSITION_LOGIC_MAX);

        // Send data to platform
        m_fsmi.SendTopTablePosLog(ref m_platformPosition);
    }
}
