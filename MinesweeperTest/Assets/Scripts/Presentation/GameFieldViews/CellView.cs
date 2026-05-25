using Application.GameField;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Presentation.GameFieldViews
{
    public class CellView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _checkboxImage;
        [SerializeField] private Image _bombImage;
        [SerializeField] private Image _emptyCellImage;
        [SerializeField] private TMP_Text _nearbyBombsText;
        
        private Cell _cell;
        
        public void Constructor(Cell cell)
        {
            _cell = cell;
            
            _cell.OnCheckboxChange += ChangeStateCheckbox;
            _cell.OnOpenBomb += OpenBomb;
            _cell.OnFindNearbyBombs += ShowNearbyBombsText;
            _cell.OnOpenCell += OpenCell;
        }

        private void OnDestroy()
        {
            _cell.OnCheckboxChange -= ChangeStateCheckbox;
            _cell.OnOpenBomb -= OpenBomb;
            _cell.OnFindNearbyBombs -= ShowNearbyBombsText;
            _cell.OnOpenCell -= OpenCell;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _cell.TryOpenCell();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                _cell.ChangeStateCheckbox();
            }
        }

        private void ChangeStateCheckbox(bool state)
        {
            _checkboxImage.gameObject.SetActive(state);
        }

        private void OpenCell()
        {
            _emptyCellImage.gameObject.SetActive(true);
        }

        private void OpenBomb()
        {
            _bombImage.gameObject.SetActive(true);
        }

        private void ShowNearbyBombsText(int nearbyBombs)
        {
            _nearbyBombsText.gameObject.SetActive(true);
            _nearbyBombsText.text = nearbyBombs.ToString();
        }
    }
}