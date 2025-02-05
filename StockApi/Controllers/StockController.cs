using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockApi.Data;
using StockApi.Dtos.Stock;
using StockApi.Mappers;
using StockApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using StockApi.Interfaces;
using StockApi.Repository;
using StockApi.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace StockApi.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IStockRepository _stockRepository;

        public StockController(ApplicationDbContext ctx, IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            _ctx = ctx;
        }

        [HttpGet]
        // When we Authorized in swagger only then we can call this endpoint
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) //?name=Hello
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stocks = await _stockRepository.GetAllAsync(query);
                
            var stockDto = stocks.Select(s => s.toStockDto());

            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock is null)
            {
                return NotFound();
            }

            return Ok(stock.toStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = stockDto.toStockFromCreateDto();
            await _stockRepository.CreateAsync(stockModel);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.toStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = await _stockRepository.UpdateAsync(id, updateDto);
            if (stockModel is null)
            {
                return NotFound();
            }

            return Ok(stockModel.toStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = await _stockRepository.DeleteAsync(id);
            if (stockModel is null)
            {
                return NotFound();
            }
           
            return NoContent();
        }

    }
}
