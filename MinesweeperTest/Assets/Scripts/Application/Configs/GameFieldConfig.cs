using UnityEngine;

namespace Application.Configs
{
    [CreateAssetMenu(fileName = nameof(GameFieldConfig), menuName = "Configs/" + nameof(GameFieldConfig))]
    public class GameFieldConfig : ScriptableObject
    {
        [SerializeField, Min(0)] public int _amountBombs;

        [field: SerializeField, Min(1)] public int FieldSizeHeight { get; private set; }
        [field: SerializeField, Min(1)] public int FieldSizeWidth { get; private set; }

        public int AmountBombs
        {
            get
            {
                var maxAmountBombs = FieldSizeHeight * FieldSizeWidth - 1;
                
                if (maxAmountBombs < _amountBombs)
                {
                    Debug.LogError("The number of bombs cannot be greater than " +
                                   "the field size minus one free cell to open. " +
                                   $"New value for the number of bombs: {maxAmountBombs}");

                    return maxAmountBombs;
                }
                
                return _amountBombs;
            }
        }
    }
}