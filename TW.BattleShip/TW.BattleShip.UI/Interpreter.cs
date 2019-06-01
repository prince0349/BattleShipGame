using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW.BattleShip.BusinessContracts;
using TW.BattleShip.Constants;
using TW.BattleShip.Extensions;
using TW.BattleShip.Models;

namespace TW.BattleShip.UI
{
    public class Interpreter
    {
        private IBattleshipBoardService _battleshipBoardService;
        private IGamePlayBoardService _gamePlayBoardService;

        private InputValidator _inputValidator;
        private BattleshipBoard _battleshipBoard;
        private GamePlay _gamePlay;

        public Interpreter(IBattleshipBoardService battleshipBoardService, IGamePlayBoardService gamePlayBoardService)
        {
            _battleshipBoardService = battleshipBoardService;
            _battleshipBoard = _battleshipBoardService.GetBattleshipBoard();
            _gamePlayBoardService = gamePlayBoardService;

            _inputValidator = new InputValidator(battleshipBoardService);
            _inputValidator.InputValidated += InputValidator_InputValidated;

            _gamePlay = new GamePlay(_gamePlayBoardService, _battleshipBoardService);
            _gamePlay.GamePlayCompleted += GamePlay_GamePlayCompleted;
        }

        public bool Parse(string commandString)
        {
            // Parse string and create Command
            var commandParts = commandString.Split(' ').ToList();
            var commandName = commandParts[0];
            var args = commandParts.Skip(1).ToList(); // the arguments is after the command

            switch (commandName)
            {
                case ApplicationConstants.Exit: return true;

                default:
                    if (string.Compare(args.First(), ApplicationConstants.Exit, true) == 0)
                        return true;

                    _inputValidator.Validate(commandName + ' ', args);
                    return false;
            }
        }

        private void InputValidator_InputValidated(object sender, InputValidatorArgs e)
        {
            if (!e.IsInputValid)
            {
                Console.WriteLine(ApplicationConstants.InvalidInputErrorMessage);
                Console.Write(e.Key.ReplaceUnderscoreWithBlankSpace());
            }
            else
            {
                switch (e.Key)
                {
                    case ApplicationConstants.BattleAreaWidthAndHeight:
                        Console.Write(ApplicationConstants.NumberOfBattleship.ReplaceUnderscoreWithBlankSpace());
                        _battleshipBoard.CurrentInput = ApplicationConstants.NumberOfBattleship;
                        break;

                    case ApplicationConstants.NumberOfBattleship:

                        Console.Write(ApplicationConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2.ReplaceUnderscoreWithBlankSpace().Replace(":", ApplicationConstants.ForExampleShipForPlayer1));
                        _battleshipBoard.CurrentInput = ApplicationConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2;
                        _battleshipBoard.CurrentInputShip--;
                        break;

                    case ApplicationConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2:
                        if (_battleshipBoard.CurrentInputShip >= 1)
                        {
                            Console.Write(ApplicationConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2.ReplaceUnderscoreWithBlankSpace().Replace(":", ApplicationConstants.ForExampleShipForPlayer2));
                            //BattleshipBoard.CurrentInput = "Enter_type_of_battleship_its_dimensions(width_and_height)_and_coordinates_for_player1_and_player2: ";
                            _battleshipBoard.CurrentInputShip--;
                        }
                        else
                        {
                            Console.Write(ApplicationConstants.SequenceOfMissileTargetLocationPlayer1.ReplaceUnderscoreWithBlankSpace().Replace(":", ApplicationConstants.ForExampleTargetForPlayer1));
                            _battleshipBoard.CurrentInput = ApplicationConstants.SequenceOfMissileTargetLocationPlayer1;
                        }
                        break;

                    case ApplicationConstants.SequenceOfMissileTargetLocationPlayer1:
                        Console.Write(ApplicationConstants.SequenceOfMissileTargetLocationPlayer1.ReplaceUnderscoreWithBlankSpace().Replace(":", ApplicationConstants.ForExampleTargetForPlayer2));
                        _battleshipBoard.CurrentInput = ApplicationConstants.SequenceOfMissileTargetLocationPlayer2;
                        break;

                    case ApplicationConstants.SequenceOfMissileTargetLocationPlayer2:
                        _battleshipBoard.CurrentInput = ApplicationConstants.BattleAreaWidthAndHeight;
                        Console.WriteLine("-------------------------------------------");
                        Console.WriteLine(ApplicationConstants.Output);
                        Console.WriteLine("-------------------------------------------");
                        Console.WriteLine();
                        //show output
                        _gamePlay.Start();
                        break;

                    default:
                        //something went wrong
                        foreach (var player in _battleshipBoard.Players)
                        {
                            player.Battleships.Clear();
                            player.TargetPositionString.Clear();
                        }
                        Console.Write(ApplicationConstants.BattleAreaWidthAndHeight.ReplaceUnderscoreWithBlankSpace());
                        _battleshipBoard.CurrentInput = ApplicationConstants.BattleAreaWidthAndHeight;
                        break;
                };
            }
        }

        private void GamePlay_GamePlayCompleted(object sender, GamePlayArgs e)
        {
            foreach (var outputString in e.Output)
                Console.WriteLine(outputString);
        }
    }
}
