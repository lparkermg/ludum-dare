extends Node

@export var start_button: Button
@export var exit_button: Button

@export var highscore_label: RichTextLabel

func _ready():
	if FileAccess.file_exists("user://highscore.dat"):
		var highscore_file = FileAccess.open("user://highscore.dat", FileAccess.READ_WRITE)
		var current_highscore = highscore_file.get_16()
		highscore_label.text = "Current Highscore: %s" % str(current_highscore)
	
	start_button.pressed.connect(self._start_pressed)
	exit_button.pressed.connect(self._exit_pressed)

func _start_pressed():
	get_tree().change_scene_to_file("scenes/game.tscn")
	
func _exit_pressed():
	get_tree().quit()
		
