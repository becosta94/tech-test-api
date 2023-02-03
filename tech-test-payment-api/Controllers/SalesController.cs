using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Contexts;
using tech_test_payment_api.Excepetions;
using tech_test_payment_api.Interfaces;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("Estabelecimento")]
    public class SalesController : ControllerBase
    {
        private ISaleUpdateStatus _saleUpdateStatus;
        private ISaleFinder _saleFinder;
        private ISaleFactory _saleFactory;
        private Sales _currentSale;

        public SalesController(ISaleUpdateStatus saleUpdateStatus, ISaleFinder saleFinder, ISaleFactory saleFactory)
        {
            _saleUpdateStatus = saleUpdateStatus;
            _saleFinder = saleFinder;
            _saleFactory = saleFactory;
        }

        [HttpGet("Venda")]
        public async Task<IActionResult> NewSale(int sellerId, string sellerName, int sellerCpf, string sellerEmail, string sellerPhone, string itens)
        {
            var newSale = _saleFactory.Create(sellerId, sellerName, sellerCpf, sellerEmail, sellerPhone, itens);
            if (newSale == null)
                return BadRequest("Não foi possível efetuar a compra");
            return Ok(newSale);
        }

        [HttpGet("Consulta")]
        public async Task<IActionResult> ConsultSale(int saleNumber)
        {
            _currentSale = _saleFinder.Find(saleNumber);
            if (_currentSale == null)
                return BadRequest("Venda não encontrada");
            else
                return Ok(_currentSale);
        }
        [HttpGet("Atualizar_Venda")]
        public async Task<IActionResult> UpdateStatus(int saleNumber, SaleStatus newStatus)
        {
            try
            {
                _currentSale = _saleFinder.Find(saleNumber);
                var saleUpdateStatusResponsse = _saleUpdateStatus.Update(saleNumber, newStatus);
                switch (saleUpdateStatusResponsse)
                {
                    case 0:
                        return Ok(_currentSale);
                    case 1:
                        throw new SaleNotFoundException("Venda não encontrada");
                    case 2:
                        throw new UpdateStatusNotAllowedException("O status só pode ser alterado para Pagamento Aceito ou Cancelada");
                    case 3:
                        throw new UpdateStatusNotAllowedException("O status só pode ser alterado para Enviado para Transportadora ou Cancelada");
                    case 4:
                        throw new UpdateStatusNotAllowedException("O status só pode ser alterado para Entregue");
                    default:
                        return BadRequest("Operação não permitida");
                }
            }
            catch (SaleNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }            
            catch (UpdateStatusNotAllowedException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}