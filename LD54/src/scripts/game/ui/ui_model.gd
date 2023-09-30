extends Node
class_name UiModel

# Stat display
@export var worship_amount: int # 0 to 100 %
@export var worship_points: int

@export var settlements: int

@export var max_population: int
@export var current_population: int

@export var current_deity_points: int

# Action stuff
@export var can_place_wonder: bool
@export var can_place_settlement: bool
