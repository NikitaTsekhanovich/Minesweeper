using System;
using System.Collections.Generic;
using Application.Configs;
using Application.GameCore;
using Application.GameCore.States;
using Domain;

namespace Application.GameField
{
    public class Field : IDisposable
    {
        private const int MAX_NEIGHBOR_CELL = 2;
        private const int MIN_NEIGHBOR_CELL = -1;
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly Dictionary<(int, int), Cell> _cells = new();
        private readonly HashSet<(int, int)> _visitedPositions = new();
        private readonly int _cellsToOpen;

        private bool _firstOpenCell = true;
        private int _currentCellsToOpen;

        public event Action OnFindBomb;
        public event Action OnClearedBombs;
        
        public Field(GameFieldConfig gameFieldConfig, GameStateMachine gameStateMachine)
        {
            GameFieldConfig = gameFieldConfig;
            _gameStateMachine = gameStateMachine;
            _cellsToOpen = gameFieldConfig.FieldSizeHeight * gameFieldConfig.FieldSizeWidth - gameFieldConfig.AmountBombs;
            _currentCellsToOpen = _cellsToOpen;
        }
        
        public GameFieldConfig GameFieldConfig { get; private set; }
        
        public Cell GetCell(int x, int y, int index)
        {
            var cell = new Cell(x, y, index);
            _cells[(x, y)] = cell;
            
            cell.OnClickCell += OpenCell;
            cell.OnOpenBomb += FindBomb;
            
            return cell;
        }
        
        public void Dispose()
        {
            foreach (var cell in _cells.Values)
            {
                cell.OnClickCell -= OpenCell;
                cell.OnOpenBomb -= FindBomb;
            }
            
            _cells.Clear();
            _visitedPositions.Clear();
            _firstOpenCell = true;
            _currentCellsToOpen = _cellsToOpen;
        }

        private void OpenCell(Cell openingCell)
        {
            if (_firstOpenCell)
            {
                SpawnBombs(openingCell);
                _firstOpenCell = false;
                _gameStateMachine.EnterIn<LoopState>();
            }
            
            CheckNeighboringCells(openingCell);
            CheckCellsToOpen();
        }
        
        private void CheckNeighboringCells(Cell openingCell)
        {
            var nearbyBombs = 0;
            
            nearbyBombs = FindNearbyBombs(openingCell, nearbyBombs);
            
            openingCell.SetNearbyBombs(nearbyBombs);
            _currentCellsToOpen--;
            _visitedPositions.Add((openingCell.X, openingCell.Y));
            
            if (nearbyBombs == 0)
            {
                ChooseNeighboringCells(openingCell);
            }
            
            openingCell.OpenCell();
        }

        private void ChooseNeighboringCells(Cell openingCell)
        {
            for (var x = MIN_NEIGHBOR_CELL; x < MAX_NEIGHBOR_CELL; x++)
            {
                for (var y = MIN_NEIGHBOR_CELL; y < MAX_NEIGHBOR_CELL; y++)
                {
                    var searchPositionX = openingCell.X + x;
                    var searchPositionY = openingCell.Y + y;
                        
                    if (!_visitedPositions.Contains((searchPositionX, searchPositionY)) && 
                        _cells.TryGetValue((searchPositionX, searchPositionY), out var cell) &&
                        !cell.HasBomb)
                    {
                        CheckNeighboringCells(cell);
                    }
                }
            }
        }

        private int FindNearbyBombs(Cell openingCell, int nearbyBombs)
        {
            for (var x = MIN_NEIGHBOR_CELL; x < MAX_NEIGHBOR_CELL; x++)
            {
                for (var y = MIN_NEIGHBOR_CELL; y < MAX_NEIGHBOR_CELL; y++)
                {
                    var searchPositionX = openingCell.X + x;
                    var searchPositionY = openingCell.Y + y;

                    if (searchPositionX < 0)
                    {
                        break;
                    }
                    
                    if (searchPositionX == openingCell.X && searchPositionY == openingCell.Y)
                    {
                        continue;
                    }

                    if (_cells.TryGetValue((searchPositionX, searchPositionY), out var cell) && cell.HasBomb)
                    {
                        nearbyBombs++;
                    }
                }
            }

            return nearbyBombs;
        }
        
        private void SpawnBombs(Cell openingCell)
        {
            var cellsIds = GetCellsIds();
            var bombCellIndexes = GetBombCellIndexes(openingCell, cellsIds);
            PermutationShuffle.RearrangeShuffle(bombCellIndexes);

            for (var i = 0; i < GameFieldConfig.AmountBombs; i++)
            {
                cellsIds[bombCellIndexes[i]].AddBomb();
            }
        }

        private int[] GetBombCellIndexes(Cell openingCell, List<Cell> cellsIds)
        {
            var bombCellIndexes = new int[_cells.Count - 1];
            var k = 0;
            foreach (var cell in cellsIds)
            {
                if (cell.Index != openingCell.Index)
                {
                    bombCellIndexes[k] = cell.Index;
                    k++;
                }
            }

            return bombCellIndexes;
        }

        private List<Cell> GetCellsIds()
        {
            var cellsIds = new List<Cell>();
            foreach (var cell in _cells.Values)
            {
                cellsIds.Add(cell);
            }

            return cellsIds;
        }

        private void FindBomb()
        {
            OnFindBomb?.Invoke();
        }

        private void CheckCellsToOpen()
        {
            if (_currentCellsToOpen <= 0)
            {
                OnClearedBombs?.Invoke();
            }
        }
    }
}