extends Node

@export var go_again_button: Button
@export var title_button: Button
@export var exit_button: Button

func _ready():
	go_again_button.set_pressed_no_signal(true)
	go_again_button.pressed.connect(_game_pressed)
	title_button.pressed.connect(_title_pressed)
	exit_button.pressed.connect(_exit_pressed)

func _game_pressed():
	get_tree().change_scene_to_file("scenes/game.tscn")
	
func _title_pressed():
	get_tree().change_scene_to_file("scenes/title.tscn")
	
func _exit_pressed():
	get_tree().quit()
		
