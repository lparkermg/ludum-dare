extends Node
class_name IslandersSystem

@export var deity_system: DeitySystem
@export var settler_spawn_timer: Timer

@export var settlement_cost: int = 5
@export var max_settlers_per_settlement: int = 5

var state: IslandersModel

signal wonder_placed()

signal settlement_placed(new_settlement_amount: int, new_max_settlers: int, worship_points_left: int)
signal settlement_not_placed()

signal settlers_arrived(new_settler_amount: int)
# Called when the node enters the scene tree for the first time.
func _ready():
	state = IslandersModel.new()
	
	state.wonder_placed = false
	
	state.amount_of_settlements = 0
	state.worship_amount = 0
	state.worship_points = 5
	
	state.amount_of_settlers = 0
	state.max_amount_of_settlers = 0
	
	deity_system.try_place_wonder.connect(_place_wonder)
	deity_system.try_place_settlement.connect(_place_settlement)
	
	settler_spawn_timer.timeout.connect(_spawn_settlers)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _place_wonder(place_at: Vector2i):
	if state.wonder_placed:
		return
	
	state.wonder_placed = true
	state.wonder_placed_at = place_at
	wonder_placed.emit()
	
func _place_settlement(place_at: Vector2i):
	if state.worship_points < settlement_cost or state.wonder_placed_at == place_at:
		settlement_not_placed.emit()
		return
	
	state.worship_points = state.worship_points - settlement_cost
	state.amount_of_settlements = state.amount_of_settlements + 1
	state.max_amount_of_settlers = state.max_amount_of_settlers + max_settlers_per_settlement
	settlement_placed.emit(state.amount_of_settlements, state.max_amount_of_settlers, state.worship_points)
	
	if state.amount_of_settlements == 1:
		settler_spawn_timer.start()
	
func _spawn_settlers():
	# 1 per settlement or maybe randon between 0 and amount of settlements?
	state.amount_of_settlers = state.amount_of_settlers + state.amount_of_settlements
	settlers_arrived.emit(state.amount_of_settlers)
