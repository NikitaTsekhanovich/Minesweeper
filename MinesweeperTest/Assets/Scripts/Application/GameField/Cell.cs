using System;

namespace Application.GameField
{
    public class Cell
    {
        private bool _hasCheckbox;
        private bool _isOpen;

        public event Action<Cell> OnClickCell;
        public event Action<bool> OnCheckboxChange;
        public event Action OnOpenBomb;
        public event Action<int> OnFindNearbyBombs;
        public event Action OnOpenCell; 

        public Cell(int x, int y, int index)
        {
            X = x;
            Y = y;
            Index = index;
        }
        
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Index { get; private set; }
        public bool HasBomb { get; private set; }
        public int NearbyBombs { get; private set; }

        public void AddBomb()
        {
            HasBomb = true;
        }
        
        public void TryOpenCell()
        {
            if (_hasCheckbox || _isOpen)
            {
                return;
            }
            
            if (HasBomb)
            {
                OnOpenBomb?.Invoke();
            }
            else
            {
                OnClickCell?.Invoke(this);
            }
        }

        public void OpenCell()
        {
            _isOpen = true;
            OnOpenCell?.Invoke();
        }

        public void ChangeStateCheckbox()
        {
            if (_isOpen)
            {
                return;
            }
            
            _hasCheckbox = !_hasCheckbox;
            OnCheckboxChange?.Invoke(_hasCheckbox);
        }

        public void SetNearbyBombs(int nearbyBombs)
        {
            if (nearbyBombs > 0)
            {
                NearbyBombs = nearbyBombs;
                OnFindNearbyBombs?.Invoke(nearbyBombs);
            }
        }

        public void RestartCell()
        {
            _hasCheckbox = false;
            _isOpen = false;
            HasBomb = false;
            NearbyBombs = 0;
            
            OnCheckboxChange?.Invoke(_hasCheckbox);
        }
    }
}