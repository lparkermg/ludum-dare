extends Node
class_name IslandersSystem

@export var deity_system: DeitySystem

@export var settlement_cost: int = 5

var state: IslandersModel

signal wonder_placed()

signal settlement_placed(new_settlement_amount: int, worship_points_left: int)
signal settlement_not_placed()
# Called when the node enters the scene tree for the first time.
func _ready():
	state = IslandersModel.new()
	
	state.wonder_placed = false
	
	state.amount_of_settlements = 0
	state.worship_amount = 0
	state.worship_points = 5
	
	deity_system.try_place_wonder.connect(_place_wonder)
	deity_system.try_place_settlement.connect(_place_settlement)


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
	settlement_placed.emit(state.amount_of_settlements, state.worship_points)
