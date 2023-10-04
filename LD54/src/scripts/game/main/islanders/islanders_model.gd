extends Node
class_name IslandersModel

# Holds all the state relating to the islanders.

# Worship states
@export var worship_amount: float
@export var deity_points: float

@export var ascension_level: int

# Wonder state
@export var wonder_placed: bool
@export var wonder_placed_at: Vector2i

# Settlement state
@export var amount_of_settlements: int
@export var settlement_locations: Array[Vector2i]

@export var max_amount_of_settlers: int
@export var amount_of_settlers: int
