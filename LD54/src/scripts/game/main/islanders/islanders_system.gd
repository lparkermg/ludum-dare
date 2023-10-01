extends Node
class_name IslandersSystem

@export var deity_system: DeitySystem

@export var settler_spawn_timer: Timer
@export var worship_increase_timer: Timer
@export var deity_points_increase_timer: Timer

# Cost stuff
@export var settlement_cost: int = 5
@export var lightning_cost: int = 5
@export var flood_cost: int = 5
@export var earthquake_cost: int = 5
@export var fire_cost: int = 5

# rng stuff
@export var flood_max_range: int = 30
@export var flood_min_range: int = -50
@export var flood_greater_range: int = 0

@export var earthquake_destroy_wonder_range_max: int = 50
@export var earthquake_destroy_wonder_range_min: int = -100
@export var earthquake_destroy_wonder_range_greater_than: int = 35

@export var earthquake_max_range: int = 30
@export var earthquake_min_range: int = -50
@export var earthquake_greater_range: int = 0

@export var fire_max_range: int = 30
@export var fire_min_range: int = -50
@export var fire_greater_range: int = 0

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

# TODO: Change settlers_arrived and deity_points_increased to be changed or something.
signal settlers_arrived(new_settler_amount: int)
signal worship_level_changed(new_level: int)
signal deity_points_increased(new_deity_points: int)

signal wonder_destroyed()
signal settlement_destroyed(location: Vector2i, new_settlement_amount: int, new_max_settlers_amount) 
signal disaster_complete()

# Called when the node enters the scene tree for the first time.
func _ready():
	state = IslandersModel.new()
	
	state.wonder_placed = false
	
	state.amount_of_settlements = 0
	state.worship_amount = 0
	state.deity_points = 50
	
	state.amount_of_settlers = 0
	state.max_amount_of_settlers = 0
	
	deity_system.try_place_wonder.connect(_place_wonder)
	deity_system.try_place_settlement.connect(_place_settlement)
	deity_system.try_start_disaster.connect(_start_disaster)
	
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
	
	state.settlement_locations.append(place_at)
	
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
	
func _start_disaster(type: DisasterEnums.Disaster, location: Vector2i):
	if type == DisasterEnums.Disaster.Lightning:
		_handle_lightning_disaster(location)
		disaster_complete.emit()
	elif type == DisasterEnums.Disaster.Flood:
		_handle_flood_disaster()
		disaster_complete.emit()
	elif type == DisasterEnums.Disaster.Earthquake:
		_handle_earthquake_disaster()
		disaster_complete.emit()
	elif type == DisasterEnums.Disaster.Fire:
		_handle_fire_disaster(location)
		disaster_complete.emit()

func _handle_lightning_disaster(location:Vector2i):
	if state.deity_points < lightning_cost:
		return
	state.deity_points = state.deity_points - lightning_cost
	deity_points_increased.emit(state.deity_points)
	# Lightning Strike
			# - Single tile
			# - Instant destruction of whats on the tile
			# - Can destroy Wonder
			# - Removes 5 settlers
	if location == state.wonder_placed_at:
		wonder_destroyed.emit()

	if state.settlement_locations.any(func(settlement: Vector2i): return settlement.x == location.x && settlement.y == location.y):
		_remove_settlements([location])
	
	state.amount_of_settlers = state.amount_of_settlers - 5
	
	if state.amount_of_settlers < 0:
		state.amount_of_settlers = 0
		
	settlers_arrived.emit(state.amount_of_settlers)

func _handle_flood_disaster():
	if state.deity_points < flood_cost:
		return
	state.deity_points = state.deity_points - flood_cost
	deity_points_increased.emit(state.deity_points)
	# Flood
			# - All tiles
			# - Chance to destroy what is on a tile
			# - Cannot destroy Wonder
			# - halves settlers
	
	# Implement random number here.
	var rng = RandomNumberGenerator.new()
	
	var settlements_to_remove = state.settlement_locations.filter(func(settlement: Vector2i): return rng.randi_range(flood_min_range, flood_max_range) > flood_greater_range)
	# TODO Remove destroyed locations form array
	_remove_settlements(settlements_to_remove)
	
	state.amount_of_settlers = state.amount_of_settlers / 2
	settlers_arrived.emit(state.amount_of_settlers)

func _handle_earthquake_disaster():
	if state.deity_points < earthquake_cost:
		return
	state.deity_points = state.deity_points - earthquake_cost
	deity_points_increased.emit(state.deity_points)
	# Earthquake
			# - All tiles
			# - Small chance to destroy what is on a tile
			# - Can destroy Wonder
			# - Removes half or more settlers
	var rng = RandomNumberGenerator.new()
	var destroy_wonder = rng.randi_range(earthquake_destroy_wonder_range_min, earthquake_destroy_wonder_range_max) > earthquake_destroy_wonder_range_greater_than # TODO: random number here
	
	if destroy_wonder:
		wonder_destroyed.emit()
	
	# TODO Remove destroyed locations form array
	
	var settlements_to_remove = state.settlement_locations.filter(func(settlement: Vector2i): return rng.randi_range(earthquake_min_range, earthquake_max_range) > earthquake_greater_range)
	_remove_settlements(settlements_to_remove)
	
	state.amount_of_settlers = state.amount_of_settlers / 2 
	settlers_arrived.emit(state.amount_of_settlers)
	
func _handle_fire_disaster(location: Vector2i):
	if state.deity_points < fire_cost:
		return
	state.deity_points = state.deity_points - fire_cost
	deity_points_increased.emit(state.deity_points)
	# Fire
			# - Single tile
			# - Chance to destroy what is on the tile
			# - Can destroy Wonder
			# - Removes  between 5 and 10 settlers
	var rng = RandomNumberGenerator.new()
	var should_destroy_at_location = rng.randi_range(fire_min_range, fire_max_range) > fire_greater_range
	if should_destroy_at_location:
		if location == state.wonder_placed_at:
			wonder_destroyed.emit()

		if state.settlement_locations.any(func(settlement: Vector2i): return settlement.x == location.x && settlement.y == location.y):
			_remove_settlements([location])
	
	state.amount_of_settlers = state.amount_of_settlers - rng.randi_range(5, 10) # Random between 5 and 10
	
	if state.amount_of_settlers < 0:
		state.amount_of_settlers = 0
		
	settlers_arrived.emit(state.amount_of_settlers)

func _remove_settlements(locations: Array[Vector2i]):
	for loc in locations:
		var loc_index = state.settlement_locations.find(loc)
		state.settlement_locations.remove_at(loc_index)
		state.amount_of_settlements = state.amount_of_settlements - 1
		state.max_amount_of_settlers = state.max_amount_of_settlers - max_settlers_per_settlement
		settlement_destroyed.emit(loc, state.amount_of_settlements, state.max_amount_of_settlers)
