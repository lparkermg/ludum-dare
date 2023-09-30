extends Node

@export var loading_scene_root: Node # Holds the loading scene
@export var scene_root: Node # Holds the scene that's being displayed.

@export var loading_scene: PackedScene
@export var title_scene: PackedScene
@export var game_scene: PackedScene
@export var end_scene: PackedScene

@export var load_buffer_seconds: int

signal load_completed(scene: SceneEnums)

func transition_scene(new_scene: SceneEnums.SceneTypes):
	# Show the loading scene
	var loading_instance = loading_scene.new()
	
	loading_instance.name = "loading_scene"
	loading_scene_root.add_child(loading_instance)
	
	# unload the current scene
	for n in scene_root.get_children():
		scene_root.remove_child(n)
		n.queue_free()
		
	var scene:Node
	# load the new scene
	if new_scene == SceneEnums.SceneTypes.Title:
		scene = _load_scene(title_scene, scene_root)
	elif new_scene == SceneEnums.SceneTypes.Game:
		scene = _load_scene(game_scene, scene_root)
	elif new_scene == SceneEnums.SceneTypes.End:
		scene = _load_scene(end_scene, scene_root)
	
	# wait for load_buffer_seconds
	
	# unload loading screen
	loading_scene_root.remove_child(loading_instance)
	loading_instance.queue_free()
	
	# send load_complete signal.
	load_completed.emit(new_scene)

func _load_scene(new_scene: PackedScene, parent: Node) -> Node:
	var scene = new_scene.new()
	parent.add_child(scene)
	return scene
