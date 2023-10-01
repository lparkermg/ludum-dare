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
signal settlement_placed_ui(settlement_amount: int, new_max_settlers: int)
signal settlement_placed_map()

signal try_start_disaster(type: DisasterEnums.Disaster, location: Vector2i)
signal disaster_completed_ui()
signal disaster_settlements_destroyed_map(location: Vector2i)

signal settlement_destroyed_ui(settlement_amount: int, new_max_settlers: int)

signal settlers_arrived_ui(new_settler_amount: int)
signal worship_level_changed_ui(new_level: int)
signal deity_points_changed_ui(new_amount: int)
# Called when the node enters the scene tree for the first time.
func _ready():
	
	# UI Signals
	ui_controller.place_wonder_clicked.connect(_try_place_wonder)
	ui_controller.place_settlement_clicked.connect(_try_place_settlement)
	
	ui_controller.activate_earthquake_disaster_clicked.connect(func(): _try_start_disaster(DisasterEnums.Disaster.Earthquake))
	ui_controller.activate_flood_disaster_clicked.connect(func(): _try_start_disaster(DisasterEnums.Disaster.Flood))
	ui_controller.activate_fire_disaster_clicked.connect(func(): _try_start_disaster(DisasterEnums.Disaster.Fire))
	ui_controller.activate_lightning_disaster_clicked.connect(func(): _try_start_disaster(DisasterEnums.Disaster.Lightning))
	
	
	# Islander Signals
	islander_system.wonder_placed.connect(_wonder_placed)
	islander_system.settlement_placed.connect(_settlement_placed)
	islander_system.settlers_arrived.connect(_settlers_arrived)
	islander_system.worship_level_changed.connect(_worship_level_changed)
	islander_system.deity_points_increased.connect(_deity_points_increased)
	islander_system.disaster_complete.connect(_disaster_complete)
	
	islander_system.wonder_destroyed.connect(_disaster_wonder_destroyed)
	islander_system.settlement_destroyed.connect(_disaster_settlement_destroyed)
	
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

func _settlement_placed(settlement_amount: int, new_max_settlers: int, deity_points: int):
	print("settlement placed")
	settlement_placed_map.emit()
	settlement_placed_ui.emit(settlement_amount, new_max_settlers)
	deity_points_changed_ui.emit(deity_points)
	
func _settlers_arrived(new_settler_amount: int):
	settlers_arrived_ui.emit(new_settler_amount)
	
func _worship_level_changed(new_worship_level: int):
	worship_level_changed_ui.emit(new_worship_level)
	
func _deity_points_increased(new_amount: int):
	deity_points_changed_ui.emit(new_amount)
	
func _try_start_disaster(type: DisasterEnums.Disaster):
	var select_pos = map_controller.get_select_position()
	
	try_start_disaster.emit(type, select_pos)

func _disaster_settlement_destroyed(locations: Vector2i, new_settlement_amount: int, new_max_settlers_amount):
	print("deity system - settlements destroyed")
	disaster_settlements_destroyed_map.emit(locations)
	settlement_destroyed_ui.emit(new_settlement_amount, new_max_settlers_amount)

func _disaster_wonder_destroyed():
	# This is actually game over. A message needs to be populated and the scene 
	# needs to transition to the end.
	print("Peeps, it's game over.")

func _disaster_complete():
	print("Disaster Complete")
	disaster_completed_ui.emit()
