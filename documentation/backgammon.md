## Objective

Need to be able to let users interact with a backgammon board that has spaces, bar, and pockets (goals). The board needs to accommodate spaces

## Game Flow

- player1
- roll dice
  - invoke board.onDiceRoll
    - will iterate over all spaces to see if a player's piece is there and can make a legal move, set piece.isInteractable accordingly.
    - will set board.interactableSpaceIds accordingly
    - will set board.remainingMoves according to dice roll (i.e. [2,3], [6,5], [4, 4, 4, 4])
- click space (start move)
  - invoke board.onMoveStart
    - try to find interactableSpaceIds with delta from clicked space's id in direction dependent on player (moves are unilateral)
- click space (end/cancel move)
  - invoke board.onSpaceClicked
- swap turn
  - invoke board.resetSpace

## Board

- A board must be a 3D mesh with collision so that dice may be able to roll naturally with physics instead of complicated faking.
- A board must contain 24 standard spaces
- A board must contain 1 bar space (centre of board for 'out')
- A board must contain 2 goal spaces

## Spaces

Spaces act as containers for backgammon pieces, they can be either primary or secondary colour. They must be interactable so that users can choose to place their piece on a space by clicking it.

Albedo - Space should expose a hue property so the board can decide if it's colour is primary or secondary (i.e. black or white)

Interaction

- A space must maintain an isInteractable boolean which will affect

  1.  if the onSpaceClick early returns
  2.  any UI or other indicators that the space is interactable

- OnClick of the space, an OnSpaceClicked signal will be fired with a reference to self

- Spaces will provide a public method checkSpaceLegalMove
