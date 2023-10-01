extends Control

@export var title: Label
@export var message: Label

@export var background_image: TextureRect

@export var game_over_image: CompressedTexture2D
@export var complete_image: CompressedTexture2D

var audio_system: AudioSystem

# Called when the node enters the scene tree for the first time.
func _ready():
	audio_system = get_tree().get_root().get_node("core_scene/audio_handler")
	audio_system.play_bgm_bad_game_over()
	# TODO: Have a model populated from the games outcome (probably via storage)
	# TODO: Change to load from a model
	
	title.text = "Game Complete"
	message.text = "There could be some kind of message here to do with if you managed or not to keep your island nation in check..."
	background_image.texture = game_over_image
