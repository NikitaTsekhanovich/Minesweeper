using System;
using System.Collections.Generic;
using Application.GameField;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.GameFieldViews
{
    public class FieldView : MonoBehaviour, IDisposable
    {
        [SerializeField] private GridLayoutGroup _cellViewParent;
        [SerializeField] private CellView _cellViewPrefab;
        
        private readonly List<CellView> _cellViews = new ();
        
        private Field _field;
        private RectTransform _rectTransform;
        
        public void Constructor(Field field)
        {
            _field = field;
            _rectTransform = GetComponent<RectTransform>();
            
            var cellSizeX = _rectTransform.rect.width / _field.GameFieldConfig.FieldSizeHeight - _cellViewParent.spacing.x;
            var cellSizeY = _rectTransform.rect.height / _field.GameFieldConfig.FieldSizeWidth - _cellViewParent.spacing.y;
            
            _cellViewParent.constraintCount = _field.GameFieldConfig.FieldSizeHeight;
            _cellViewParent.cellSize = new Vector2(cellSizeX, cellSizeY);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void InitGameField()
        {
            var indexCell = 0;
            
            for (var i = 0; i < _field.GameFieldConfig.FieldSizeHeight; i++)
            {
                for (var j = 0; j < _field.GameFieldConfig.FieldSizeWidth; j++)
                {
                    var cellView = Instantiate(_cellViewPrefab, _cellViewParent.transform);
                    var cell = _field.GetCell(i, j, indexCell);
                    cellView.Constructor(cell);
                    _cellViews.Add(cellView);
                    indexCell++;
                }
            }
        }

        public void RestartFieldView()
        {
            _field.RestartField();
            
            foreach (var cellView in _cellViews)
            {
                cellView.RestartCellView();
            }
        }

        public void Dispose()
        {
            foreach (var cellView in _cellViews)
            {
                Destroy(cellView.gameObject);
            }
            
            _cellViews.Clear();
            _field = null;
        }
    }
}