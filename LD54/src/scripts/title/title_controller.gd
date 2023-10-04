extends Node

@export var title_label: Label
@export var title_action: Label

@export var title_button: BaseButton

var scene_transitioner: SceneTransitioner
var audio_system: AudioSystem

# Called when the node enters the scene tree for the first time.
func _ready():
	scene_transitioner = get_tree().get_root().get_node("core_scene/scene_handler")
	
	title_label.text = "Islanders"
	title_action.text = "Click to start"
	title_button.pressed.connect(_on_button_clicked)
	
	audio_system = get_tree().get_root().get_node("core_scene/audio_handler")
	audio_system.sfx_complete.connect(_sfx_complete)
	
	audio_system.play_bgm_title()
	
func _on_button_clicked():
	audio_system.play_error_sound()

func _sfx_complete():
	audio_system.stop_bgm()
	audio_system.sfx_complete.disconnect(_sfx_complete)
	scene_transitioner.transition_scene(SceneEnums.SceneTypes.Game)
	
