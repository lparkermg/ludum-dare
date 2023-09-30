extends Control

@export var title: Label
@export var message: Label

# Called when the node enters the scene tree for the first time.
func _ready():
	# TODO: Have a model populated from the games outcome (probably via storage)
	# TODO: Change to load from a model
	
	title.text = "Game Complete"
	message.text = "There could be some kind of message here to do with if you managed or not to keep your island nation in check..."
