extends Node
class_name UiModel

@export var showing_help: bool

# Stat display
@export var worship_amount: int # 0 to 100 %

@export var settlements: int

@export var max_settlers: int
@export var current_settlers: int

@export var deity_points: int

@export var ascension_level: int

# Action stuff
@export var can_place_wonder: bool
@export var can_place_settlement: bool
@export var can_perform_actions: bool
