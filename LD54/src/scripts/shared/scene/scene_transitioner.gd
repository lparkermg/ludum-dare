extends Node
class_name SceneTransitioner

@export var loading_screen_timer: Timer
var loading_instance: Node
var current_scene: SceneEnums.SceneTypes 
@export var loading_scene_root: Node # Holds the loading scene
@export var scene_root: Node # Holds the scene that's being displayed.

@export var loading_scene: PackedScene
@export var title_scene: PackedScene
@export var game_scene: PackedScene
@export var end_scene: PackedScene

@export var load_buffer_seconds: float = 2

signal load_completed(scene: SceneEnums)

func _ready():
	loading_screen_timer.wait_time = load_buffer_seconds
	loading_screen_timer.timeout.connect(_complete_loading)

func transition_scene(new_scene: SceneEnums.SceneTypes):
	print("transitioning to new scene")
	# unload the current scene
	for n in scene_root.get_children():
		scene_root.remove_child(n)
		n.queue_free()
	
	loading_instance = loading_scene.instantiate()
	
	loading_instance.name = "loading_scene"
	loading_scene_root.add_child(loading_instance)
	
	current_scene = new_scene
	loading_screen_timer.start()
	

func _load_scene(new_scene: PackedScene, parent: Node) -> Node:
	var scene = new_scene.instantiate()
	parent.add_child(scene)
	return scene

func _complete_loading():
	var scene: Node
	if current_scene == SceneEnums.SceneTypes.Title:
		scene = _load_scene(title_scene, scene_root)
	elif current_scene == SceneEnums.SceneTypes.Game:
		scene = _load_scene(game_scene, scene_root)
	elif current_scene == SceneEnums.SceneTypes.End:
		scene = _load_scene(end_scene, scene_root)
	
	loading_scene_root.remove_child(loading_instance)
	loading_instance.queue_free()
	loading_screen_timer.stop()
	# send load_complete signal.
	load_completed.emit(current_scene)
