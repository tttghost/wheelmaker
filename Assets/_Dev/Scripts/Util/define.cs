

using System;
using UnityEngine;

/// <summary>
/// 모든 상수에 대한 정의
/// </summary>
public static class define
{
    // scene
    public const string Scene_Title = nameof(Scene_Title);
    public const string Scene_Game = nameof(Scene_Game);


    // input
    public const string input_horizontal = "Horizontal";
    public const string input_vertical = "Vertical";

    // color
    public static Color red = Color.red;
    public static Color blue = Color.blue;
    public static Color green = Color.green;
    public static Color white = Color.white;
    public static Color black = Color.black;

    /// <summary>
    /// ====================================================================================================
    /// 
    /// path
    /// 
    /// ====================================================================================================
    /// </summary>
    // animation, animator
    private const string path_ani = "Ani/";
    public const string path_animator = path_ani + "mators/";
    public const string path_animation = path_ani + "mations/";

    // font
    public const string path_font = "Fonts/";
        public const string path_font_base = path_font + "base/";
        public const string path_font_material = path_font + "material/";

    // sound
    public const string path_sound = "Sounds/";
        public const string path_sound_audiomixer = path_sound + "AudioMixer/";
        public const string path_sound_ambience = path_sound + "Ambience/";
        public const string path_sound_dialogue = path_sound + "Dialogue/";
        public const string path_sound_interface = path_sound + "Interface/";
        public const string path_sound_music = path_sound + "Music/";
        public const string path_sound_soundeffects = path_sound + "SoundEffects/";

    // prefab
    public const string path_prefab = "Prefabs/";
        public const string path_prefab_go = path_prefab + "go/";
        public const string path_prefab_ui = path_prefab + "ui/";
        public const string path_prefab_player = path_prefab + "player/";
        public const string path_prefab_Controller = path_prefab + "Controller/";
}