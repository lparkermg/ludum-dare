extends Node3D

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if Input.is_action_just_pressed("move_north"):
		position.z += 2
	elif Input.is_action_just_pressed("move_east"):
		position.x += 2
	elif Input.is_action_just_pressed("move_south"):
		position.z -= 2
	elif Input.is_action_just_pressed("move_west"):
		position.x -= 2

