extends Node
class_name IslandersSystem

@export var deity_system: DeitySystem

@export var settler_spawn_timer: Timer
@export var worship_increase_timer: Timer
@export var deity_points_increase_timer: Timer

# Cost stuff
@export var settlement_cost: int = 5


# Max Stuff
@export var max_settlers_per_settlement: int = 5

# This is how much the worship level will go up based on current amount.
# Reductions are a fixed amount
@export var worship_curve: Curve
@export var worship_drop_amount: float = 10

@export var deity_points_curve: Curve

var state: IslandersModel

signal wonder_placed()

signal settlement_placed(new_settlement_amount: int, new_max_settlers: int, deity_points_left: int)
signal settlement_not_placed()

signal settlers_arrived(new_settler_amount: int)
signal worship_level_changed(new_level: int)
signal deity_points_increased(new_deity_points: int)
# Called when the node enters the scene tree for the first time.
func _ready():
	state = IslandersModel.new()
	
	state.wonder_placed = false
	
	state.amount_of_settlements = 0
	state.worship_amount = 0
	state.deity_points = 5
	
	state.amount_of_settlers = 0
	state.max_amount_of_settlers = 0
	
	deity_system.try_place_wonder.connect(_place_wonder)
	deity_system.try_place_settlement.connect(_place_settlement)
	
	settler_spawn_timer.timeout.connect(_spawn_settlers)
	worship_increase_timer.timeout.connect(_worship_changed)
	deity_points_increase_timer.timeout.connect(_deity_points_increased)

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
	if state.deity_points < settlement_cost or state.wonder_placed_at == place_at:
		settlement_not_placed.emit()
		return
	
	state.deity_points = state.deity_points - settlement_cost
	state.amount_of_settlements = state.amount_of_settlements + 1
	state.max_amount_of_settlers = state.max_amount_of_settlers + max_settlers_per_settlement
	settlement_placed.emit(state.amount_of_settlements, state.max_amount_of_settlers, state.deity_points)
	
	if state.amount_of_settlements == 1:
		settler_spawn_timer.start()
		worship_increase_timer.start()
		deity_points_increase_timer.start()
		_spawn_settlers()
	
func _spawn_settlers():
	# 1 per settlement or maybe randon between 0 and amount of settlements?
	state.amount_of_settlers = state.amount_of_settlers + state.amount_of_settlements
	settlers_arrived.emit(state.amount_of_settlers)
	
func _worship_changed():
	if state.amount_of_settlers < 1:
		return

	if state.amount_of_settlers > state.max_amount_of_settlers:
		state.worship_amount = state.worship_amount - worship_drop_amount
		
		if state.worship_amount < 0:
			state.worship_amount = 0
	else:
		state.worship_amount = round(state.worship_amount + worship_curve.sample(float(state.amount_of_settlers) / float(state.max_amount_of_settlers)))
		
		if state.worship_amount > 100:
			state.worship_amount = 100
		elif state.worship_amount < 0:
			state.worship_amount = 0

	worship_level_changed.emit(int(state.worship_amount))
	
func _deity_points_increased():
	state.deity_points = round(state.deity_points + deity_points_curve.sample(state.worship_amount / 100))
	
	deity_points_increased.emit(int(state.deity_points))
