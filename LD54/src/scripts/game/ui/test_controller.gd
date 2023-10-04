extends Control

@export var transition_to_end_button: BaseButton

var scene_transitioner: SceneTransitioner

# Called when the node enters the scene tree for the first time.
func _ready():
	scene_transitioner = get_tree().get_root().get_node("core_scene/scene_handler")

	transition_to_end_button.pressed.connect(_on_transition_clicked)

func _on_transition_clicked():
	scene_transitioner.transition_scene(SceneEnums.SceneTypes.End)
