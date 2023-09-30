extends Node
class_name DeitySystem

@export var ui_controller: UiController
@export var map_controller: MapController
@export var islander_system: IslandersSystem

signal place_at_selected_tile() # Attempts to place at selected tile (map_controller)
signal select_new_tile(direction: MoveEnums.Tile) #Attempts to select a tile (map_controller)

signal try_place_wonder(position: Vector2i)
signal wonder_placed_ui()
signal wonder_placed_map()

signal try_place_settlement(position: Vector2i)
signal settlement_placed_ui(settlement_amount: int, new_max_settlers: int, worship_points_amount: int)
signal settlement_placed_map()

signal settlers_arrived_ui()
# Called when the node enters the scene tree for the first time.
func _ready():
	
	# UI Signals
	ui_controller.place_wonder_clicked.connect(_try_place_wonder)
	ui_controller.place_settlement_clicked.connect(_try_place_settlement)
	
	# Islander Signals
	islander_system.wonder_placed.connect(_wonder_placed)
	islander_system.settlement_placed.connect(_settlement_placed)
	islander_system.settlers_arrived.connect(_settlers_arrived)
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

func _try_place_wonder():
	var select_pos = map_controller.get_select_position()

	try_place_wonder.emit(select_pos)
	
func _try_place_settlement():
	var select_pos = map_controller.get_select_position()
	
	try_place_settlement.emit(select_pos)
	
func _wonder_placed():
	wonder_placed_ui.emit()
	wonder_placed_map.emit()

func _settlement_placed(settlement_amount: int, new_max_settlers: int, worship_points: int):
	print("settlement placed")
	settlement_placed_map.emit()
	settlement_placed_ui.emit(settlement_amount, new_max_settlers, worship_points)
	
func _settlers_arrived(new_settler_amount: int):
	settlers_arrived_ui.emit(new_settler_amount)
