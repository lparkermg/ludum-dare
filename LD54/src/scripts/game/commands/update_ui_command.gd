extends Node
class_name UpdateUiCommand

# Stats
@export var worship_amount: int # 0 to 100 %

@export var max_population: int
@export var current_population: int

@export var current_deity_points: int

# Actions
@export var can_place_wonder: bool
@export var can_place_settlement: bool
