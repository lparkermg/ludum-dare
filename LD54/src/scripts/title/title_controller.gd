extends Node

@export var title_label: Label
@export var title_action: Label

@export var title_button: BaseButton

var scene_transitioner: SceneTransitioner

# Called when the node enters the scene tree for the first time.
func _ready():
	scene_transitioner = get_tree().get_root().get_node("core_scene/scene_handler")
	
	title_label.text = "LD54 Game"
	title_action.text = "Click to start"
	title_button.pressed.connect(_on_button_clicked)
	


func _on_button_clicked():
	scene_transitioner.transition_scene(SceneEnums.SceneTypes.Game)
