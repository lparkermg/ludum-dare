extends Node
class_name DeitySystem

@export var ui_controller: Node
@export var map_controller: MapController
@export var islander_system: Node

signal place_at_selected_tile() # Attempts to place at selected tile (map_controller)
signal update_ui() # Updates the UI with the request (ui_controller)
signal select_new_tile(direction: MoveEnums.Tile) #Attempts to select a tile (map_controller)

# Called when the node enters the scene tree for the first time.
func _ready():
	# Listen for
		# Ui Updates
		# Map updates
		# Islander Updates
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	_handle_input()

func _handle_input():
	# Handle select tile
	if Input.is_action_just_pressed("move_up"):
		select_new_tile.emit(MoveEnums.Tile.Up)
	elif Input.is_action_just_pressed("move_right"):
		select_new_tile.emit(MoveEnums.Tile.Right)
	elif Input.is_action_just_pressed("move_down"):
		select_new_tile.emit(MoveEnums.Tile.Down)
	elif Input.is_action_just_pressed("move_left"):
		select_new_tile.emit(MoveEnums.Tile.Left)
	# Note: Placing stuff is handled when UI icons are clicked.
