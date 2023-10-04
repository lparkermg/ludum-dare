extends Control

@export var title: Label
@export var message: Label

@export var back_to_title_button: BaseButton

@export var background_image: TextureRect

@export var game_over_image: CompressedTexture2D
@export var complete_image: CompressedTexture2D

var audio_system: AudioSystem
var scene_handler: SceneTransitioner

# Called when the node enters the scene tree for the first time.
func _ready():
	audio_system = get_tree().get_root().get_node("core_scene/audio_handler")
	scene_handler = get_tree().get_root().get_node("core_scene/scene_handler")
	back_to_title_button.pressed.connect(_back_to_title)
	# TODO: Have a model populated from the games outcome (probably via storage)
	# TODO: Change to load from a model
	var file = FileAccess.open("user://islanders-complete.dat", FileAccess.READ)
	var game_over = file.get_8()
	
	if game_over == 0:
		audio_system.play_bgm_bad_game_over()
		title.text = "Game Over"
		message.text = "Your wonder got destroyed and your settlers lost interest. Un-guided they found the internet and *gags* twitter. They'll probably be fine... right?"
		background_image.texture = game_over_image
	else:
		audio_system.play_bgm_good_game_complete()
		title.text = "Acesended"
		message.text = "By keeping your settlers happy you have ascended to a higher plane... Who knows what happened to those you left behind though. You're a fully feldged god now, so who cares right? Right??"
		background_image.texture = complete_image

func _back_to_title():
	scene_handler.transition_scene(SceneEnums.SceneTypes.Title)
