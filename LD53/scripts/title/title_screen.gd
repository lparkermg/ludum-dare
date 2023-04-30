extends Node

@export var start_button: Button
@export var exit_button: Button

func _ready():
	start_button.pressed.connect(self._start_pressed)
	exit_button.pressed.connect(self._exit_pressed)

func _start_pressed():
	get_tree().change_scene_to_file("scenes/game.tscn")
	
func _exit_pressed():
	get_tree().quit()
		
